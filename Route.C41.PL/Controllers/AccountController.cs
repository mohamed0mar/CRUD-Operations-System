using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Route.C41.DAL.Models;
using Route.C41.PL.Models.Account;
using Route.C41.PL.Services.EmailSender;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Route.C41.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IConfiguration _configuration;
		private readonly IEmailSender _emailSender;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IConfiguration configuration,
			IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
			_emailSender = emailSender;
		}

		#region Sing Up 

		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
			{

				var user = await _userManager.FindByNameAsync(model.Username);
				if (user is null)
				{
					user = new ApplicationUser()
					{
						FName = model.FirstName,
						LName = model.LastName,
						UserName = model.Username,
						Email = model.Email,
						IsAgress = model.IsAgree
					};

					var result = await _userManager.CreateAsync(user, model.Password);

					if (result.Succeeded)
						return RedirectToAction(nameof(SignIn));

					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
				}

				ModelState.AddModelError(string.Empty, "This Account Is Already Exist");

			}
			return View(model);
		}

		#endregion

		#region Sign In
		public IActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var flag = await _userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var result = await _signInManager.PasswordSignInAsync
							(user, model.Password, model.RememberMe, false);

						if (result.IsLockedOut)
							ModelState.AddModelError(string.Empty, "Your Account Is Locked!!");

						//if (result.IsNotAllowed)
						//	ModelState.AddModelError(string.Empty, "Your Email is Not Confirmed yet!!");

						if (result.Succeeded)
							return RedirectToAction(nameof(HomeController.Index), "Home");

					}
				}
				ModelState.AddModelError(string.Empty, "Invalid Login");
			}
			return View(model);
		}

		#endregion

		#region Sign Out
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		}

		#endregion

		#region Forget Password
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);

					var resetYourPasswordUrl = Url.Action("ResetPassword", "Account", 
						new { email = user.Email, token = resetPasswordToken }, "https", "localhost:5001");


					await _emailSender.SendAsync(
						from: _configuration["EmailSettings:SenderEmail"],
						recipients: model.Email,
						subject: "Reset Your Password",
						body: resetYourPasswordUrl
						);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "Ther Is No Account With This EMail!!");
			}
			return View(model);
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}
		#endregion

		#region ResetPassword

		public IActionResult ResetPassword(string email, string token)
		{
			TempData["Email"] = email;
			TempData["Token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["Email"] as string;
				var token = TempData["Token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				if (user != null)
				{
					await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
					return RedirectToAction(nameof(SignIn));
				}
				ModelState.AddModelError(string.Empty, "Url Is Not Valid");
			}
			return View(model);
		}
		#endregion
	}
}
