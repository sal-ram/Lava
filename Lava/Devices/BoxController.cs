using Lava.DeviceArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lava.Devices
{
    class BoxController : BaseDevice
    {
        public BoxController()
        {
            bytesOnRead = 17;
        }
        protected override void Convert(byte[] arrayOfdata)
        {
            List<byte[]> valuesBytesBoxController = new List<byte[]>();

            for (int i = 0; i < arrayOfdata.Length / 2 - 1; i++)
            {
                valuesBytesBoxController.Add(arrayOfdata.Skip(2 * i + 0).Take(2).ToArray());
            }

            valuesBytesBoxController.Add(arrayOfdata.Skip(arrayOfdata.Length - 3).Take(1).ToArray());
            valuesBytesBoxController.Add(arrayOfdata.Skip(arrayOfdata.Length - 2).Take(1).ToArray());
            valuesBytesBoxController.Add(arrayOfdata.Skip(arrayOfdata.Length - 1).Take(1).ToArray());

            args = new BoxControllerArgs(valuesBytesBoxController.Take(7).ToList(), valuesBytesBoxController.Skip(7).Take(1).ToList(), valuesBytesBoxController.Skip(8).Take(1).ToList(), valuesBytesBoxController.Skip(9).Take(1).ToList());
        }

        public override void Send(byte[] arrayOfData)
        {
            Convert(arrayOfData);
            InvokeUpdate(this, args);
        }
    }
}
