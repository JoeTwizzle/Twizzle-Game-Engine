namespace TGE
{
    public enum InputEventType
    {
        None,
        Mouse,
        Keyboard
    }

    public class InputEvent
    {
        public InputEventType InputType { get; set; }
        public MouseInputEvent MouseInput { get; set; }
        public KeyboardInputEvent KeyboardInput { get; set; }

        public InputEvent()
        {
            InputType = InputEventType.None;
        }
    }
}

