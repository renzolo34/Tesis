using AppComedor.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppComedor.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private INavigation navigation;

        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }

        public async Task NavegarWelcome()
        {
            await navigation.PushAsync(new Welcome());
        }

        public ICommand NavegarCommand => new Command(async () => await NavegarWelcome());

        
    }


}