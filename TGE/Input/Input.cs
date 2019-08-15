using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TGE
{
    public struct KeyState
    {
        public bool Pressed;
        public bool Released;
        public bool Held;

        public static KeyState operator |(KeyState a, KeyState b)
        {
            return new KeyState() { Held = a.Held || b.Held, Pressed = a.Pressed || b.Pressed, Released = a.Released || b.Released };
        }

        public static KeyState operator &(KeyState a, KeyState b)
        {
            return new KeyState() { Held = a.Held && b.Held, Pressed = a.Pressed && b.Pressed, Released = a.Released && b.Released };
        }
    }
    public static class Input
    {
        static IntPtr HWnd = NativeMethods.GetConsoleWindow();
        public static bool Running;
        private static MouseState Mouse = new MouseState();
        public static KeyboardUpdate[] keyboardUpdate;
        private static readonly KeyState[] keys = new KeyState[Enum.GetNames(typeof(Key)).Length];

        public static Keyboard keyboard;
        public static Mouse mouse;

         [STAThread]
        internal static void Start()
        {
            RawInput raw = new RawInput();

            // Initialize DirectInput
            DirectInput directInput = new DirectInput();

            mouse = new Mouse(directInput);
            // Instantiate the joystick
            mouse.Properties.BufferSize = 128;
            mouse.Acquire();
            keyboard = new Keyboard(directInput);
            // Acquire the joystick
            keyboard.Properties.BufferSize = 128;
            keyboard.Acquire();
        }

        public static void Update()
        {
            for (int i = 0; i < Enum.GetNames(typeof(Key)).Length; i++)
            {
                 keys[i] = new KeyState() { Pressed = false, Held = keys[i].Held && !keys[i].Released, Released = false };
            }
            //var inputEvent = input.GetInput();
            keyboard.Poll();
            var kbData = keyboard.GetBufferedData();
            keyboardUpdate = kbData;
            foreach (var state in kbData)
            {
                ProcessKB(state);
            }
            
            mouse.Poll();
            var mstate = mouse.GetCurrentState();
            ProcessMouse(mstate);
        }
        static void ProcessKB(KeyboardUpdate keyboard)
        {
            for (int i = 0; i < Enum.GetNames(typeof(Key)).Length; i++)
            {
                if ((int)keyboard.Key == i)
                {
                    if (keyboard.IsPressed)
                    {
                        keys[i] = new KeyState() { Pressed = keyboard.IsPressed, Held = true, Released = keyboard.IsReleased };
                    }
                    else
                    {
                        keys[i] = new KeyState() { Pressed = keyboard.IsPressed, Held = keys[i].Held && !keyboard.IsReleased, Released = keyboard.IsReleased };
                    }
                }
            }
       
        }

        public static bool IsMouseWheelUp(this MouseState state)
        {
            return state.Z < 0;
        }
        public static bool IsMouseWheelDown(this MouseState state)
        {
            return state.Z > 0;
        }
        public static bool IsLeftClick(this MouseState state)
        {
            return state.Buttons[0];
        }
        public static bool IsRightClick(this MouseState state)
        {
            return state.Buttons[1];
        }
        public static bool IsMiddleClick(this MouseState state)
        {
            return state.Buttons[2];
        }
        public static bool IsFWDClick(this MouseState state)
        {
            return state.Buttons[3];
        }
        public static bool IsBWDClick(this MouseState state)
        {
            return state.Buttons[4];
        }
        public static bool IsSpecial1Click(this MouseState state)
        {
            return state.Buttons[5];
        }
        public static bool IsSpecial2Click(this MouseState state)
        {
            return state.Buttons[6];
        }
        static void ProcessMouse(MouseState state)
        {
            NativeMethods.GetCursorPos(out NativeMethods.POINT pos);
            NativeMethods.GetWindowRect(HWnd, out NativeMethods.RECT rect);
            NativeMethods.GetConsoleDisplayMode(out NativeMethods.ConsoleDisplayMode mode);
            Mouse = state;
            if (mode == NativeMethods.ConsoleDisplayMode.Fullscreen || mode == NativeMethods.ConsoleDisplayMode.FullscreenHardware)
            {
                Mouse.X = (pos.X - (rect.X + 0)) / Game.ActiveGame.Screen.PixelX;
                Mouse.Y = (pos.Y - (rect.Y + 0)) / Game.ActiveGame.Screen.PixelY;
            }
            else
            {
                Mouse.X = (pos.X - (rect.X + 8)) / Game.ActiveGame.Screen.PixelX;
                Mouse.Y = (pos.Y - (rect.Y + 30)) / Game.ActiveGame.Screen.PixelY;
            }
            Mouse.Z /= 120;
        }
        public static MouseState GetMouse()
        {
            return Mouse;
        }

        public static KeyState GetKey(Key key)
        {
            return keys[(int)key];
        }
    }
}
