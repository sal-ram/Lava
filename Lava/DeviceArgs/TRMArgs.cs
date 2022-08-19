using Lava.DeviceArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lava
{
    class TRMArgs: BaseArgs
    {
        public TRMArgs()
        {
            bytesOnRead = 32;
        }

        public float GazohranTemp { get; private set; }
        public float LiveZoneTemp { get; private set; }
        public float PromZoneTemp { get; private set; }
        public float ManometrValue { get; private set; }

        public TRMArgs(float gazohranTemp, float livezoneTemp, float promzoneTemp, float manometrValue)
        {
            GazohranTemp = gazohranTemp;
            LiveZoneTemp = livezoneTemp;
            PromZoneTemp = promzoneTemp;
            ManometrValue = manometrValue;
        }
    }
}
