using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL.Interface;
using Route.C41.DAL.Models;
using Route.C41.PL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Route.C41.PL.Controllers
{
    //Inhertiance : DepartmentController is a  Controller
    //Composition : DepartmentController has a Controller =>must | Mandatory
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public DepartmentController(
            IUnitOfWork unitOfWork,
            IWebHostEnvironment env,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _env = env;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchInp)
        {
            IEnumerable<Department> departments;
            if (string.IsNullOrEmpty(SearchInp))
                departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
            else
                departments = _unitOfWork.DepartmentRepository.SearchByName(SearchInp);

            var mappedDept = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDept);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)  //Serever Side Validation
            {
                var mappedDept=_mapper.Map<DepartmentViewModel,Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Add(mappedDept);
                var count =await _unitOfWork.Complete();
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        //Department/Details/10
        //Department/Details
        //[HttpGet]
        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest(); //400

            var department =await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            var mappedDept=_mapper.Map<Department,DepartmentViewModel>(department);
            if (department is null)
                return NotFound(); //404    
            return View(viewName, mappedDept);
        }

        //Department/Edit/10
        //Department/Edit
        //[HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
            ///if(!id.HasValue)
            ///    return BadRequest();
            ///var department = _departmentRepository.Get(id.Value);
            ///if(department is null)
            ///    return NotFound();
            ///return View(department);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(departmentVM);
            try
            {
                var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Update(mappedDept);
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
                return View(departmentVM);
            }

        }

        //[HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM)
        {
            try
            {
                var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);
                _unitOfWork.DepartmentRepository.Delete(mappedDept);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    ModelState.AddModelError(string.Empty, "An Error Occured during Updating the Department");
                return View(departmentVM);
            }
        }
    }
}
