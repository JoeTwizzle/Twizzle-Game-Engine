using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGE.Math
{
    public struct Vector2
    {
        public float x, y;

        #region Properties
        static Vector2 zeroVector = new Vector2(0f, 0f);
        static Vector2 unitVector = new Vector2(1f, 1f);
        static Vector2 unitXVector = new Vector2(1f, 0f);
        static Vector2 unitYVector = new Vector2(0f, 1f);

        public static Vector2 Zero { get { return zeroVector; } }
        public static Vector2 One { get { return unitVector; } }
        public static Vector2 UnitX { get { return unitXVector; } }
        public static Vector2 UnitY { get { return unitYVector; } }
        public float SqrMagnitude { get { return x * x + y * y; } }
        public float Magnitude { get { return (float)System.Math.Sqrt(x * x + y * y); } }
        public Vector2 Normalized { get { float val = 1.0f / (float)System.Math.Sqrt((x * x) + (y * y)); return new Vector2(x * val, y * val); } }
        #endregion

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            return (a - b).Magnitude;
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return (a.x * b.x) + (a.y * b.y);
        }

        #region Operators
        public static Vector2 operator -(Vector2 value)
        {
            value.x = -value.x;
            value.y = -value.y;
            return value;
        }

        public static Vector2 operator +(Vector2 value1, Vector2 value2)
        {
            value1.x += value2.x;
            value1.y += value2.y;
            return value1;
        }


        public static Vector2 operator -(Vector2 value1, Vector2 value2)
        {
            value1.x -= value2.x;
            value1.y -= value2.y;
            return value1;
        }


        public static Vector2 operator *(Vector2 value1, Vector2 value2)
        {
            value1.x *= value2.x;
            value1.y *= value2.y;
            return value1;
        }


        public static Vector2 operator *(Vector2 value, float scaleFactor)
        {
            value.x *= scaleFactor;
            value.y *= scaleFactor;
            return value;
        }


        public static Vector2 operator *(float scaleFactor, Vector2 value)
        {
            value.x *= scaleFactor;
            value.y *= scaleFactor;
            return value;
        }


        public static Vector2 operator /(Vector2 value1, Vector2 value2)
        {
            value1.x /= value2.x;
            value1.y /= value2.y;
            return value1;
        }


        public static Vector2 operator /(Vector2 value1, float divider)
        {
            float factor = 1 / divider;
            value1.x *= factor;
            value1.y *= factor;
            return value1;
        }
        #endregion
    }
}
