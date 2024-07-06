using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Route.C41.BLL.Interface;
using Route.C41.BLL.Repositories;
using Route.C41.DAL.Data;
using Route.C41.DAL.Models;
using Route.C41.PL.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.C41.PL
{
	public class Startup
	{
		public IConfiguration Configuration
		{
			get;
		}  //Public Read Only Property
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}


		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllersWithViews();


			services.AddDbContext<ApplicationDbContext>(
				option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
				);


			services.AddApplicationServices();

			//services.AddScoped<UserManager<ApplicationUser>>();

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				// Password settings
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = false;

				// Lockout settings
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;

				// User settings
				options.User.RequireUniqueEmail = true;
			})
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders(); 

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Account/SignIn";
				options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
				options.AccessDeniedPath = "/Home/Error";
			});


			
			#region The Suitable life Time for Services We want to add to it DI

			//Allow DI For DbContex
			//The Suitable life Time for Services We want to add to it DI 

			//services.AddTransient<ApplicationDbContext>();
			//AddTransient
			///In The Same Request 
			///If the user ask from CLR to Create more than one Object from DbContext 
			///The CLR in Every Time Will Create new Object from Cleass Dbcontext

			//services.AddScoped<ApplicationDbContext>();
			//AddScoped
			///In The Same Request 
			///If the user ask from CLR to Create more than one Object from DbContext 
			///The CLR Will Create the Same Object from Cleass Dbcontext 
			///as Long as he still in the Same Request

			//services.AddSingleton<ApplicationDbContext>();
			//AddSingleton
			///When Sameone Ask from CLR to Create Object from DbContext 
			///CLR will Create Object from DbContext and Store this Object in Heap 
			///And Will Send the Addres for this Object to the User [he ask it ]
			///And this Object will be Open As Soon As The User Still Open the Session
			///The Connection will Be Open [this object will be accessable] As Soon As The User Still Open the Session 
			#endregion

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
