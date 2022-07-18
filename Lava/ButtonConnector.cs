using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectedDevice;

namespace Lava
{
    class ButtonConnector
    {
        ConnectedDevice.ConnectedDevice device;
        public event EventHandler<TRMArgs> OnTRMUpdated;
        public event EventHandler<GasAnalyzerArgs> OnGazAnalyzerPromZoneUpdated;
        public event EventHandler<GasAnalyzerArgs> OnGazAnalyzerLiveZoneUpdated;
        public event EventHandler<GasAnalyzerArgs> OnGazAnalyzerGazohranUpdated;
        public event EventHandler<BoxControllerArgs> OnBoxControllerUpdated;
        public ButtonConnector(string addr, int port) 
        {
            device = new NetworkDeviceConn(addr, port, new ConnectedDevice.LoggerConsole());
            device.Connect();

            //var senderId = device.SendPeriodically(new Command(12,new byte[] {0}));

            device.Send(new Command(12, new byte[] {4}));

            device.OnCommandReceived += Device_OnCommandReceived;
            device.OnFailedCRC += onFailed;

            //device.StopSendingPeriodically(senderId);
        }

        private void Device_OnCommandReceived(object sender, CommandEventArgs e)
        {
            var cmd = e.receivedCommand;

            var deviceId = cmd.dataCommand.Take(1).ToArray(); // id устройства

            switch (deviceId[0])
            {

                case 0:
                    OnTRMUpdated.Invoke(this, ConvertTRMValues(cmd.dataCommand));
                    break;

                case 1:
                    OnGazAnalyzerPromZoneUpdated.Invoke(this, ConvertGasAnalizerValues(cmd.dataCommand)); // пром зона. Показатель газоанализатора
                    break;

                case 2:
                    OnGazAnalyzerLiveZoneUpdated.Invoke(this, ConvertGasAnalizerValues(cmd.dataCommand)); // жил зона. Показатель газоанализатора
                    break;

                case 3:
                    OnGazAnalyzerGazohranUpdated.Invoke(this, ConvertGasAnalizerValues(cmd.dataCommand)); // хранилище газа. Показатель газоанализатора
                    break;

                case 4:
                    OnBoxControllerUpdated.Invoke(this, ConvertBoxValues(cmd.dataCommand));
                    break;
            }
        }

        private GasAnalyzerArgs ConvertGasAnalizerValues(byte[] data)
        {
            List<byte[]> valuesBytesGasAnalizer = new List<byte[]>();
            valuesBytesGasAnalizer.Add(data.Skip(1).Take(2).ToArray()); // Показатель газоанализатора
            valuesBytesGasAnalizer.Add(data.Skip(3).Take(2).ToArray()); // Состояние газоанализатора

            var GasAnalyzerPercent = BitConverter.ToInt16(valuesBytesGasAnalizer[0], 0);
            var GasAnalyzeState = BitConverter.ToInt16(valuesBytesGasAnalizer[1], 0);

            return new GasAnalyzerArgs(GasAnalyzerPercent, GasAnalyzeState);
        }

        private TRMArgs ConvertTRMValues(byte[] data)
        {
            List<byte[]> valuesBytesTRM = new List<byte[]>();
            valuesBytesTRM.Add(data.Skip(17).Take(4).ToArray()); // Жилая зона. Температура
            valuesBytesTRM.Add(data.Skip(21).Take(4).ToArray()); // Промышленная зона. Температура
            valuesBytesTRM.Add(data.Skip(25).Take(4).ToArray()); // Газововое хранилище. Температура

            var LiveZoneTemp = BitConverter.ToSingle(valuesBytesTRM[0], 0);
            var PromZoneTemp = BitConverter.ToSingle(valuesBytesTRM[1], 0);
            var GazohranZoneTemp = BitConverter.ToSingle(valuesBytesTRM[2], 0);

            return new TRMArgs(GazohranZoneTemp, LiveZoneTemp, PromZoneTemp);
        }

        private BoxControllerArgs ConvertBoxValues(byte[] data)
        {
            List<byte[]> valuesBytesBoxController = new List<byte[]>();

            for (int i = 0; i < data.Length / 2 - 1; i++)
            {
                valuesBytesBoxController.Add(data.Skip(2 * i + 1).Take(2).ToArray());
            }

            valuesBytesBoxController.Add(data.Skip(data.Length - 2).Take(1).ToArray());
            valuesBytesBoxController.Add(data.Skip(data.Length - 1).Take(1).ToArray());

            return new BoxControllerArgs(valuesBytesBoxController.Take(7).ToList(), valuesBytesBoxController.Skip(7).Take(1).ToList(), valuesBytesBoxController.Skip(8).Take(1).ToList());
        }

        void onFailed(object sender, CommandEventArgs args)
        {
  
        }
    }
}
