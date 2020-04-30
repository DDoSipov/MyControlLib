namespace MyControlLib.RatingControl.RatingElement.Drawing_Type
{
    public class RE_Drawing_VM : VM_Base, Base.IRE_Base_VM
    {
        private readonly RE_Drawing_Model _Model;

        public RE_Drawing_VM(RE_Drawing_Model model)
        {
            _Model = model;
        }

        public double Value
        {
            get { return _Model.Value; }
            set
            {
                _Model.Value = value;
                NotifyPropertyChanged("Value");
                NotifyPropertyChanged("Data");
            }
        }
        public object Data
        {
            get { return _Model.Data; }
        }
    }
}
