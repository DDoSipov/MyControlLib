namespace MyControlLib.RatingControl.RatingElement.String_Type
{
    public class RE_String_Model : Base.IRE_Base_Model
    {
        private double _Value;
        private string _Data;


        public RE_String_Model()
        {
            Value = 0.0;
        }


        public double Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                _Data = _Value.ToString();
            }
        }
        public object Data
        {
            get { return _Data; }
        }


        public Base.IRE_Base_Model GetCopy()
        {
            RE_String_Model outp = new RE_String_Model();
            outp._Value = _Value;
            return outp;
        }
    }
}
