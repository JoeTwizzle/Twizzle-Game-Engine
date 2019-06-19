using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGE
{
    public partial class OpenGLRenderer : Display
    {
        public OpenGLRenderer(Display display) : base(display)
        {
            if (display.displayType != DisplayType.OpenGl)
            {
                return;
            }
            Setup();
        }
        public OpenGLRenderer(string FontName, int Width, int Height, int PixelX, int PixelY) :
            base(DisplayType.OpenGl, FontName, Width, Height, PixelX, PixelY)
        {
            Setup();
        }

        private void Setup()
        {
            throw new NotImplementedException();
        }
    }
}
