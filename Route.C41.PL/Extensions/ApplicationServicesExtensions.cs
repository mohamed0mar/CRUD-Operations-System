using Microsoft.Extensions.DependencyInjection;
using Route.C41.BLL;
using Route.C41.BLL.Interface;
using Route.C41.BLL.Repositories;
using Route.C41.PL.Helpers;
using Route.C41.PL.Services.EmailSender;
namespace Route.C41.PL.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{

			services.AddTransient<IEmailSender, EmailSender>();
			services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
			return services;
		}
	}
}
