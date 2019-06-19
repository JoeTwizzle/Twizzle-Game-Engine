using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;


namespace TGE
{
    public partial class ConsoleRenderer
    {

        #region Draw
        //Base Draw Method
        public override void Draw(char c, int X, int Y, short attribute)
        {
            if (c != 0)
            {
                if (!IsInBounds(X, Y))
                {
                    return;
                }
                if (c=='█')
                {
                    attribute |= (short)(attribute * 16);
                }
                //Height * width is to get to the correct spot (since this array is not two dimensions).
                buf[(X) + (Y * Width)].Char.UnicodeChar = c;
                buf[(X) + (Y * Width)].Attributes = attribute;
            }
        }

        public override void Draw(char[] str, int Width, int Height, short attribute)
        {
            if (str != null)
            {
                int tc = 0;
                foreach (char le in str)
                {
                    Draw(le, Width + tc, Height, attribute);
                    tc++;
                }
            }
        }

        public override void Draw(string str, int Width, int Height, short attribute)
        {
            if (str != null)
            {
                int tc = 0;
                foreach (char le in str)
                {
                    Draw(le, Width + tc, Height, attribute);
                    tc++;
                }
            }
        }
        public override void FillRectangle(char c, int x, int y, int width, int height, short col)
        {
            for (int y1 = 0; y1 < height; y1++)
            {
                for (int x1 = 0; x1 < width; x1++)
                {
                    Draw(c, x + x1, y + y1, col);
                }
            }
        }
        public override void DrawRectangle(char c, int x, int y, int width, int height, short col)
        {
            //horizontal
            DrawLine(c, x, y, x + width, y, col);
            DrawLine(c, x, y + height, x + width, y + height, col);
            //vertical
            DrawLine(c, x, y, x, y + height, col);
            DrawLine(c, x + width, y, x + width, y + height, col);
        }

        #endregion

        #region DrawLine

        public override void DrawLine(char c, int x1, int y1, int x2, int y2, short col)
        {
            int x, y, dx, dy, dx1, dy1, px, py, xe, ye, i;
            dx = x2 - x1; dy = y2 - y1;
            dx1 = System.Math.Abs(dx); dy1 = System.Math.Abs(dy);
            px = 2 * dy1 - dx1; py = 2 * dx1 - dy1;
            if (dy1 <= dx1)
            {
                if (dx >= 0)
                { x = x1; y = y1; xe = x2; }
                else
                { x = x2; y = y2; xe = x1; }

                Draw((char)c, x, y, col);

                for (i = 0; x < xe; i++)
                {
                    x = x + 1;
                    if (px < 0)
                        px = px + 2 * dy1;
                    else
                    {
                        if ((dx < 0 && dy < 0) || (dx > 0 && dy > 0)) y = y + 1; else y = y - 1;
                        px = px + 2 * (dy1 - dx1);
                    }
                    Draw((char)c, x, y, col);
                }
            }
            else
            {
                if (dy >= 0)
                { x = x1; y = y1; ye = y2; }
                else
                { x = x2; y = y2; ye = y1; }

                Draw((char)c, x, y, col);

                for (i = 0; y < ye; i++)
                {
                    y = y + 1;
                    if (py <= 0)
                        py = py + 2 * dx1;
                    else
                    {
                        if ((dx < 0 && dy < 0) || (dx > 0 && dy > 0)) x = x + 1; else x = x - 1;
                        py = py + 2 * (dx1 - dy1);
                    }
                    Draw((char)c, x, y, col);
                }
            }
        }

        void drawline(char c, int sx, int ex, int ny, short col)
        {
            for (int i = sx; i <= ex; i++)
            {
                Draw(c, i, ny, col);
            }
        }

        #endregion

        #region DrawTriangles

        public override void Swap(ref int x, ref int y)
        {
            int t = x; x = y; y = t;
        }

        public override void DrawTriangle(char c, int x1, int y1, int x2, int y2, int x3, int y3, short col)
        {
            DrawLine(c, x1, y1, x2, y2, col);
            DrawLine(c, x2, y2, x3, y3, col);
            DrawLine(c, x3, y3, x1, y1, col);
        }

        public override void FillTriangle(char c, int x1, int y1, int x2, int y2, int x3, int y3, short col)
        {
            // = [&](int sx, int ex, int ny) { for (int i = sx; i <= ex; i++) Draw(i, ny, c, col); };

            int t1x, t2x, y, minx, maxx, t1xp, t2xp;
            bool changed1 = false;
            bool changed2 = false;
            int signx1, signx2, dx1, dy1, dx2, dy2;
            int e1, e2;
            // Sort vertices
            if (y1 > y2) { Swap(ref y1, ref y2); Swap(ref x1, ref x2); }
            if (y1 > y3) { Swap(ref y1, ref y3); Swap(ref x1, ref x3); }
            if (y2 > y3) { Swap(ref y2, ref y3); Swap(ref x2, ref x3); }

            t1x = t2x = x1; y = y1;   // Starting points
            dx1 = (int)(x2 - x1); if (dx1 < 0) { dx1 = -dx1; signx1 = -1; }
            else signx1 = 1;
            dy1 = (int)(y2 - y1);

            dx2 = (int)(x3 - x1); if (dx2 < 0) { dx2 = -dx2; signx2 = -1; }
            else signx2 = 1;
            dy2 = (int)(y3 - y1);

            if (dy1 > dx1)
            {   // swap values
                Swap(ref dx1, ref dy1);
                changed1 = true;
            }
            if (dy2 > dx2)
            {   // swap values
                Swap(ref dy2, ref dx2);
                changed2 = true;
            }

            e2 = (int)(dx2 >> 1);
            // Flat top, just process the second half
            if (y1 == y2) goto next;
            e1 = (int)(dx1 >> 1);

            for (int i = 0; i < dx1;)
            {
                t1xp = 0; t2xp = 0;
                if (t1x < t2x) { minx = t1x; maxx = t2x; }
                else { minx = t2x; maxx = t1x; }
                // process first line until y value is about to change
                while (i < dx1)
                {
                    i++;
                    e1 += dy1;
                    while (e1 >= dx1)
                    {
                        e1 -= dx1;
                        if (changed1) t1xp = signx1;//t1x += signx1;
                        else goto next1;
                    }
                    if (changed1) break;
                    else t1x += signx1;
                }
            // Move line
            next1:
                // process second line until y value is about to change
                while (true)
                {
                    e2 += dy2;
                    while (e2 >= dx2)
                    {
                        e2 -= dx2;
                        if (changed2) t2xp = signx2;//t2x += signx2;
                        else goto next2;
                    }
                    if (changed2) break;
                    else t2x += signx2;
                }
            next2:
                if (minx > t1x) minx = t1x; if (minx > t2x) minx = t2x;
                if (maxx < t1x) maxx = t1x; if (maxx < t2x) maxx = t2x;
                drawline(c, minx, maxx, y, col);    // Draw line from min to max points found on the y
                                                    // Now increase y
                if (!changed1) t1x += signx1;
                t1x += t1xp;
                if (!changed2) t2x += signx2;
                t2x += t2xp;
                y += 1;
                if (y == y2) break;

            }
        next:
            // Second half
            dx1 = (int)(x3 - x2); if (dx1 < 0) { dx1 = -dx1; signx1 = -1; }
            else signx1 = 1;
            dy1 = (int)(y3 - y2);
            t1x = x2;

            if (dy1 > dx1)
            {   // swap values
                Swap(ref dy1, ref dx1);
                changed1 = true;
            }
            else changed1 = false;

            e1 = (int)(dx1 >> 1);

            for (int i = 0; i <= dx1; i++)
            {
                t1xp = 0; t2xp = 0;
                if (t1x < t2x) { minx = t1x; maxx = t2x; }
                else { minx = t2x; maxx = t1x; }
                // process first line until y value is about to change
                while (i < dx1)
                {
                    e1 += dy1;
                    while (e1 >= dx1)
                    {
                        e1 -= dx1;
                        if (changed1) { t1xp = signx1; break; }//t1x += signx1;
                        else goto next3;
                    }
                    if (changed1) break;
                    else t1x += signx1;
                    if (i < dx1) i++;
                }
            next3:
                // process second line until y value is about to change
                while (t2x != x3)
                {
                    e2 += dy2;
                    while (e2 >= dx2)
                    {
                        e2 -= dx2;
                        if (changed2) t2xp = signx2;
                        else goto next4;
                    }
                    if (changed2) break;
                    else t2x += signx2;
                }
            next4:

                if (minx > t1x) minx = t1x; if (minx > t2x) minx = t2x;
                if (maxx < t1x) maxx = t1x; if (maxx < t2x) maxx = t2x;
                drawline(c, minx, maxx, y, col);
                if (!changed1) t1x += signx1;
                t1x += t1xp;
                if (!changed2) t2x += signx2;
                t2x += t2xp;
                y += 1;
                if (y > y3) return;
            }
        }

        #endregion

        #region DrawCircle

        public override void DrawCircle(char c,int xc, int yc, int r, short col)
        {
            int x = 0;
            int y = r;
            int p = 3 - 2 * r;
            if (r == 0) return;

            while (y >= x) // only formulate 1/8 of circle
            {
                Draw(c,xc - x, yc - y, col);//upper left left
                Draw(c,xc - y, yc - x, col);//upper upper left
                Draw(c,xc + y, yc - x, col);//upper upper right
                Draw(c,xc + x, yc - y, col);//upper right right
                Draw(c,xc - x, yc + y, col);//lower left left
                Draw(c,xc - y, yc + x, col);//lower lower left
                Draw(c,xc + y, yc + x, col);//lower lower right
                Draw(c,xc + x, yc + y, col);//lower right right
                if (p < 0) p += 4 * x++ + 6;
                else p += 4 * (x++ - y--) + 10;
            }
        }

        public override void FillCircle(char c, int xc, int yc, int r, short col)
        {
            // Taken from wikipedia
            int x = 0;
            int y = r;
            int p = 3 - 2 * r;
            if (r == 0) return;


            while (y >= x)
            {
                // Modified to draw scan-lines instead of edges
                drawline(c,xc - x, xc + x, yc - y,col);
                drawline(c,xc - y, xc + y, yc - x,col);
                drawline(c,xc - x, xc + x, yc + y,col);
                drawline(c,xc - y, xc + y, yc + x,col);
                if (p < 0) p += 4 * x++ + 6;
                else p += 4 * (x++ - y--) + 10;
            }
        }
        #endregion

        #region Clear
        public override void Clear()
        {
            Parallel.For(0,buf.Length, i =>
            {
                buf[i].Attributes = 0;
                buf[i].Char.UnicodeChar = ' ';
            });
        }

        public override void clearRow(int row)
        {
            for (int i = (row * Width); i < ((row * Width + Width)); i++)
            {
                if (row > windowHeight - 1)
                {
                    throw new ArgumentOutOfRangeException();
                }
                buf[i].Attributes = 0;
                buf[i].Char.UnicodeChar = ' ';
            }
        }

        public override void clearColumn(int col)
        {
            if (col > windowWidth - 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = col; i < windowHeight * windowWidth; i += windowWidth)
            {
                buf[i].Attributes = 0;
                buf[i].Char.UnicodeChar = ' ';
            }
        }
        #endregion

        #region Misc
        public override void Print()
        {
            if (!STD_OUT_HND.IsInvalid)
            {
                bool b = NativeMethods.WriteConsoleOutput(STD_OUT_HND, buf, new NativeMethods.Coord((short)Width, (short)Height), new NativeMethods.Coord((short)0, (short)0), ref rect);
            }
        }

        public override bool IsInBounds(int X, int Y)
        {
            if (X < Width && Y < Height && Y >= 0 && X >= 0)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
