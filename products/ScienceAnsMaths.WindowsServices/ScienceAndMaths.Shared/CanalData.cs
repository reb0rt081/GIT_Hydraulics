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

        public ICanalStretch GetCanalStretch(CanalPointResult canalPointResult)
        {
            int canalIndex = 0;
            double relativeCanalX = canalPointResult.X;

            while(canalIndex < Canal.CanalStretches.Count)
            {
                ICanalStretch canalStretch = Canal.CanalStretches[canalIndex];

                if(relativeCanalX <= canalStretch.Length)
                {
                    return canalStretch;
                }
                else
                {
                    relativeCanalX -= canalStretch.Length;
                }
            }

            return null;
        }

        public ICanalSection GetCanalSection(CanalPointResult canalPointResult)
        {
            var canalStretch = GetCanalStretch(canalPointResult);

            if(canalStretch != null)
            {
                return canalStretch.CanalSection;
            }

            return null;
        }
    }
}
