using System.Windows;
using System.Collections.Generic;

namespace MyControlLib.RatingControl.RatingElement.Drawing_Type
{
    /// <summary>
    /// Enum - has only names for holding different patterns to show
    /// 
    /// !! Needs to be loaded from some database
    /// !!
    /// </summary>
    public enum RE_Type
    {
        Star = 0,
        Square = 1
    }

    /// <summary>
    /// Class for holding different patterns to show
    /// 
    /// </summary>
    public static class Dictionary
    {
        public static readonly Dictionary<RE_Type, Point[]> Data;

        static Dictionary()
        {
            Data = new Dictionary<RE_Type, Point[]>();

            /// Load from file
            Data.Add(RE_Type.Star, ParseFromString(RE_Type.Star));
            Data.Add(RE_Type.Square, ParseFromString(RE_Type.Square));
        }

        static Point[] ParseFromString(RE_Type val)
        {
            Point[] outp;
            switch (val)
            {
                case RE_Type.Star:
                    outp = new Point[8];
                    outp[0] = new Point(40, 0);
                    outp[1] = new Point(50, 30);
                    outp[2] = new Point(80, 40);
                    outp[3] = new Point(50, 50);
                    outp[4] = new Point(40, 80);
                    outp[5] = new Point(30, 50);
                    outp[6] = new Point(0, 40);
                    outp[7] = new Point(30, 30);
                    break;

                case RE_Type.Square:
                    outp = new Point[4];
                    outp[0] = new Point(10, 10);
                    outp[1] = new Point(70, 10);
                    outp[2] = new Point(70, 70);
                    outp[3] = new Point(10, 70);
                    break;

                default:
                    outp = new Point[0];
                    break;
            }


            return outp;
        }
    }
}
