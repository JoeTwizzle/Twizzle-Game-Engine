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
    public static class ConsoleInput
    {
        public static bool Running;
        private static MouseInputEvent mouse = new MouseInputEvent(0, 0, 0, 0, 0);
        private static RawInput input = new RawInput();

        private static bool[] OldKeyState = new bool[ushort.MaxValue];
        private static bool[] NewKeyState = new bool[ushort.MaxValue];
        private static KeyState[] keys = new KeyState[ushort.MaxValue];
        private static string keysPressed = "";

        public static void GetCharPressed(ref string s)
        {
            for (int i = 0; i < keysPressed.Length; i++)
            {

                switch (keysPressed[i])
                {
                    case (char)VirtualKeys.Back:
                        if (s.Length >= 1)
                        {
                            s = s.Remove(s.Length - 1, 1);
                        }
                        break;

                    default:
                        if (IsValidKey(keysPressed[i]) && keysPressed[i] != 0)
                        {
                            s += (char)keysPressed[i];//GetCharsFromKeys(keysPressed[i], false);
                        }
                        break;
                }
            }
            keysPressed = "";
        }

        static bool IsValidKey(char c)
        {
            return !(c == ((char)VirtualKeys.Shift) || c == ((char)VirtualKeys.Left) || c == ((char)VirtualKeys.Right)
                || c == ((char)VirtualKeys.Up) || c == ((char)VirtualKeys.Down) || c == ((char)VirtualKeys.CapsLock)
                || c == ((char)VirtualKeys.Return) || c == ((char)VirtualKeys.Tab) || c == ((char)VirtualKeys.Control)
                || c == ((char)VirtualKeys.Menu) || c == ((char)VirtualKeys.Escape) || c == ((char)VirtualKeys.Insert));
        }

        internal static void GetInput()
        {
            while (Running)
            {
                var inputEvent = input.MaybeGetInput();

                GetKBInput();
                if (inputEvent == null)
                {
                    continue;
                }
                switch (inputEvent.InputType)
                {
                    case InputEventType.Keyboard:
                        keysPressed += inputEvent.KeyboardInput.UnicodeChar;
                        break;
                    case InputEventType.Mouse:
                        mouse = inputEvent.MouseInput;
                        break;
                }
            }
        }

        public static MouseInputEvent GetMouse()
        {
            return mouse;
        }

        public static KeyState GetKey(char key, bool caseSensitive = false)
        {
            return (caseSensitive ? keys[key] : (keys[char.ToUpper(key)] | keys[char.ToLower(key)]));
        }

        internal static void GetKBInput()
        {
            for (int i = 0; i < NewKeyState.Length; i++)
            {
                NewKeyState[i] = false;
            }
            if (Game.ApplicationIsActivated())
            {
                for (int i = 0; i < 256; i++)
                {
                    NewKeyState[i] = NativeMethods.GetKeyState(i) < 0;
                    keys[i].Pressed = false;
                    keys[i].Released = false;

                    if (NewKeyState[i] != OldKeyState[i])
                    {
                        if (NewKeyState[i])
                        {
                            keys[i].Pressed = !keys[i].Held;
                            keys[i].Held = true;
                            keys[i].Released = false;
                        }
                        else
                        {
                            keys[i].Pressed = false;
                            keys[i].Released = true;
                            keys[i].Held = false;
                        }
                    }
                    OldKeyState[i] = NewKeyState[i];
                }
            }
        }
    }
}
