using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGE
{
    public static class ConsoleRendererExtensions
    {
        public static KeyValuePair<char, byte> getCharAt(ConsoleRenderer screen, int x, int y)
        {
            if (x > screen.windowWidth)
            {
                x = screen.windowWidth;
            }
            if (y > screen.windowHeight)
            {
                y = screen.windowHeight;
            }
            return new KeyValuePair<char, byte>(screen.buf[((y * screen.Width + x))].Char.UnicodeChar, (byte)screen.buf[((y * screen.Width + x))].Attributes);
        }

        public static void DrawConsoleSprite(ConsoleRenderer screen, ConsoleSprite sprite, int X, int Y)
        {
            Parallel.For(0, sprite.Height, y1 =>
            {
                Parallel.For(0, sprite.Width, x1 =>
                {
                    screen.Draw(sprite.GetChar(x1, y1), x1 + X, y1 + Y, sprite.GetColor(x1, y1));
                });
            });
        }
        public static void DrawConsoleSprite(ConsoleRenderer screen, ConsoleSprite sprite, int X, int Y, float ScaleX, float ScaleY)
        {
            for (int y1 = 0; y1 < (int)(sprite.Height * ScaleY - 1); y1++)
            {
                for (int x1 = 0; x1 < (int)(sprite.Width * ScaleX - 1); x1++)
                {
                    int sampleX = (int)(x1 * (1f / ScaleX));
                    int sampleY = (int)(y1 * (1f / ScaleY));
                    for (float y = 0; y < ScaleY; y += (1f / ScaleY))
                    {
                        for (float x = 0; x < ScaleX; x += (1f / ScaleX))
                        {
                            screen.Draw(sprite.GetChar(sampleX, sampleY), (int)(x1 * ScaleX + x) + X, (int)(y1 * ScaleY + y) + Y, sprite.GetColor(sampleX, sampleY));
                        }
                    }
                }
            }
        }
    }
}
