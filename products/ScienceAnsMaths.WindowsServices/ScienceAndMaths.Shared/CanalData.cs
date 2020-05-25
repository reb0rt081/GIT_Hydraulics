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

        public ICanalStretchModel GetCanalStretch(CanalPointResult canalPointResult)
        {
            int canalIndex = 0;
            double relativeCanalX = canalPointResult.X;

            while(canalIndex < Canal.CanalStretches.Count)
            {
                ICanalStretchModel canalStretch = Canal.CanalStretches[canalIndex];

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

        public CanalPointResult GetCanalPointResult(double horizontalDistance)
        {
            int canalIndex = 0;
            bool found = false;
            double relativehorizontalDistance = horizontalDistance;
            double coordinateX = 0;

            while (!found && canalIndex < Canal.CanalStretches.Count)
            {
                ICanalStretchModel canalStretch = Canal.CanalStretches[canalIndex];
                double relativeLengthDistance = canalStretch.Length * Math.Cos(Math.Atan(canalStretch.CanalSection.Slope));

                if (relativehorizontalDistance <= relativeLengthDistance)
                {
                    coordinateX += relativehorizontalDistance / Math.Cos(Math.Atan(canalStretch.CanalSection.Slope));
                    found = true;
                }
                else
                {
                    relativehorizontalDistance -= relativeLengthDistance;
                    coordinateX += canalStretch.Length;
                }
            }

            return CanalResult?.CanalPointResults?.OrderBy(pr => Math.Abs(coordinateX - pr.X)).FirstOrDefault();
        }
    }
}
