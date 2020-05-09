using System.Globalization;

namespace RollingPlaces.Prism.Interfaces
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo();

		void SetLocale(CultureInfo ci);
	}
}