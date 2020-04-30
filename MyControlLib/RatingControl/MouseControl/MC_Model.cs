using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControlLib.RatingControl.MouseControl
{
    /// <summary>
    /// Mouse Control Model
    /// </summary>
    public class MC_Model
    {
        private double _SavedValue;
        private double _CurrentValue;


        private bool _CanSave = false;
        public bool CanSave
        {
            get { return _CanSave; }
            set
            {
                /// If user cliks without moving
                if (!_CanSave && value)
                    SaveValue();
                _CanSave = value;
            }
        }

        private bool _CanEdit = false;
        public bool CanEdit
        {
            get { return _CanEdit; }
            set
            {
                /// When user moves outside we go back and show saved value
                if (_CanEdit && !value)
                    ShowValue();
                _CanEdit = value;
            }
        }

        /// <summary>
        /// Current value
        /// </summary>
        public double CurrentValue
        {
            set { _CurrentValue = value; }
        }

        /// <summary>
        /// Saved value that stays after user finishes editing
        /// </summary>
        public double SavedValue
        {
            set { _SavedValue = value; }
        }

        public double Value
        {
            get
            {
                /// If user is still editing value
                /// return to display current value
                /// 
                if (CanEdit)
                    return _CurrentValue;
                else
                    return _SavedValue;
            }
            set
            {
                if (CanEdit)
                {
                    _CurrentValue = value;
                    if (CanSave)
                        SaveValue();
                }
                else
                {
                    _SavedValue = value;
                    ShowValue();
                }
            }
        }

        private void SaveValue()
        {
            _SavedValue = _CurrentValue;
        }
        private void ShowValue()
        {
            _CurrentValue = _SavedValue;
        }
    }
}
