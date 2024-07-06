using System.ComponentModel.DataAnnotations;

namespace Route.C41.PL.Models.Account
{
	public class SignInViewModel
	{
		public string Username { get; set; }
		[Required(ErrorMessage ="Email Is Required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
