using System.ComponentModel.DataAnnotations;

namespace Route.C41.PL.Models.Account
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="Username Is Required")]
        public string Username { get; set; }
		[Required(ErrorMessage ="Email Is Required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "First Name Is Required")]
		[Display(Name ="First Name")]
        public string FirstName { get; set; }
		[Required(ErrorMessage = "Last Name Is Required")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Password Is Required")]
		[MinLength(5, ErrorMessage = "Minimum Password Length Is 5")]
		[DataType(DataType.Password)]
        public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password Is Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="ConfirmPassword dosent match with Password")]
		public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
