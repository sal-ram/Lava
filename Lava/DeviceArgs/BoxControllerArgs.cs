using ConnectedDevice;
using Lava.DeviceArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lava
{
    class BoxControllerArgs: BaseArgs
    {
        public BoxControllerArgs()
        {
            bytesOnRead = 17;
        }

        public List<byte[]> BoxFlangesArgs { get; private set; }

        public List<byte[]> BoxRubilnikArgs { get; private set; }

        public List<byte[]> BoxAlarmArgs { get; private set; }
        public List<byte[]> WorkModeArgs { get; private set; }

        public BoxControllerArgs(List<byte[]> boxFlangesArgs, List<byte[]> boxRubilnikArgs, List<byte[]> boxAlarmArgs, List<byte[]> workModeArgs)
        {
            BoxFlangesArgs = boxFlangesArgs;
            BoxRubilnikArgs = boxRubilnikArgs;
            BoxAlarmArgs = boxAlarmArgs;
            WorkModeArgs = workModeArgs;
        }
    }
}
