using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGE
{
    public partial class OpenGLRenderer
    {
        public override void Clear()
        {
            base.Clear();
        }

        public override void clearColumn(int col)
        {
            base.clearColumn(col);
        }

        public override void clearRow(int row)
        {
            base.clearRow(row);
        }

        public override void Draw(char c, int X, int Y, short attribute)
        {
            base.Draw(c, X, Y, attribute);
        }

        public override void Draw(char[] str, int Width, int Height, short attribute)
        {
            base.Draw(str, Width, Height, attribute);
        }

        public override void Draw(string str, int Width, int Height, short attribute)
        {
            base.Draw(str, Width, Height, attribute);
        }

        public override void DrawCircle(char c, int xc, int yc, int r, short col)
        {
            base.DrawCircle(c, xc, yc, r, col);
        }

        public override void DrawLine(char c, int x1, int y1, int x2, int y2, short col)
        {
            base.DrawLine(c, x1, y1, x2, y2, col);
        }

        public override void DrawTriangle(char c, int x1, int y1, int x2, int y2, int x3, int y3, short col)
        {
            base.DrawTriangle(c, x1, y1, x2, y2, x3, y3, col);
        }

        public override void FillCircle(char c, int xc, int yc, int r, short col)
        {
            base.FillCircle(c, xc, yc, r, col);
        }

        public override void FillTriangle(char c, int x1, int y1, int x2, int y2, int x3, int y3, short col)
        {
            base.FillTriangle(c, x1, y1, x2, y2, x3, y3, col);
        }

        public override bool IsInBounds(int X, int Y)
        {
            return base.IsInBounds(X, Y);
        }

        public override void Print()
        {
            base.Print();
        }

        public override void Swap(ref int x, ref int y)
        {
            base.Swap(ref x, ref y);
        }
    }
}
