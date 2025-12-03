using MelonLoader;
using UnityEngine;

namespace SunkenEngineStuffStuff
{
    using Il2Cpp;
    using SunkenEngineStuff;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public static class ModernUI
    {
        private static GUIStyle buttonStyle;
        private static GUIStyle buttonHoverStyle;
        private static GUIStyle headerStyle;
        private static GUIStyle windowStyle;
        private static GUIStyle sectionStyle;
        private static Texture2D buttonNormalTex;
        private static Texture2D buttonHoverTex;
        private static Texture2D windowTex;
        private static Texture2D sectionTex;

        public static void InitStyles()
        {
            if (buttonStyle != null) return;

            buttonNormalTex = MakeTexture(2, 2, new Color(0.2f, 0.2f, 0.25f, 0.95f));
            buttonHoverTex = MakeTexture(2, 2, new Color(0.3f, 0.35f, 0.45f, 0.95f));
            windowTex = MakeTexture(2, 2, new Color(0.12f, 0.12f, 0.15f, 0.98f));
            sectionTex = MakeTexture(2, 2, new Color(0.15f, 0.15f, 0.18f, 0.8f));

            buttonStyle = new GUIStyle(GUI.skin.box);
            buttonStyle.normal.background = buttonNormalTex;
            buttonStyle.normal.textColor = new Color(0.9f, 0.9f, 0.95f);
            buttonStyle.fontSize = 12;
            buttonStyle.fontStyle = FontStyle.Bold;
            buttonStyle.alignment = TextAnchor.MiddleCenter;
            buttonStyle.padding = new RectOffset(8, 8, 8, 8);
            buttonStyle.border = new RectOffset(4, 4, 4, 4);

            buttonHoverStyle = new GUIStyle(buttonStyle);
            buttonHoverStyle.normal.background = buttonHoverTex;
            buttonHoverStyle.normal.textColor = Color.white;

            headerStyle = new GUIStyle(GUI.skin.label);
            headerStyle.fontSize = 14;
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.normal.textColor = new Color(0.7f, 0.85f, 1f);
            headerStyle.alignment = TextAnchor.MiddleLeft;
            headerStyle.padding = new RectOffset(5, 0, 5, 5);

            windowStyle = new GUIStyle(GUI.skin.window);
            windowStyle.normal.background = windowTex;
            windowStyle.normal.textColor = new Color(0.8f, 0.9f, 1f);
            windowStyle.fontSize = 14;
            windowStyle.fontStyle = FontStyle.Bold;
            windowStyle.padding = new RectOffset(10, 10, 25, 10);
            windowStyle.border = new RectOffset(8, 8, 8, 8);

            sectionStyle = new GUIStyle(GUI.skin.box);
            sectionStyle.normal.background = sectionTex;
            sectionStyle.padding = new RectOffset(10, 10, 8, 8);
            sectionStyle.margin = new RectOffset(0, 0, 5, 5);
        }

        private static Texture2D MakeTexture(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++)
                pix[i] = col;

            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }

        public static void Button(Rect rect, string label, System.Action onClick)
        {
            InitStyles();

            bool isHovering = rect.Contains(Event.current.mousePosition);
            GUIStyle currentStyle = isHovering ? buttonHoverStyle : buttonStyle;

            GUI.Box(rect, label, currentStyle);

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

        public static void Header(Rect rect, string label)
        {
            InitStyles();
            GUI.Label(rect, label, headerStyle);
        }

        public static void Section(Rect rect)
        {
            InitStyles();
            GUI.Box(rect, "", sectionStyle);
        }

        public static GUIStyle GetWindowStyle()
        {
            InitStyles();
            return windowStyle;
        }
    }

    public class Menu
    {
        private Rect windowRect = new Rect(100, 100, 280, 600);
        private string windowTitle = "Mod Menu";

        private bool menuVisible = false;
        private bool wasCursorVisible = false;
        private CursorLockMode previousLockState;

        public void Draw()
        {
            if (!menuVisible)
                return;

            windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)DrawWindowContents, windowTitle, ModernUI.GetWindowStyle());
        }

        private void DrawWindowContents(int id)
        {
            float yPos = 10;
            float spacing = 8;
            float buttonHeight = 32;
            float buttonWidth = 240;
            float xOffset = 15;

            ModernUI.Section(new Rect(10, yPos, 260, 120));
            yPos += 5;
            ModernUI.Header(new Rect(xOffset, yPos, 200, 20), "Economy");
            yPos += 25;

            ModernUI.Button(new Rect(xOffset, yPos, buttonWidth, buttonHeight), "Add $1000", () =>
            {
                Money.AddCash(1000);
            });
            yPos += buttonHeight + spacing;

            ModernUI.Button(new Rect(xOffset, yPos, buttonWidth, buttonHeight), "Add Level", () =>
            {
                Level.AddLevels(1);
            });
            yPos += buttonHeight + spacing + 10;

            ModernUI.Section(new Rect(10, yPos, 260, 160));
            yPos += 5;
            ModernUI.Header(new Rect(xOffset, yPos, 200, 20), "Time");
            yPos += 25;

            ModernUI.Button(new Rect(xOffset, yPos, buttonWidth, buttonHeight), Time.IsTimeLocked ? "Unlock Time" : "Lock Time", () =>
            {
                Time.ToggleTimeLock();
            });
            yPos += buttonHeight + spacing;

            ModernUI.Button(new Rect(xOffset, yPos, buttonWidth, buttonHeight), "+ 1 Hour", () =>
            {
                Time.AddTime(1, 0, 0);
            });
            yPos += buttonHeight + spacing;

            ModernUI.Button(new Rect(xOffset, yPos, buttonWidth, buttonHeight), "- 1 Hour", () =>
            {
                Time.SubtractTime(1, 0, 0);
            });
            yPos += buttonHeight + spacing + 10;

            ModernUI.Section(new Rect(10, yPos, 260, 160));
            yPos += 5;
            ModernUI.Header(new Rect(xOffset, yPos, 200, 20), "Sanity");
            yPos += 25;

            ModernUI.Button(new Rect(xOffset, yPos, buttonWidth, buttonHeight), "+ 10 Sanity", () =>
            {
                Sanity.AddSanity(10);
            });
            yPos += buttonHeight + spacing;

            ModernUI.Button(new Rect(xOffset, yPos, buttonWidth, buttonHeight), "- 10 Sanity", () =>
            {
                Sanity.SubtractSanity(10);
            });
            yPos += buttonHeight + spacing;

            ModernUI.Button(new Rect(xOffset, yPos, buttonWidth, buttonHeight), Sanity.IsSanityLocked() ? "Unlock Sanity" : "Lock Sanity", () =>
            {
                Sanity.LockSanity(!Sanity.IsSanityLocked());
            });
            yPos += buttonHeight + spacing + 10;

            ModernUI.Section(new Rect(10, yPos, 260, 160));
            yPos += 5;
            ModernUI.Header(new Rect(xOffset, yPos, 200, 20), "Misc");
            yPos += 25;

            ModernUI.Button(new Rect(xOffset, yPos, buttonWidth, buttonHeight), Radio.IsRadioOn() ? "Radio OFF" : "Radio ON", () =>
            {
                Radio.ToggleRadio();
            });
            yPos += buttonHeight + spacing;

            ModernUI.Button(new Rect(xOffset, yPos, buttonWidth, buttonHeight), "Remove Ship", () =>
            {
                Ship.RemoveShip();
            });
            yPos += buttonHeight + spacing;

            ModernUI.Button(new Rect(xOffset, yPos, buttonWidth, buttonHeight), "Unlock All Licenses", () =>
            {
                License.UnlockAll();
            });

            GUI.DragWindow();
        }

        public void Keybinds()
        {
            if (Keyboard.current != null && Keyboard.current.f1Key.wasPressedThisFrame)
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