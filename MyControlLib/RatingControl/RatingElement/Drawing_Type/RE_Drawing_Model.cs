using System.Windows;

namespace MyControlLib.RatingControl.RatingElement.Drawing_Type
{
    public class RE_Drawing_Model : Base.IRE_Base_Model
    {
        private double _Value;
        private Point[] _Data;

        private readonly RE_Type _RE_Type;


        public RE_Drawing_Model(RE_Type re_Type = RE_Type.Star)
        {
            _RE_Type = re_Type;
            Dictionary.Data.TryGetValue(_RE_Type, out _Data);
            Value = 0.0;
        }


        public double Value
        {
            get { return _Value; }
            set
            {
                _Value = (value > 0.5) ? 1.0 : 0;
            }
        }
        public object Data
        {
            get { return _Data; }
        }



        public Base.IRE_Base_Model GetCopy()
        {
            var outp = new RE_Drawing_Model(_RE_Type);
            outp.Value = _Value;

            return outp;
        }
    }
}
