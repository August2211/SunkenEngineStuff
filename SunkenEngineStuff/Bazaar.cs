using Il2Cpp;
using MelonLoader;
using UnityEngine.UIElements.UIR;

namespace SunkenEngineStuffStuff
{
    public static class Bazaar
    {
        public static void ClearCustomers()
        {
            if (TimeManager.Instance.IsNight) return;

            BazaarManager.Instance.HandleNightStarted();
            BazaarManager.Instance.HandleDayStarted();
        }
    }
}
