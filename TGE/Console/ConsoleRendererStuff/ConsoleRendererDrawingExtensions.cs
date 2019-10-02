using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGE
{
    public static class ConsoleRendererExtensions
    {
        public static Tuple<char, short> getCharAt(ConsoleRenderer screen, int x, int y)
        {
            if (x >= screen.windowWidth || y >= screen.windowHeight || x < 0 || y < 0)
            {
                return null;
            }

            return new Tuple<char, short>(screen.buf[x + (y * screen.Width)].Char.UnicodeChar, screen.buf[x + (y * screen.Width)].Attributes);
        }

        public static void DrawConsoleSprite(ConsoleRenderer screen, ConsoleSprite sprite, int X, int Y)
        {
            if (sprite == null)
            {
                return;
            }
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
                    int sampleX = (int)System.Math.Round((x1 * (1f / ScaleX)));
                    int sampleY = (int)System.Math.Round((y1 * (1f / ScaleY)));
                    for (float y = 0; y < ScaleY; y += (1f / ScaleY))
                    {
                        for (float x = 0; x < ScaleX; x += (1f / ScaleX))
                        {
                            screen.Draw(sprite.GetChar(sampleX, sampleY), (int)System.Math.Round((x1 * ScaleX + x) + X), (int)System.Math.Round((y1 * ScaleY + y) + Y), sprite.GetColor(sampleX, sampleY));
                        }
                    }
                }
            }
        }
    }
}
