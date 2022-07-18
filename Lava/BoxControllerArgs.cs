using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lava
{
    class BoxControllerArgs
    {
        public List<byte[]> BoxFlangesArgs { get; private set; }

        public List<byte[]> BoxRubilnikArgs { get; private set; }

        public List<byte[]> BoxAlarmArgs { get; private set; }

        public BoxControllerArgs(List<byte[]> boxFlangesArgs, List<byte[]> boxRubilnikArgs, List<byte[]> boxAlarmArgs)
        {
            BoxFlangesArgs = boxFlangesArgs;
            BoxRubilnikArgs = boxRubilnikArgs;
            BoxAlarmArgs = boxAlarmArgs;
        }
    }
}
