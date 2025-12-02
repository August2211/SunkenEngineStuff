using MelonLoader;
using UnityEngine;

namespace SunkenEngineStuffStuff
{
    using Il2Cpp;
    using SunkenEngineStuff;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public static class LazyButtonFix
    {
        public static void Button(Rect rect, string label, System.Action onClick)
        {
            GUI.Box(rect, label);

            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 0)
            {
                if (rect.Contains(e.mousePosition))
                {
                    onClick?.Invoke();
                    e.Use();
                }
            }
        }
    }


    public class Menu
    {
        private Rect windowRect = new Rect(100, 100, 220, 1000);
        private string windowTitle = "Mode Manu";

        private bool menuVisible = false;
        private bool wasCursorVisible = false;
        private CursorLockMode previousLockState;

        public void Draw()
        {
            if (!menuVisible)
                return;

            windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)DrawWindowContents, windowTitle);
        }

        private void DrawWindowContents(int id)
        {
            LazyButtonFix.Button(new Rect(20, 30, 160, 30), "Add $1000", () =>
            {
                Money.AddCash(1000);
            });

            LazyButtonFix.Button(new Rect(20, 70, 160, 30), "Add 1 level", () =>
            {
                Level.AddLevels(1);
            });

            LazyButtonFix.Button(new Rect(20, 110, 160, 30), Time.IsTimeLocked ? "Unlock Time" : "Lock Time", () =>
            {
                Time.ToggleTimeLock();
            });

            LazyButtonFix.Button(new Rect(20, 150, 160, 30), "Add 1 Hour", () =>
            {
                Time.AddTime(1, 0, 0);
            });

            LazyButtonFix.Button(new Rect(20, 190, 160, 30), "Subtract 1 Hour", () =>
            {
                Time.SubtractTime(1, 0, 0);
            });

            LazyButtonFix.Button(new Rect(20, 230, 160, 30), "Add 10 Sanity", () =>
            {
                Sanity.AddSanity(10);
            });

            LazyButtonFix.Button(new Rect(20, 270, 160, 30), "Subtract 10 Sanity", () =>
            {
                Sanity.SubtractSanity(10);
            });

            LazyButtonFix.Button(new Rect(20, 310, 160, 30), Sanity.IsSanityLocked() ? "Unlock Sanity" : "Lock Sanity", () =>
            {
                Sanity.LockSanity(!Sanity.IsSanityLocked());
            });

            LazyButtonFix.Button(new Rect(20, 350, 160, 30), Radio.IsRadioOn() ? "Turn Radio Off" : "Turn Radio On", () =>
            {
                Radio.ToggleRadio();
            });

            LazyButtonFix.Button(new Rect(20, 390, 160, 30), "Remove Ship", () =>
            {
                Ship.RemoveShip();
            });

            LazyButtonFix.Button(new Rect(20, 430, 160, 30), "Unlock All Licenses", () =>
            {
                License.UnlockAll();
            });


            GUI.DragWindow();
        }

        public void Keybinds()
        {
            if (Keyboard.current != null && Keyboard.current.insertKey.wasPressedThisFrame)
            {
                menuVisible = !menuVisible;

                if (menuVisible)
                {
                    wasCursorVisible = Cursor.visible;
                    previousLockState = Cursor.lockState;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.visible = wasCursorVisible;
                    Cursor.lockState = previousLockState;
                }
            }
            if(Keyboard.current != null && Keyboard.current.vKey.wasPressedThisFrame)
            {
                Fly.ToggleFly();
            }
        }
    }
}
