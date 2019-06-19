using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGE
{
    public abstract class AbstractDisplay
    {
        public virtual void Draw(char c, int X, int Y, short attribute) { }
        public virtual void Draw(char[] str, int Width, int Height, short attribute) { }
        public virtual void Draw(string str, int Width, int Height, short attribute) { }
        public virtual void DrawRectangle(char c, int x, int y, int width, int height, short col) { }
        public virtual void FillRectangle(char c, int x, int y, int width, int height, short col) { }
        public virtual void DrawLine(char c, int x1, int y1, int x2, int y2, short col) { }
        public virtual void Swap(ref int x, ref int y) { }
        public virtual void DrawTriangle(char c, int x1, int y1, int x2, int y2, int x3, int y3, short col) { }
        public virtual void FillTriangle(char c, int x1, int y1, int x2, int y2, int x3, int y3, short col) { }
        public virtual void DrawCircle(char c, int xc, int yc, int r, short col) { }
        public virtual void FillCircle(char c, int xc, int yc, int r, short col) { }
        public virtual void Clear() { }
        public virtual void Print() { }
        public virtual void clearRow(int row) { }
        public virtual void clearColumn(int col) { }
        public virtual bool IsInBounds(int X, int Y) { return false; }
    }
}
