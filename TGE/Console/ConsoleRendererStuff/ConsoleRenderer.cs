using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace TGE
{
    public partial class ConsoleRenderer : Display
    {
        public int windowWidth { get; private set; }
        public int windowHeight { get; private set; }
        private SafeFileHandle STD_OUT_HND;
        public NativeMethods.CharInfo[] buf;
        private NativeMethods.SmallRect rect;

        public ConsoleRenderer(Display display) : base(display)
        {
            if (display.displayType != DisplayType.Console)
            {
                return;
            }
            Setup();
        }

        public ConsoleRenderer(string FontName, int Width, int Height, int PixelX, int PixelY) : 
            base(DisplayType.Console, FontName, Width, Height, PixelX, PixelY)
        {
            Setup();
        }

        void Setup()
        {
            windowWidth = Width;
            windowHeight = Height;
            STD_OUT_HND = NativeMethods.CreateFile("CONOUT$", NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE, 1 | 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            buf = new NativeMethods.CharInfo[Width * Height];
            rect = new NativeMethods.SmallRect();
            rect.SetDrawCord(0, 0);
            rect.SetWindowSize((short)windowWidth, (short)windowHeight);
            SetConsoleFont(FontName, PixelX, PixelY);
            Console.SetWindowSize(Width, Height);
            Console.SetBufferSize(Width , Height);
            Console.SetWindowSize(Width, Height);
        }

        public unsafe void SetConsoleFont(string FontName, int x, int y)
        {
            NativeMethods.CONSOLE_FONT_INFO_EX newInfo = new NativeMethods.CONSOLE_FONT_INFO_EX();
            newInfo.cbSize = (uint)Marshal.SizeOf(newInfo);
            newInfo.dwFontSize = new NativeMethods.Coord((short)x, (short)y);
            newInfo.nFont = 0;
            newInfo.FontWeight = 400;
            newInfo.FontFamily = 48;
            newInfo.FontWeight = 0;
            var ptr = new IntPtr(newInfo.FaceName);
            Marshal.Copy(FontName.ToCharArray(), 0, ptr, FontName.Length);
            NativeMethods.SetCurrentConsoleFontEx(STD_OUT_HND.DangerousGetHandle(), false, ref newInfo);
            var size = GetConsoleFontSize();
            if (size.X != x || size.Y != y)
            {
                throw new Exception("Failed to set Textsize to valid Value");
            }
        }

        private NativeMethods.Coord GetConsoleFontSize()
        {
            int errorCode = Marshal.GetLastWin32Error();
            if (STD_OUT_HND.IsInvalid)
            {
                throw new IOException("Unable to open CONOUT$", errorCode);
            }

            NativeMethods.ConsoleFontInfo cfi = new NativeMethods.ConsoleFontInfo();
            if (!NativeMethods.GetCurrentConsoleFont(STD_OUT_HND.DangerousGetHandle(), false, cfi))
            {
                throw new InvalidOperationException("Unable to get font information.");
            }

            return new NativeMethods.Coord(cfi.dwFontSize.X, cfi.dwFontSize.Y);
        }

    }
}
