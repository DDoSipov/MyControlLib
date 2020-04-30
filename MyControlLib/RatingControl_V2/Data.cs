using System.Collections.Generic;
using System.Windows;
using System.ComponentModel;

namespace MyControlLib.RatingControl_V2
{
    public enum RE_Type
    {
        Star = 0,
        Square = 1,
        Star_5 = 2
    }
    public static class Dictionary
    {
        public static readonly Dictionary<RE_Type, Point[]> Data;

        static Dictionary()
        {
            Data = new Dictionary<RE_Type, Point[]>();

            /// Load from file
            Data.Add(RE_Type.Star, GetData(RE_Type.Star));
            Data.Add(RE_Type.Star_5, GetData(RE_Type.Star_5));
            Data.Add(RE_Type.Square, GetData(RE_Type.Square));
        }

        static Point[] GetData(RE_Type val)
        {
            Point[] outp;
            switch (val)
            {
                case RE_Type.Star:
                    outp = new Point[]
                    {
                        new Point(40, 0),
                        new Point(50, 30),
                        new Point(80, 40),
                        new Point(50, 50),
                        new Point(40, 80),
                        new Point(30, 50),
                        new Point(0, 40),
                        new Point(30, 30)
                    };
                    break;

                case RE_Type.Star_5:
                    outp = new Point[]
                    {
                        new Point(91, 0),
                        new Point(147, 174),
                        new Point(0, 66),
                        new Point(182, 66),
                        new Point(35, 174)
                    };
                    break;

                case RE_Type.Square:
                    outp = new Point[]
                    {
                        new Point(0, 0),
                        new Point(60, 0),
                        new Point(60, 60),
                        new Point(0, 60)
                    };
                    break;

                default:
                    outp = new Point[0];
                    break;
            }


            return outp;
        }
    }

    public class RE_VM : VM_Base
    {
        private double _Value = 0;
        public double Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                NotifyPropertyChanged();
            }
        }

        private RE_Type _Data = RE_Type.Star_5;
        public RE_Type Data
        {
            get { return _Data; }
            set
            {
                _Data = value;
                NotifyPropertyChanged();
            }
        }

        private bool _Debug = false;
        public bool Debug
        {
            get { return _Debug; }
            set
            {
                _Debug = value;
                NotifyPropertyChanged();
            }
        }
    }


    public abstract class VM_Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            var propertyChanged = PropertyChanged;
            propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}