using System;

namespace TGE
{
    public class KeyboardInputEvent
    {
        public KeyboardInputEvent(ushort virtualKeyCode, char unicodeChar, int controlkeyState, ushort repeatCount,
            bool keyDown)
        {
            VirtualKeyCode = virtualKeyCode;
            UnicodeChar = unicodeChar;
            ControlKeyState = controlkeyState;
            RepeatCount = repeatCount;
            KeyDown = keyDown;
        }

        public bool KeyDown { get; set; }
        public int RepeatCount { get; set; }
        public int VirtualKeyCode { get; set; }
        public char UnicodeChar { get; set; }
        public int ControlKeyState { get; set; }

        public ConsoleKey ConsoleKey
        {
            get { return (ConsoleKey) VirtualKeyCode; }
        }

        public ConsoleKeyInfo ConsoleKeyInfo
        {
            get { return new ConsoleKeyInfo(UnicodeChar, ConsoleKey, IsShifted, IsAlted, IsCtrled); }
        }

        public bool IsAlted
        {
            get { return (1 << 1 == (ControlKeyState & 1 << 1)) || (1 << 0 == (ControlKeyState & 1 << 0)); }
        }

        public bool IsCtrled
        {
            get { return 1 << 3 == (ControlKeyState & 1 << 3) || (1 << 2 == (ControlKeyState & 1 << 2)); }
        }

        public bool IsShifted
        {
            get { return 1 << 4 == (ControlKeyState & 1 << 4); }
        }

        public bool IsWinKeyed
        {
            get { return 1 << 8 == (ControlKeyState & 1 << 8); }
        }
    }
}