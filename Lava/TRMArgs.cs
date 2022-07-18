using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lava
{
    class TRMArgs
    {
        public float Gazohran_Temp { get; private set; }
        public float LiveZone_Temp { get; private set; }
        public float PromZone_Temp { get; private set; }
        public float Manometr_value { get; private set; }

        public TRMArgs(float gazohran_temp, float livezone_temp, float promzone_temp, float manometr_value)
        {
            Gazohran_Temp = gazohran_temp;
            LiveZone_Temp = livezone_temp;
            PromZone_Temp = promzone_temp;
            Manometr_value = manometr_value;
        }

        public TRMArgs(float gazohran_temp, float livezone_temp, float promzone_temp)
        {
            Gazohran_Temp = gazohran_temp;
            LiveZone_Temp = livezone_temp;
            PromZone_Temp = promzone_temp;
            //Manometr_value = manometr_value;
        }
    }
}
