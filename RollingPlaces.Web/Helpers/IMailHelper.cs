using RollingPlaces.Common.Models;

namespace RollingPlaces.Web.Helpers
{
	public interface IMailHelper
	{
		Response SendMail(string to, string subject, string body);
	}
}
