using System.Threading.Tasks;

namespace Route.C41.PL.Services.EmailSender
{
	public interface IEmailSender
	{
		Task SendAsync(string from, string recipients, string subject, string body);
	}
}
