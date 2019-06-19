using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGE
{
    public class Display : AbstractDisplay
    {
        public DisplayType displayType;
        public string FontName;
        public int Width;
        public int Height;
        public int PixelX;
        public int PixelY;

        public Display(Display display)
        {
            this.displayType = display.displayType;
            this.FontName = display.FontName;
            this.Width = display.Width;
            this.Height = display.Height;
            this.PixelX = display.PixelX;
            this.PixelY = display.PixelY;
        }

        public Display(DisplayType displayType, string FontName, int Width, int Height, int PixelX, int PixelY)
        {
            this.displayType = displayType;
            this.FontName = FontName;
            this.Width = Width;
            this.Height = Height;
            this.PixelX = PixelX;
            this.PixelY = PixelY;
        }


    }

    public enum DisplayType
    {
        None,
        Console,
        OpenGl
    }
}
