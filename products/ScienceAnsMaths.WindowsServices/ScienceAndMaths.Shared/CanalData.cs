using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Shared
{
    public class CanalData
    {
        public CanalData(ICanal canal, CanalSimulationResult canalResult)
        {
            Canal = canal;
            CanalResult = canalResult;
        }

        public CanalData(ICanal canal)
        {
            Canal = canal;
            CanalResult = null;
        }

        public ICanal Canal { get; set; }

        public CanalSimulationResult CanalResult { get; set; }
    }
}
