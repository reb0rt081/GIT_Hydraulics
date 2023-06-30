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

        public ICanalStretchModel GetCanalStretch(double relativeCanalX)
        {
            int canalIndex = 0;
            
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
                    canalIndex++;
                }
            }

            return null;
        }

        public ICanalStretchModel GetCanalStretch(string canalStretchId)
        {
            return Canal.CanalStretches.Single(cs => cs.Id == canalStretchId);
        }

        public ICanalSection GetCanalSection(CanalPointResult canalPointResult)
        {
            var canalStretch = GetCanalStretch(canalPointResult.X);

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

        /// <summary>
        /// Returns the canal geometry data
        /// </summary>
        /// <returns></returns>
        public CanalGeometryData GetCanalGeometryData(string canalStretchId)
        {
            if (canalStretchId == null)
            {
                return new CanalGeometryData();
            }

            ICanalStretchModel canalStretch = GetCanalStretch(canalStretchId);
            
            return new CanalGeometryData
            {
                Length = canalStretch.Length,
                Flow = canalStretch.Flow,
                Roughness = canalStretch.CanalSection.Roughness,
                Slope = canalStretch.CanalSection.Slope,
                Id = canalStretch.Id,
                CriticalSlope = CanalResult?.GetCanalStretchResult(canalStretchId)?.CriticalSlope ?? 0,
                CriticalWaterLevel = CanalResult?.GetCanalStretchResult(canalStretchId)?.CriticalWaterLevel ?? 0,
                NormalWaterLevel = CanalResult?.GetCanalStretchResult(canalStretchId)?.NormalWaterLevel ?? 0,
                BackwaterCurve = CanalResult?.GetCanalStretchResult(canalStretchId)?.BackwaterCurve ?? "NotYetCalculated"
            };
        }
    }
}
