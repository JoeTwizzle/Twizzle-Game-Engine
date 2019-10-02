using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TGE
{
    public enum DirectoryMode
    {
        Sprites,
        Absolute,
        Custom
    }
    [Serializable]
    public class ConsoleSprite
    {
        public int Width;
        public int Height;
        char[,] chars;
        short[,] colors;
        public ConsoleSprite(int Width, int Height)
        {
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Sprites\\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Sprites\\");
            }
            this.Width = Width;
            this.Height = Height;
            colors = new short[Width, Height];
            chars = new char[Width, Height];
        }
        public bool SetData(char[,] chars, short[,] colors)
        {
            if (chars == null || colors == null)
            {
                return false;
            }
            int crWidth = chars.GetLength(0);
            int crHeight = chars.GetLength(1);
            int cWidth = colors.GetLength(0);
            int cHeight = colors.GetLength(1);
            if (crWidth != cWidth || crHeight != cHeight)
            {
                return false;
            }

            this.Width = crWidth;
            this.Height = crHeight;
            this.chars = chars;
            this.colors = colors;
            return true;
        }
        public static ConsoleSprite Load(string FileName, DirectoryMode directoryMode)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            switch (directoryMode)
            {
                case DirectoryMode.Sprites:
                    {
                        if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Sprites\\"))
                        {
                            return null;
                        }
                        Stream stream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Sprites\\" + FileName + ".cpr", FileMode.Open);
                        var sprite = (ConsoleSprite)formatter.Deserialize(stream);
                        stream.Close();
                        stream.Dispose();
                        return sprite;
                    }
                case DirectoryMode.Absolute:
                    {
                        Stream stream = new FileStream(FileName, FileMode.Open);
                        var sprite = (ConsoleSprite)formatter.Deserialize(stream);
                        stream.Close();
                        stream.Dispose();
                        return sprite;
                    }
                case DirectoryMode.Custom:
                    {
                        Stream stream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + FileName + ".cpr", FileMode.Open);
                        var sprite = (ConsoleSprite)formatter.Deserialize(stream);
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
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Sprites\\"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Sprites\\");
            }
            Stream stream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\Sprites\\" + FileName + ".cpr", FileMode.Create);
            formatter.Serialize(stream, this);
            stream.Close();
            stream.Dispose();
        }
        public void SetColor(int x, int y, short color)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                colors[x, y] = color;
            }
        }
        public void SetChar(int x, int y, char character)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                chars[x, y] = character;
            }
        }
        public short GetColor(int x, int y)
        {
            return colors[x, y];
        }
        public char GetChar(int x, int y)
        {
            return chars[x, y];
        }
    }
}
