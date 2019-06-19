namespace TGE
{
    public class MouseInputEvent
    {
        public int X;
        public int Y;
        public int ButtonState;
        public int ControlKeyState;
        public int EventFlags;

        public MouseInputEvent(ushort x, ushort y, int buttonState, int controlKeyState, int eventFlags)
        {
            X = x;
            Y = y;
            ButtonState = buttonState;
            ControlKeyState = controlKeyState;
            EventFlags = eventFlags;
        }

        public bool IsLeftClick
        {
            get { return 1 << 0 == (ButtonState & 1 << 0); }
        }

        public bool IsRightClick
        {
            get { return 1 << 1 == (ButtonState & 1 << 1); }
        }

        public bool IsMiddleClick
        {
            get { return 1 << 2 == (ButtonState & 1 << 2); }
        }

        public bool IsShifted
        {
            get { return 1 == (ControlKeyState & 1 << 0); }
        }

        public bool IsCtrled
        {
            get { return 1 << 1 == (ControlKeyState & 1 << 1); }
        }

        public bool IsAlted
        {
            get { return 1 << 2 == (ControlKeyState & 1 << 2); }
        }

        public bool IsWinKeyed
        {
            get { return 1 << 3 == (ControlKeyState & 1 << 3); }
        }

        public bool IsDoubleClick
        {
            get { return 1 << 1 == (EventFlags & 1 << 1); }
        }

        public bool IsMouseDown
        {
            get { return 0 == EventFlags; }
        }

        public bool IsMouseMove
        {
            get { return 1 == (EventFlags & 1); }
        }

        public bool IsMouseWheelUp
        {
            get { return (1 << 2 == (EventFlags & 1 << 2)) && (ButtonState > 0); }
        }

        public bool IsMouseWheelDown
        {
            get { return (1 << 2 == (EventFlags & 1 << 2)) && (ButtonState < 0); }
        }

        public bool IsMouseWheelLeft
        {
            get { return (1 << 3 == (EventFlags & 1 << 3)) && (ButtonState < 0); }
        }

        public bool IsMouseWheelRight
        {
            get { return (1 << 3 == (EventFlags & 1 << 3)) && (ButtonState > 0); }
        }
    }
}