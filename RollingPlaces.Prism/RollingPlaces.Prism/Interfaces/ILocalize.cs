<<<<<<< HEAD
﻿using System.Globalization;

namespace RollingPlaces.Prism.Interfaces
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo();

		void SetLocale(CultureInfo ci);
	}
}
=======
﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RollingPlaces.Prism.Interfaces
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();

        void SetLocale(CultureInfo ci);
    }

}
>>>>>>> RamaJulian
