using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectedDevice;
using Lava.Devices;

namespace Lava
{
    class ButtonConnector
    {
        ConnectedDevice.ConnectedDevice connector;
        public event EventHandler<TRMArgs> OnTRMUpdated;
        public event EventHandler<GasAnalyzerArgs> OnGazAnalyzerPromZoneUpdated;
        public event EventHandler<GasAnalyzerArgs> OnGazAnalyzerLiveZoneUpdated;
        public event EventHandler<GasAnalyzerArgs> OnGazAnalyzerGazohranUpdated;
        public event EventHandler<BoxControllerArgs> OnBoxControllerUpdated;

        public List<BaseDevice> devices = new List<BaseDevice>() { new TRM(), new GasAnalyzer(), new GasAnalyzer(), new GasAnalyzer(), new BoxController() };

        public ButtonConnector(string addr, int port)
        {
            //ServerTCP.DataLoadEvent += GetData; //расскоментировать для теста с tcp эмулятором
            ServerTCP.StartRead();

            connector = new NetworkDeviceConn(addr, port, new ConnectedDevice.LoggerConsole());
            connector.Connect();

            var senderId = connector.SendPeriodically(new Command(12, new byte[] { 0, 1, 2, 3, 4 }));

            //device.Send(new Command(12, new byte[] { 4 }));

            connector.OnCommandReceived += Device_OnCommandReceived;
            connector.OnFailedCRC += onFailed;
        }

       /* public void GetData(byte[] data) /// расскоментировать для теста с tcp эмулятором
        {
            int i = 0;
            while (i < data.Length)
            {
                //Получает устройство, которому предназначается команда
                BaseDevice devise = devices[data[i]];

                //Массив для данных, полученных от устройства
                byte[] arrayForDevice = new byte[devise.bytesOnRead];

                //Собирает полученную команду для выбранного устройства
                Array.Copy(data, (i + 1), arrayForDevice, 0, devise.bytesOnRead);

                devise.Send(arrayForDevice);

                //Смещаем указатель на следующую команду
                i += (devise.bytesOnRead + 1);
            }
        }*/

        public void ConnectHandlers()
        {
            devices[0].OnUpdate += ButtonConnector_TRM_OnUpdate;
            devices[1].OnUpdate += ButtonConnector_GasAnalyzerPromZone_OnUpdate;
            devices[2].OnUpdate += ButtonConnector_GasAnalyzerLiveZone_OnUpdate;
            devices[3].OnUpdate += ButtonConnector_GasAnalyzerGazohran_OnUpdate;
            devices[4].OnUpdate += ButtonConnector_BoxController_OnUpdate;
        }

        private void ButtonConnector_TRM_OnUpdate(object sender, DeviceArgs.BaseArgs e)
        {
            var args = e as TRMArgs;
            OnTRMUpdated.Invoke(sender,args);
        }

        private void ButtonConnector_GasAnalyzerPromZone_OnUpdate(object sender, DeviceArgs.BaseArgs e)
        {
            var args = e as GasAnalyzerArgs;
            OnGazAnalyzerPromZoneUpdated.Invoke(sender, args);
        }

        private void ButtonConnector_GasAnalyzerLiveZone_OnUpdate(object sender, DeviceArgs.BaseArgs e)
        {
            var args = e as GasAnalyzerArgs;
            OnGazAnalyzerLiveZoneUpdated.Invoke(sender, args);
        }

        private void ButtonConnector_GasAnalyzerGazohran_OnUpdate(object sender, DeviceArgs.BaseArgs e)
        {
            var args = e as GasAnalyzerArgs;
            OnGazAnalyzerGazohranUpdated.Invoke(sender, args);
        }

        private void ButtonConnector_BoxController_OnUpdate(object sender, DeviceArgs.BaseArgs e)
        {
            var args = e as BoxControllerArgs;
            OnBoxControllerUpdated.Invoke(sender, args);
        }

        private void Device_OnCommandReceived(object sender, CommandEventArgs e)
        {
            int i = 0;
            while (i < e.receivedCommand.dataCommand.Length)
            {
                //Получает устройство, которому предназначается команда
                BaseDevice devise = devices[e.receivedCommand.dataCommand[i]];

                //Массив для данных, полученных от устройства
                byte[] arrayForDevice = new byte[devise.bytesOnRead];

                //Собирает полученную команду для выбранного устройства
                Array.Copy(e.receivedCommand.dataCommand, (i + 1), arrayForDevice, 0, devise.bytesOnRead);

                devise.Send(arrayForDevice);

                //Смещаем указатель на следующую команду
                i += (devise.bytesOnRead + 1);
            }
        }

        void onFailed(object sender, CommandEventArgs args)
        {
  
        }

        public void Send(params bool[] flags) {

            /*byte[] dataMassive = new byte[flags.Length]; //расскоментировать для теста с tcp эмулятором
            for (int i = 0; i < dataMassive.Length; i++)
            {
                dataMassive[i] = Convert.ToByte(flags[i]);
            }

            ServerTCP.SendData(dataMassive);*/

            var dataArray = new byte[flags.Length + 1];
            dataArray[0] = 4; // первым числом идет номер девайса, которому все отправляется (тут только один девайс)
            for (int i = 0; i < flags.Length; i++) {
                dataArray[i + 1] = Convert.ToByte(flags[i]);
            }

            connector.Send(new Command(14, dataArray)); // массив состояний горелок
        }
    }
}
