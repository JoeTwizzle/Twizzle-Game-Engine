using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace TGE
{
    [Serializable]
    public struct Color
    {
        public Color(IntPtr SourcePixel)
        {
            this = (Color)Marshal.PtrToStructure(SourcePixel, typeof(Color));
        }
        public Color(Color c)
        {
            this.A = c.A;
            this.R = c.R;
            this.G = c.G;
            this.B = c.B;
        }
        public static Color FromArgb(byte A, byte R, byte G, byte B)
        {
            return new Color { R = R, G = G, B = B, A = A };
        }
        public static Color FromArgb(byte R, byte G, byte B)
        {
            return new Color { R = R, G = G, B = B, A = 255 };
        }
        public byte A;
        public byte R;
        public byte G;
        public byte B;
        public override string ToString()
        {
            return "A: " + A + "R: " + R + "G: " + G + "B: " + B;
        }
    }
    public static class ColorChanger
    {

        public static void ResetColors()
        {
            SetStandardPalette();
            SetPalette(StandardColors);
        }
        public static void SetPalette(Palette palette)
        {
            SetStandardPalette();
            for (int i = 0; i < 16 && i < palette.colors.Length; i++)
            {
                if (palette.colors[i].A != 0)
                {
                    SetColor(i, palette.colors[i]);
                }
            }
        }
        public static Palette StandardColors;
        static void SetStandardPalette()
        {
            if (StandardColors != null)
            {
                return;
            }
            NativeMethods.CONSOLE_SCREEN_BUFFER_INFO_EX csbe = new NativeMethods.CONSOLE_SCREEN_BUFFER_INFO_EX();
            csbe.cbSize = (int)Marshal.SizeOf(csbe);
            IntPtr hConsoleOutput = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE).DangerousGetHandle();
            NativeMethods.GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
            Color[] colors = new Color[16];
            colors[0] = csbe.black.GetColor();
            colors[1] = csbe.darkBlue.GetColor();
            colors[2] = csbe.darkGreen.GetColor();
            colors[3] = csbe.darkCyan.GetColor();
            colors[4] = csbe.darkRed.GetColor();
            colors[5] = csbe.darkMagenta.GetColor();
            colors[6] = csbe.darkYellow.GetColor();
            colors[7] = csbe.gray.GetColor();
            colors[8] = csbe.darkGray.GetColor();
            colors[9] = csbe.blue.GetColor();
            colors[10] = csbe.green.GetColor();
            colors[11] = csbe.cyan.GetColor();
            colors[12] = csbe.red.GetColor();
            colors[13] = csbe.magenta.GetColor();
            colors[14] = csbe.yellow.GetColor();
            colors[15] = csbe.white.GetColor();
            StandardColors = new Palette();
            StandardColors.colors = colors;
        }

        public static Palette GetPalette()
        {
            SetStandardPalette();
            NativeMethods.CONSOLE_SCREEN_BUFFER_INFO_EX csbe = new NativeMethods.CONSOLE_SCREEN_BUFFER_INFO_EX();
            csbe.cbSize = (int)Marshal.SizeOf(csbe);
            IntPtr hConsoleOutput = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE).DangerousGetHandle();
            NativeMethods.GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
            Color[] colors = new Color[16];
            colors[0] = csbe.black.GetColor();
            colors[1] = csbe.darkBlue.GetColor();
            colors[2] = csbe.darkGreen.GetColor();
            colors[3] = csbe.darkCyan.GetColor();
            colors[4] = csbe.darkRed.GetColor();
            colors[5] = csbe.darkMagenta.GetColor();
            colors[6] = csbe.darkYellow.GetColor();
            colors[7] = csbe.gray.GetColor();
            colors[8] = csbe.darkGray.GetColor();
            colors[9] = csbe.blue.GetColor();
            colors[10] = csbe.green.GetColor();
            colors[11] = csbe.cyan.GetColor();
            colors[12] = csbe.red.GetColor();
            colors[13] = csbe.magenta.GetColor();
            colors[14] = csbe.yellow.GetColor();
            colors[15] = csbe.white.GetColor();
            Palette palette = new Palette();
            palette.colors = colors;
            return palette;
        }
        public static Color GetColor(int color)
        {
            SetStandardPalette();
            NativeMethods.CONSOLE_SCREEN_BUFFER_INFO_EX csbe = new NativeMethods.CONSOLE_SCREEN_BUFFER_INFO_EX();
            csbe.cbSize = (int)Marshal.SizeOf(csbe);
            IntPtr hConsoleOutput = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE).DangerousGetHandle();
            NativeMethods.GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
            switch (color)
            {
                case 0:
                    return csbe.black.GetColor();
                case 1:
                    return csbe.darkBlue.GetColor();
                case 2:
                    return csbe.darkGreen.GetColor();
                case 3:
                    return csbe.darkCyan.GetColor();
                case 4:
                    return csbe.darkRed.GetColor();
                case 5:
                    return csbe.darkMagenta.GetColor();
                case 6:
                    return csbe.darkYellow.GetColor();
                case 7:
                    return csbe.gray.GetColor();
                case 8:
                    return csbe.darkGray.GetColor();
                case 9:
                    return csbe.blue.GetColor();
                case 10:
                    return csbe.green.GetColor();
                case 11:
                    return csbe.cyan.GetColor();
                case 12:
                    return csbe.red.GetColor();
                case 13:
                    return csbe.magenta.GetColor();
                case 14:
                    return csbe.yellow.GetColor();
                case 15:
                    return csbe.white.GetColor();
                default:
                    throw new Exception("Not a valid Color");
            }
        }

        public static Color[] GetColors()
        {
            SetStandardPalette();
            Color[] colors = new Color[16];
            NativeMethods.CONSOLE_SCREEN_BUFFER_INFO_EX csbe = new NativeMethods.CONSOLE_SCREEN_BUFFER_INFO_EX();
            csbe.cbSize = (int)Marshal.SizeOf(csbe);
            IntPtr hConsoleOutput = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE).DangerousGetHandle();
            NativeMethods.GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
            colors[0] = csbe.black.GetColor();
            colors[1] = csbe.darkBlue.GetColor();
            colors[2] = csbe.darkGreen.GetColor();
            colors[3] = csbe.darkCyan.GetColor();
            colors[4] = csbe.darkRed.GetColor();
            colors[5] = csbe.darkMagenta.GetColor();
            colors[6] = csbe.darkYellow.GetColor();
            colors[7] = csbe.gray.GetColor();
            colors[8] = csbe.darkGray.GetColor();
            colors[9] = csbe.blue.GetColor();
            colors[10] = csbe.green.GetColor();
            colors[11] = csbe.cyan.GetColor();
            colors[12] = csbe.red.GetColor();
            colors[13] = csbe.magenta.GetColor();
            colors[14] = csbe.yellow.GetColor();
            colors[15] = csbe.white.GetColor();
            return colors;
        }

        public static int SetColor(ConsoleColor color, uint r, uint g, uint b)
        {
            SetStandardPalette();
            return SetColor((int)color, r, g, b);
        }
        public static int SetColor(int consoleColor, Color targetColor)
        {
            SetStandardPalette();
            return SetColor(consoleColor, targetColor.R, targetColor.G, targetColor.B);
        }
        public static int SetColor(int color, uint r, uint g, uint b)
        {
            SetStandardPalette();
            NativeMethods.CONSOLE_SCREEN_BUFFER_INFO_EX csbe = new NativeMethods.CONSOLE_SCREEN_BUFFER_INFO_EX();
            csbe.cbSize = (int)Marshal.SizeOf(csbe);
            IntPtr hConsoleOutput = NativeMethods.GetStdHandle(NativeMethods.STD_OUTPUT_HANDLE).DangerousGetHandle();
            if (hConsoleOutput == NativeMethods.INVALID_HANDLE_VALUE)
            {
                return Marshal.GetLastWin32Error();
            }
            bool brc = NativeMethods.GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
            if (!brc)
            {
                return Marshal.GetLastWin32Error();
            }
            switch (color)
            {
                case 0:
                    csbe.black = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 1:
                    csbe.darkBlue = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 2:
                    csbe.darkGreen = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 3:
                    csbe.darkCyan = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 4:
                    csbe.darkRed = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 5:
                    csbe.darkMagenta = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 6:
                    csbe.darkYellow = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 7:
                    csbe.gray = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 8:
                    csbe.darkGray = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 9:
                    csbe.blue = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 10:
                    csbe.green = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 11:
                    csbe.cyan = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 12:
                    csbe.red = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 13:
                    csbe.magenta = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 14:
                    csbe.yellow = new NativeMethods.COLORREF(r, g, b);
                    break;
                case 15:
                    csbe.white = new NativeMethods.COLORREF(r, g, b);
                    break;
                default:
                    throw new Exception("Not a valid Color");
            }
            ++csbe.srWindow.Bottom;
            ++csbe.srWindow.Right;
            brc = NativeMethods.SetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
            if (!brc)
            {
                return Marshal.GetLastWin32Error();
            }
            return 0;
        }
        public static int SetColor(ConsoleColor consoleColor, Color targetColor)
        {
            SetStandardPalette();
            return SetColor(consoleColor, targetColor.R, targetColor.G, targetColor.B);
        }

        public static int SetScreenColors(Color foregroundColor, Color backgroundColor)
        {
            SetStandardPalette();
            int irc;
            irc = SetColor(ConsoleColor.Gray, foregroundColor);
            if (irc != 0) return irc;
            irc = SetColor(ConsoleColor.Black, backgroundColor);
            if (irc != 0) return irc;

            return 0;
        }
    }
}