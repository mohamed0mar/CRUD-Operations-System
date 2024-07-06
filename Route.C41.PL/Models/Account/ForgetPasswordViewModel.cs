using System.ComponentModel.DataAnnotations;

namespace Route.C41.PL.Models.Account
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage ="Email Is Required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }
	}
}
