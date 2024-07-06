using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Route.C41.BLL.Interface;
using Route.C41.BLL.Repositories;
using Route.C41.DAL.Models;
using Route.C41.PL.Helpers;
using Route.C41.PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.C41.PL.Controllers
{
	[Authorize]
	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public EmployeeController(
            IUnitOfWork unitOfWork,
            IWebHostEnvironment env,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchInp)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchInp))
                employees =await _unitOfWork.EmployeeRepository.GetAllAsync();
            else
                employees = _unitOfWork.EmployeeRepository.SearchByName(SearchInp.ToLower());

            var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmp);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            employeeVM.ImageName =await DocumentSettings.UploadFile(employeeVM.Image, "images");

            if (ModelState.IsValid)
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);


                _unitOfWork.EmployeeRepository.Add(mappedEmp);
                var count =await _unitOfWork.Complete();
                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created Successfully";
                }
                else
                    TempData["Message"] = "An Error Has Occured ,Employee Not Created";
                return RedirectToAction(nameof(Index));
            }

            return View(employeeVM);
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            var MappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound();
            if (viewName.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                TempData["ImageName"] = employee.ImageName;
            return View(viewName, MappedEmp);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {

            if (id != employeeVM.Id)
                return BadRequest();
            employeeVM.ImageName =await DocumentSettings.UploadFile(employeeVM.Image, "images");
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Update(mappedEmp);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //1-Log Exceptiob
                //2-Frindly Message
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Occured during Updating the Department");
                return View(employeeVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeMV)
        {
            try
            {
                employeeMV.ImageName = TempData["ImageName"] as string;
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeMV);
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                var count =await _unitOfWork.Complete();
                if (count > 0)
                {
                    DocumentSettings.DeleteFile(employeeMV.ImageName, "images");
                    return RedirectToAction(nameof(Index));
                }
                return View(employeeMV);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Occured during Updating the Department");
                return View(employeeMV);
            }

        }
    }
}
