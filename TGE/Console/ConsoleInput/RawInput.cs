using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace TGE
{
    public class RawInput
    {
        private NativeMethods.ConsoleHandle _handler;
        public RawInput()
        {
            SetConsoleMode();
        }

        void SetConsoleMode()
        {
            _handler = NativeMethods.GetStdHandle(NativeMethods.STD_INPUT_HANDLE);

            int mode = 0;
            if (!(NativeMethods.GetConsoleMode(_handler, ref mode))) { throw new Win32Exception(); }

            mode |= NativeMethods.ENABLE_MOUSE_INPUT;
            mode &= ~NativeMethods.ENABLE_QUICK_EDIT_MODE;
            mode |= NativeMethods.ENABLE_EXTENDED_FLAGS;

            if (!(NativeMethods.SetConsoleMode(_handler, mode))) { throw new Win32Exception(); }
        }

        public InputEvent GetInput()
        {
            var record = new NativeMethods.INPUT_RECORD();
            uint recordLen = 0;
            InputEvent inputEvent;

            if (!(NativeMethods.ReadConsoleInput(_handler, ref record, 1, ref recordLen))) { throw new Win32Exception(); }

            CreateInputEvent(record, out inputEvent);
            return inputEvent;
        }
        public InputEvent MaybeGetInput()
        {
            UInt32 events = 0;
            NativeMethods.GetNumberOfConsoleInputEvents(_handler, out events);
            InputEvent inputEvent = null;

            if (events > 0)
            {
                var record = new NativeMethods.INPUT_RECORD();
                uint recordLen = 0;
                if (!(NativeMethods.ReadConsoleInput(_handler, ref record, 1, ref recordLen))) { throw new Win32Exception(); }

                CreateInputEvent(record, out inputEvent);
            }
            return inputEvent;
        }

        private void CreateInputEvent(NativeMethods.INPUT_RECORD record, out InputEvent inputEvent)
        {
            inputEvent = new InputEvent();

            switch (record.EventType)
            {
                case NativeMethods.MOUSE_EVENT:
                        inputEvent.InputType = InputEventType.Mouse;
                        inputEvent.MouseInput = new MouseInputEvent(
                                                    record.MouseEvent.dwMousePosition.X,
                                                    record.MouseEvent.dwMousePosition.Y,
                                                    record.MouseEvent.dwButtonState,
                                                    record.MouseEvent.dwControlKeyState,
                                                    record.MouseEvent.dwEventFlags
                                                );
                    break;

                case NativeMethods.KEY_EVENT:
                        inputEvent.InputType = InputEventType.Keyboard;
                        inputEvent.KeyboardInput = new KeyboardInputEvent(
                                                       record.KeyEvent.wVirtualKeyCode,
                                                       record.KeyEvent.UnicodeChar,
                                                       record.KeyEvent.dwControlKeyState,
                                                       record.KeyEvent.wRepeatCount,
                                                       record.KeyEvent.bKeyDown
                                                   );
                    break;
                default:
                    inputEvent = null;
                    break;
            }
        }



        // Thanks to Josip Medved for this blog post/wrapping of NativeMethods
        // https://www.jmedved.com/2013/05/console-mouse-input-in-c/
        private class NativeMethods
        {
            public const Int32 STD_INPUT_HANDLE = -10;

            public const Int32 ENABLE_MOUSE_INPUT = 0x0010;
            public const Int32 ENABLE_QUICK_EDIT_MODE = 0x0040;
            public const Int32 ENABLE_EXTENDED_FLAGS = 0x0080;

            public const Int32 KEY_EVENT = 1;
            public const Int32 MOUSE_EVENT = 2;


            [DebuggerDisplay("EventType: {EventType}")]
            [StructLayout(LayoutKind.Explicit)]
            public struct INPUT_RECORD
            {
                [FieldOffset(0)]
                public Int16 EventType;
                [FieldOffset(4)]
                public KEY_EVENT_RECORD KeyEvent;
                [FieldOffset(4)]
                public MOUSE_EVENT_RECORD MouseEvent;
            }

            [DebuggerDisplay("{dwMousePosition.X}, {dwMousePosition.Y}")]
            public struct MOUSE_EVENT_RECORD
            {
                public COORD dwMousePosition;
                public Int32 dwButtonState;
                public Int32 dwControlKeyState;
                public Int32 dwEventFlags;
            }

            [DebuggerDisplay("{X}, {Y}")]
            public struct COORD
            {
                public UInt16 X;
                public UInt16 Y;
            }

            [DebuggerDisplay("KeyCode: {wVirtualKeyCode}")]
            [StructLayout(LayoutKind.Explicit)]
            public struct KEY_EVENT_RECORD
            {
                [FieldOffset(0)]
                [MarshalAs(UnmanagedType.Bool)]
                public Boolean bKeyDown;
                [FieldOffset(4)]
                public UInt16 wRepeatCount;
                [FieldOffset(6)]
                public UInt16 wVirtualKeyCode;
                [FieldOffset(8)]
                public UInt16 wVirtualScanCode;
                [FieldOffset(10)]
                public Char UnicodeChar;
                [FieldOffset(10)]
                public Byte AsciiChar;
                [FieldOffset(12)]
                public Int32 dwControlKeyState;
            };

            public class ConsoleHandle : SafeHandleMinusOneIsInvalid
            {
                public ConsoleHandle() : base(false) { }

                protected override bool ReleaseHandle()
                {
                    return true; // Releasing console handle is not our business
                }
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern Boolean GetConsoleMode(ConsoleHandle hConsoleHandle, ref Int32 lpMode);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern ConsoleHandle GetStdHandle(Int32 nStdHandle);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern Boolean ReadConsoleInput(ConsoleHandle hConsoleInput, ref INPUT_RECORD lpBuffer, UInt32 nLength, ref UInt32 lpNumberOfEventsRead);

            //Currently unused
            //[DllImport("kernel32.dll", SetLastError = true)]
            //[return: MarshalAs(UnmanagedType.Bool)]
            //public static extern Boolean PeekConsoleInput(ConsoleHandle hConsoleInput, ref INPUT_RECORD lpBuffer, UInt32 nLength, ref UInt32 lpNumberOfEventsRead);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern Boolean SetConsoleMode(ConsoleHandle hConsoleHandle, Int32 dwMode);

            [DllImport("kernel32.dll")]
            public static extern Boolean GetNumberOfConsoleInputEvents(ConsoleHandle hConsoleInput, out UInt32 lpcNumberOfEvents);

        }
    }
}