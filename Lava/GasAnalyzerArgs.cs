using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lava
{
    class GasAnalyzerArgs
    {
        public int GasPercent { get; private set; }
        public int GasState { get; private set; }
        public GasAnalyzerArgs(int gasPercent, int gasState)
        {
            GasPercent = gasPercent;
            GasState = gasState;
        }
    }
}
