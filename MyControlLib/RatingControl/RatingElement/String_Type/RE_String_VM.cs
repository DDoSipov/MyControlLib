﻿namespace MyControlLib.RatingControl.RatingElement.String_Type
{
    public class RE_String_VM : VM_Base, Base.IRE_Base_VM
    {
        private readonly RE_String_Model _Model;

        public RE_String_VM(RE_String_Model model)
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
