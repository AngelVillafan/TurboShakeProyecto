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

        //Commands
        public ICommand CorrerCommand { get; set; }

        public BatidoViewModel()
        {
            arduinoAddress = BluetoothAddress.Parse("XX:XX:XX:XX:XX:XX");
            bluetoothClient = new BluetoothClient();
            MostrandoProgreso = false;
            SendSignalCommand = new RelayCommand(SendSignal);
            CorrerCommand = new RelayCommand(Tiempo);
            Batido batido = new Batido();
            IsConnected = false;
            Adittivos = new List<string>();
            Adittivos.Add("Proteina");
            Adittivos.Add("Creattina");
            Adittivos.Add("Ambos");

        }

        #region bluetooth
        private BluetoothAddress arduinoAddress;
        private BluetoothClient bluetoothClient;
        public ICommand SendSignalCommand { get; set; }

        private bool isConnected;
        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

        private void SendSignal()
        {
            try
            {
                BluetoothDeviceInfo arduinoDevice = new BluetoothDeviceInfo(arduinoAddress);
                bluetoothClient.Connect(arduinoDevice.DeviceAddress, BluetoothService.SerialPort);

                System.IO.Stream outputStream = bluetoothClient.GetStream();

                byte[] signal = { (byte)'1' };
                outputStream.Write(signal, 0, signal.Length);

                bluetoothClient.Close();

                Error ="Señal enviada correctamente";

                IsConnected = true;

            }
            catch (Exception ex)
            {
                Error = "Error al enviar la señal: " + ex.Message;
                IsConnected = false;
            }
        }
        #endregion

        

        private void Tiempo()
        {
            try
            {
                SendSignal();
                MostrandoProgreso = true;
                Estado = "En proceso...";

                

                for (int i = 0; i <= 6; i++)
                {
                    Porcentaje = i / 6;
                    OnPropertyChanged();
                    Task.Delay(1000);
                }
                Estado = "Listo";
                Task.Delay(1000);
                MostrandoProgreso = false;
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
