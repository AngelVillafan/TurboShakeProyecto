using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboShakeApp.ViewModels
{
    public class BatidoViewModel : INotifyPropertyChanged
    {










        void Actualizar(string palabra = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(palabra));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
