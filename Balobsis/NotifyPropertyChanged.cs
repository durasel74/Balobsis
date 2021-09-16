using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Balobsis
{
	public abstract class NotifyPropertyChanged : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}

