using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TGE
{
    [Serializable]
    public class Palette
    {
        public Color[] colors = new Color[16];

        public Palette()
        {
            for (int i = 0; i < 16; i++)
            {
                colors[i] = Color.FromArgb(0, 0, 0, 0);
            }
        }
        public static Palette Load(string FileName, DirectoryMode directoryMode)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            switch (directoryMode)
            {
                case DirectoryMode.Sprites:
                    {
                        if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Palettes\\"))
                        {
                            return null;
                        }
                        Stream stream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Palettes\\" + FileName + ".pal", FileMode.Open);
                        var sprite = (Palette)formatter.Deserialize(stream);
                        stream.Close();
                        stream.Dispose();
                        return sprite;
                    }
                case DirectoryMode.Absolute:
                    {
                        Stream stream = new FileStream(FileName, FileMode.Open);
                        var sprite = (Palette)formatter.Deserialize(stream);
                        stream.Close();
                        stream.Dispose();
                        return sprite;
                    }
                case DirectoryMode.Custom:
                    {
                        Stream stream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + FileName + ".pal", FileMode.Open);
                        var sprite = (Palette)formatter.Deserialize(stream);
                        stream.Close();
                        stream.Dispose();
                        return sprite;
                    }
                default:
                    return null;
            }
        }
        public void Save(string FileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Palettes\\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Palettes\\");
            }
            Stream stream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Palettes\\" + FileName + ".pal", FileMode.Create);
            formatter.Serialize(stream, this);
            stream.Close();
            stream.Dispose();
        }
        public void SetColor(int consoleColor, Color targetColor)
        {
            targetColor.A = 255;
            colors[consoleColor] = targetColor;
        }
    }
}
