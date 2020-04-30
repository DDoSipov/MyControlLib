using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyControlLib.RatingControl.RatingCollection
{
    /// <summary>
    /// Rating Control ViewModel
    /// </summary>
    public class RC_VM : VM_Base
    {
        private readonly RC_Model _RC_Model;
        private readonly ObservableCollection<RatingElement.RE_VM> _RE_VMs;

        public RC_VM(RC_Model RC_Model)
        {
            _RC_Model = RC_Model;
            _RE_VMs = new ObservableCollection<RatingElement.RE_VM>();
            RE_VMs = new ReadOnlyObservableCollection<RatingElement.RE_VM>(_RE_VMs);


            foreach (var m in RC_Model.RE_Models)
            {
                _RE_VMs.Add(new RatingElement.RE_VM(m));
            }
        }
        

        public double Value
        {
            get { return _RC_Model.Value; }
            set
            {
                _RC_Model.Value = value;
                Update_VMs();
                NotifyPropertyChanged("Value");
            }
        }
        public ReadOnlyObservableCollection<RatingElement.RE_VM> RE_VMs { get; }



        private void Update_VMs()
        {
            for (int i = 0; i < _RC_Model.RE_Models.Count; i++)
                RE_VMs.ElementAt(i).Value = _RC_Model.RE_Models.ElementAt(i).Value;
        }
    }
}
