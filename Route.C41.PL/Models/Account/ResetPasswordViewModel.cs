using System.ComponentModel.DataAnnotations;

namespace Route.C41.PL.Models.Account
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "New Password Is Required")]
		[MinLength(5, ErrorMessage = "Minimum Password Length Is 5")]
		[DataType(DataType.Password)]
        public string NewPassword { get; set; }
		[Required(ErrorMessage = "Confirm Password Is Required")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword),ErrorMessage ="ConfirmPassword dosent match with New Password")]
		public string ConfirmPassword { get; set; }
	}
}
