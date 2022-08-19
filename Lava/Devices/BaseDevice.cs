using Lava.DeviceArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lava.Devices
{
    abstract class BaseDevice
    {
        public int bytesOnRead { get; protected set; }

        public BaseArgs args { get; protected set; }

        public event EventHandler<BaseArgs> OnUpdate;

        protected void InvokeUpdate(BaseDevice sender, BaseArgs args) {
            OnUpdate.Invoke(sender, args);
        }

        protected abstract void Convert(byte[] arrayOfData);
        public abstract void Send(byte[] arrayOfData);
    }
}
