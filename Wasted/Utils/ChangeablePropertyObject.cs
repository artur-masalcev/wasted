using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Wasted.Utils
{
    /// <summary>
    /// Class that allows to notify that binded property is changed to invoke changes in UI
    /// </summary>
    public abstract class ChangeablePropertyObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
