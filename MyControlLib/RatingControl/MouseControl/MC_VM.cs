using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyControlLib.RatingControl.MouseControl
{
    /// <summary>
    /// Mouse Control ViewModel
    /// </summary>
    public class MC_VM : VM_Base
    {
        private readonly double _NormValue;
        private readonly MC_Model MCM;
        public MC_VM(double normValue)
        {
            MCM = new MC_Model();
            _NormValue = normValue;
        }


        private RatingCollection.RC_VM _RC_VM;
        public RatingCollection.RC_VM RC_VM
        {
            get { return _RC_VM; }
            set
            {
                _RC_VM = value;
                NotifyPropertyChanged("RC_VM");
            }
        }


        private bool _Debug = true;
        public bool Debug
        {
            get { return _Debug; }
            set
            {
                _Debug = value;
                NotifyPropertyChanged("Debug");
            }
        }


        public bool CanSave
        {
            get { return MCM.CanSave; }
            set
            {
                MCM.CanSave = value;
                NotifyPropertyChanged("CanSave");
            }
        }

        public bool CanEdit
        {
            get { return MCM.CanEdit; }
            set
            {
                MCM.CanEdit = value;
                NotifyPropertyChanged("CanEdit");
            }
        }

        /// <summary>
        /// Current value (edited by user with mouse)
        /// </summary>
        public double EditValue
        {
            set
            {
                MCM.CurrentValue = value;
                _RC_VM.Value = MCM.Value;
                NotifyPropertyChanged("Value");
            }
        }

        /// <summary>
        /// Value for editing from outside
        /// </summary>
        public double Value
        {
            get { return MCM.Value * _NormValue; }
            set
            {
                MCM.Value = value / _NormValue;
                _RC_VM.Value = MCM.Value;
                NotifyPropertyChanged("Value");
            }
        }


        public void StartEdit()
        {
            CanEdit = true;
        }
        public void AllowSave()
        {
            CanSave = true;
        }
        public void FinishEdit()
        {
            CanEdit = false;
            CanSave = false;

            Value = Value;
        }
    }
}
