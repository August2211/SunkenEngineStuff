using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SunkenEngineStuffStuff
{
    public class UIFix
    {
        [HarmonyPatch(typeof(UnityEngine.GUI), "DoControl")]
        public static class Patch_DoControl
        {
            public static void Postfix(ref bool __result, Event __state)
            {
                //if(__state.type != EventType.Layout && __state.type != EventType.Repaint)
                //MelonLogger.Msg($"DoControl Event Type: {__state.type}");

                if (__state != null && (__state.type == EventType.MouseDown || __state.type == EventType.MouseUp || __state.type == EventType.used))
                {
                    //MelonLogger.Msg("Preventing DoControl from consuming mouse event.");
                    __result = true;
                }
            }

            public static void Prefix(out Event __state)
            {
                __state = Event.current;
            }
        }
    }
}
