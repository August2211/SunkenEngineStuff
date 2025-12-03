using MelonLoader;
using Il2Cpp;

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

            MelonLogger.Msg($"[License] Unlocked {LicenseManager.Instance.Licenses.Count} licenses!");
        }
    }
}
