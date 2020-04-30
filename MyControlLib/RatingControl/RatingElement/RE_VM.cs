using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControlLib.RatingControl.RatingElement
{
    public class RE_Model
    {
        private readonly Base.IRE_Base_Model _Content_Model;


        public RE_Model(Base.IRE_Base_Model content_Model)
        {
            _Content_Model = content_Model;
        }


        public Base.IRE_Base_Model Content_Model { get { return _Content_Model; } }

        public double Value
        {
            get { return _Content_Model.Value; }
            set { _Content_Model.Value = value; }
        }
    }



    public class RE_VM : VM_Base
    {
        private readonly RE_Model _RE_Model;
        private readonly Base.IRE_Base_VM _Content_VM;

        public RE_VM(RE_Model re_Model)
        {
            _RE_Model = re_Model;
            _Content_VM = VM_Selector.Find(re_Model.Content_Model);
        }

        public Base.IRE_Base_VM Content
        {
            get { return _Content_VM; }
        }

        public double Value
        {
            get { return _RE_Model.Value; }
            set
            {
                _RE_Model.Value = value;
                _Content_VM.Value = _RE_Model.Value;
                NotifyPropertyChanged("Value");
            }
        }
    }

    public static class VM_Selector
    {
        public static Base.IRE_Base_VM Find(Base.IRE_Base_Model model)
        {
            switch (model)
            {
                case Drawing_Type.RE_Drawing_Model m:
                    return new Drawing_Type.RE_Drawing_VM(m);
                case String_Type.RE_String_Model m:
                    return new String_Type.RE_String_VM(m);
                default:
                    throw new NotImplementedException("Unknown_VM");
            }
        }
    }
}
