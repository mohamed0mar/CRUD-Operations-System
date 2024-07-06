using Route.C41.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Route.C41.PL.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Is Required!")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Code Is Required!")]
        public string Code { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }= DateTime.Now;

        public  ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
