using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace TGE
{
    public class Game
    {
        #region Fields
        public Display Screen { get; private set; }
        public bool Running { get; private set; }
        public float DeltaTime { get; private set; }
        private float tp1, tp2;
        private Stopwatch stopwatch = new Stopwatch();
        #endregion

        public static bool ApplicationIsActivated()
        {
            var activatedHandle = NativeMethods.GetForegroundWindow();
            if (activatedHandle == IntPtr.Zero)
            {
                return false;       // No window is currently activated
            }

            var procId = Process.GetCurrentProcess().Id;
            int activeProcId;
            NativeMethods.GetWindowThreadProcessId(activatedHandle, out activeProcId);

            return activeProcId == procId;
        }

        void DummyInput()
        {
            return;
        }

        delegate void InputType();
        public void Run(Display display)
        {
            InputType b = new InputType(DummyInput);
            switch (display.displayType)
            {
                case DisplayType.Console:
                    Screen = new ConsoleRenderer(display);
                    ConsoleInput.Running = true;
                    b = ConsoleInput.GetInput;
                    break;

                case DisplayType.None:
                    Screen = display;
                    break;

                default:
                    break;
            }
            
            Thread InputThread = new Thread(new ThreadStart(b));
            InputThread.Name = "InputThread";
            InputThread.Priority = ThreadPriority.AboveNormal;
            InputThread.SetApartmentState(ApartmentState.STA);
            InputThread.Start();

            Running = true;
            Thread GameThread = new Thread(new ThreadStart(GameLoop));
            GameThread.Name = "GameThread";
            GameThread.Priority = ThreadPriority.AboveNormal;
            GameThread.Start();
            
        }

        public void Close()
        {
            Running = false;
        }

        void Time()
        {
            if (!stopwatch.IsRunning)
            {
                stopwatch.Start();
            }
            tp2 = stopwatch.ElapsedMilliseconds / 1000f;
            DeltaTime = tp2 - tp1;
            tp1 = tp2;
        }

        void GameLoop()
        {
            Time();
            Initialize();
            Start();
            while (Running)
            {
                Time();
                Update();
                LateUpdate();
                Draw();
            }
        }

        public virtual void Initialize() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void Draw() { }
    }
}
