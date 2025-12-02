using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunkenEngineStuffStuff
{
    public static class License
    {
        public static void UnlockAll()
        {
            foreach (var license in LicenseManager.Instance.Licenses)
            {
                LicenseManager.Instance.UnlockLicense(license);
            }
        }
    }
}
