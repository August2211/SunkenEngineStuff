using Il2Cpp;
using UnityEngine;
using Il2CppSystem.Linq;
using MelonLoader;
using HarmonyLib;
using Il2CppSystem.Runtime.Remoting.Messaging;

namespace SunkenEngineStuffStuff
{
    [HarmonyPatch(typeof(CarnivalGameDuckHuntController), nameof(CarnivalGameDuckHuntController.ShootRaycastLogic))]
    public static class Patch_ShootRaycastLogic
    {
        [HarmonyPrefix]
        public static bool Prefix(CarnivalGameDuckHuntController __instance)
        {
            if (!Duckhunt.AimbotEnabled() || !CarnivalManager.Instance.CarnivalGameDuckHuntController.IsGameActive)
            {
                return true;
            }

            var activeTargets = __instance.activeTargets;
            if (activeTargets != null && activeTargets.Count > 0)
            {
                foreach (var t in activeTargets)
                {
                    if (t != null && t.Data != null && t.Data.HuntType == HuntType.Duck)
                    {
                        __instance.OnTargetShot(t);
                        MelonLogger.Msg("[Duckhunt] Silent aimed duck!");
                        return false;
                    }
                }
            }

            return true;
        }
    }



    public static class Duckhunt
    {
        private static bool aimbotEnabled = false;

        public static void ToggleAimbot()
        {
            MelonLogger.Msg("[Duckhunt] Toggled Aimbot");

            aimbotEnabled = !aimbotEnabled;
        }

        public static bool AimbotEnabled()
        {
            return aimbotEnabled;
        }
    }
}