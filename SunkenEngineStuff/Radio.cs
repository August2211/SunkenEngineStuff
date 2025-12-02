using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunkenEngineStuffStuff
{
    public static class Radio
    {
        public static void ToggleRadio()
        {
            if (MusicRadioInteract.Instance != null)
            {
                MusicRadioInteract.Instance.ToggleRadio();
            }
            else
            {
                MelonLoader.MelonLogger.Warning("[Radio] Cannot toggle radio, RadioManager.Instance is null!");
            }
        }

        public static bool IsRadioOn()
        {
            if (MusicRadioInteract.Instance != null)
            {
                return MusicRadioInteract.Instance.isRadioPlaying;
            }
            else
            {
                MelonLoader.MelonLogger.Warning("[Radio] Cannot get radio state, RadioManager.Instance is null!");
                return false;
            }
        }
    }
}
