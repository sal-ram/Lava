using Lava.DeviceArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lava.Devices
{
    class TRM : BaseDevice
    {
        public TRM()
        {
            bytesOnRead = 32;
        }

        protected override void Convert(byte[] arrayOfData)
        {
            try
            {
                var LiveZoneTemp = BitConverter.ToSingle(arrayOfData, 16);      // Жилая зона. Температура
                var PromZoneTemp = BitConverter.ToSingle(arrayOfData, 20);      // Промышленная зона. Температура
                var GazohranZoneTemp = BitConverter.ToSingle(arrayOfData, 24);  // Газововое хранилище. Температура
                var SystemPressure = BitConverter.ToSingle(arrayOfData, 28); // Общее давление в системе
                args = new TRMArgs(GazohranZoneTemp, LiveZoneTemp, PromZoneTemp, SystemPressure);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public override void Send(byte[] arrayOfData)
        {
            Convert(arrayOfData);
            InvokeUpdate(this, args);
        }
    }
}
