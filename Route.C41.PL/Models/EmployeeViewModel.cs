using Microsoft.AspNetCore.Http;
using Route.C41.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Route.C41.PL.Models
{
     public enum Gender
   {
       [EnumMember(Value ="Male")]
      Male=1, 
       [EnumMember(Value ="Female")]
      Female=2
   }
   public enum EmpType
   {
       FullTime=1,
       PartTime=2,
   }

   //ViewMddel 
   public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="The Name Is Required")]
       [MinLength(5,ErrorMessage ="The MinLength must be 5")]
       [MaxLength(50,ErrorMessage = "The MaxLength is 50")]
       public string Name { get; set; }
       [Range(22,60)]
       public int? Age { get; set; }
       [Required]
       [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{3,10}-[a-zA-Z]{3,10}-[a-zA-Z]{3,10}$"
           ,ErrorMessage ="The Address must be Like 123-Street-City-Country")]
       public string Address { get; set; }
       [DataType(DataType.Currency)]
       public decimal Salary { get; set; }
       public bool IsActive { get; set; }
       [EmailAddress]
       [Required]
       public string Email { get; set; }
       [Phone]
       public string PhoneNumber { get; set; }
       public DateTime HiringDate { get; set; }
       public Gender Gender { get; set; }
       public  EmpType EmployeeType { get; set; }
       public string ImageName  { get; set; }
       public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public IFormFile Image { get; set; }

    }
}
