using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lava.Devices
{
    class GasAnalyzer : BaseDevice
    {

        public GasAnalyzer()
        {
            bytesOnRead = 4;
        }
        protected override void Convert(byte[] arrayOfData)
        {
            //List<byte[]> valuesBytesGasAnalizer = new List<byte[]>();
            //valuesBytesGasAnalizer.Add(arrayOfData.Take(2).ToArray()); // Показатель газоанализатора
            //valuesBytesGasAnalizer.Add(arrayOfData.Skip(2).Take(2).ToArray()); // Состояние газоанализатора

            var GasAnalyzerPercent = BitConverter.ToInt16(arrayOfData, 0); // Показатель газоанализатора
            var GasAnalyzeState = BitConverter.ToInt16(arrayOfData, 2); // Состояние газоанализатора

            args =  new GasAnalyzerArgs(GasAnalyzerPercent, GasAnalyzeState);
        }

        public override void Send(byte[] arrayOfData)
        {
            Convert(arrayOfData);
            InvokeUpdate(this, args);
        }
    }
}
