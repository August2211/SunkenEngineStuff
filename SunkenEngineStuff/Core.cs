using MelonLoader;
using SunkenEngineStuff;
using UnityEngine;
using UnityEngine.InputSystem;

[assembly: MelonInfo(typeof(SunkenEngineStuffStuff.Core), "SunkenEngineStuffStuff", "1.1.0", "augus", null)]
[assembly: MelonGame("Two Nomads Studio", "Sunken Engine")]

namespace SunkenEngineStuffStuff
{
    public class Core : MelonMod
    {
        private Menu menu;

        public override void OnInitializeMelon()
        {
            menu = new Menu();

            MelonEvents.OnLateUpdate.Subscribe(menu.Keybinds);
            MelonEvents.OnUpdate.Subscribe(Fly.FlyMovementUpdate);
            HarmonyInstance.PatchAll();
        }

        public override void OnGUI()
        {
            menu?.Draw();
        }
    }
}