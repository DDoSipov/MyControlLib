using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Collections.ObjectModel;

using System.Windows;

namespace MyControlLib.RatingControl
{
    public abstract class VM_Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName = null)
        {
            var propertyChanged = PropertyChanged;
            propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
