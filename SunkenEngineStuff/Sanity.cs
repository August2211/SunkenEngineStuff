using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunkenEngineStuffStuff
{
    public static class Sanity
    {
        private static Il2CppSystem.Action OriginalSanityHandler = null;

        private static bool isHandling = false;
        private static float lockedSanityValue = -1;

        public static Action SanityHandler = new Action(new System.Action(() =>
        {
            if(isHandling) return;
            isHandling = true;

            float NewSanity = GetSanity();
            float Delta = NewSanity - lockedSanityValue;
            if(Delta > 0) {
                SanityManager.Instance.RemoveSanity(Delta);
            }
            else if (Delta < 0) {
                SanityManager.Instance.AddSanity(-Delta);
            }
            SanityManager.Instance.SanitySaveLoadData.SanityValue = lockedSanityValue;

            MelonLoader.MelonLogger.Msg($"[Sanity] Prevented the game from {(Delta > 0 ? "adding" : "removing")} {Delta} sanity!");

            isHandling = false;
        }));

        public static void AddSanity(float amount)
        {
            if (SanityManager.Instance != null)
            {
                SanityManager.Instance.AddSanity(amount);
            }
            else
            {
                MelonLoader.MelonLogger.Warning("[Sanity] Cannot add sanity, SanityManager.Instance is null!");
            }
        }

        public static void SubtractSanity(float amount)
        {
            if (SanityManager.Instance != null)
            {
                SanityManager.Instance.RemoveSanity(amount);
            }
            else
            {
                MelonLoader.MelonLogger.Warning("[Sanity] Cannot subtract sanity, SanityManager.Instance is null!");
            }
        }

        public static float GetSanity()
        {
            if (SanityManager.Instance != null)
            {
                return SanityManager.Instance.SanitySaveLoadData.SanityValue;
            }
            MelonLoader.MelonLogger.Warning("[Sanity] Cannot get sanity, SanityManager.Instance is null!");
            return 0;
        }

        public static void LockSanity(bool lockSanity)
        {
            if (SanityManager.Instance != null)
            {
                if(OriginalSanityHandler == null)
                {
                    OriginalSanityHandler = SanityManager.Instance.OnSanityChanged;
                    SanityManager.Instance.OnSanityChanged = SanityHandler;
                    lockedSanityValue = SanityManager.Instance.SanitySaveLoadData.SanityValue;
                }
                else
                {
                    SanityManager.Instance.OnSanityChanged = OriginalSanityHandler;
                    OriginalSanityHandler = null;
                    SanityManager.Instance.SanitySaveLoadData.SanityValue = -1;
                }
            }
            else
            {
                MelonLoader.MelonLogger.Warning("[Sanity] Cannot lock sanity, SanityManager.Instance is null!");
            }
        }

        public static bool IsSanityLocked()
        {
            return OriginalSanityHandler != null;
        }
    }
}
