using GalaSoft.MvvmLight.Command;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TurboShakeApp.Models;

namespace TurboShakeApp.ViewModels
{
    public class BatidoViewModel : INotifyPropertyChanged
    {
        public int Porcentaje { get; set; }
        public string Error { get; set; }
        public bool MostrandoProgreso { get; set; }
        public List<string> Adittivos { get; set; }
        public string Aditivo { get; set; }
        public string MensajeProteina { get; set; }
        public string MensajeCreatina { get; set; }
        public string Estado { get; set; }
        public Batido Batido { get; set; }
        public string MensajeNombre { get; set; }

        //Commands
        public ICommand CorrerCommand { get; set; }

        public BatidoViewModel()
        {
            //arduinoAddress = BluetoothAddress.Parse("XX:XX:XX:XX:XX:XX");
            //bluetoothClient = new BluetoothClient();
            MostrandoProgreso = false;
            //SendSignalCommand = new RelayCommand(SendSignal);
            CorrerCommand = new RelayCommand(Tiempo);
            Batido = new Batido();
            //IsConnected = false;
            Adittivos = new List<string>();
            Adittivos.Add("Proteina");
            Adittivos.Add("Creattina");
            Adittivos.Add("Ambos");

        }

        //#region bluetooth
        //private BluetoothAddress arduinoAddress;
        //private BluetoothClient bluetoothClient;
        //public ICommand SendSignalCommand { get; set; }

        //private bool isConnected;
        //public bool IsConnected
        //{
        //    get { return isConnected; }
        //    set
        //    {
        //        isConnected = value;
        //        OnPropertyChanged(nameof(IsConnected));
        //    }
        //}

        //private void SendSignal()
        //{
        //    try
        //    {
        //        BluetoothDeviceInfo arduinoDevice = new BluetoothDeviceInfo(arduinoAddress);
        //        bluetoothClient.Connect(arduinoDevice.DeviceAddress, BluetoothService.SerialPort);

        //        System.IO.Stream outputStream = bluetoothClient.GetStream();

        //        byte[] signal = { (byte)'1' };
        //        outputStream.Write(signal, 0, signal.Length);

        //        bluetoothClient.Close();

        //        Error ="Señal enviada correctamente";

        //        IsConnected = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        Error = "Error al enviar la señal: " + ex.Message;
        //        IsConnected = false;
        //    }
        //}
        //#endregion

        

        private void Tiempo()
        {
            try
            {
                //SendSignal();
                if (string.IsNullOrWhiteSpace(Batido.Nombre))
                {
                    Error = "Por favor ingrese su nombre";
                }
                if (byte.Parse(Batido.Edad) < 18 || byte.Parse(Batido.Edad) > 80)
                {
                    Error = "No tienes la edad requerida para consumir este batido.";
                }
                if (string.IsNullOrWhiteSpace(Aditivo))
                {
                    Error = "Por favor seleccione su aditivo";
                }
                if (Aditivo == "Ambos")
                {
                    MensajeCreatina = (0.07 * int.Parse(Batido.Peso)).ToString() + " Gr.";
                    MensajeProteina = (1.2 * int.Parse(Batido.Peso)).ToString() + " Gr.";
                }
                else if (Aditivo == "Proteina")
                {
                    MensajeCreatina = "0 Gr.";
                    MensajeProteina = (1.2 * int.Parse(Batido.Peso)).ToString() + " Gr.";
                }
                else
                {
                    MensajeCreatina = (0.04 * int.Parse(Batido.Peso)).ToString() + " Gr.";
                    MensajeProteina = "0 Gr.";
                }
                OnPropertyChanged();
                MostrandoProgreso = true;
                Estado = "En proceso...";
                MensajeNombre = Batido.Nombre + ", estamos preparando tu batido";
                
                
                OnPropertyChanged();
                for (int i = 0; i <= 6; i++)
                {
                    Porcentaje = i / 6;
                    OnPropertyChanged();
                    Task.Delay(1000);
                    OnPropertyChanged();
                }
                Estado = "Listo";
                Task.Delay(1000);
                MostrandoProgreso = false;
                OnPropertyChanged();
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
           
        }

        void OnPropertyChanged(string palabra = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(palabra));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
