using Il2Cpp;
using MelonLoader;

namespace SunkenEngineStuffStuff
{
    public static class Utils
    {
        private static PlayerFPSController controller;

        public static PlayerFPSController GetPlayer()
        {
            if (controller != null)
                return controller;

            controller = UnityEngine.Object.FindObjectOfType<PlayerFPSController>();

            if (controller == null)
            {
                MelonLogger.Warning("[Utils] Could not find PlayerFPSController in scene!");
                return null;
            }

            MelonLogger.Msg("[Utils] Found PlayerFPSController on scene!");

            return controller;
        }
    }
}