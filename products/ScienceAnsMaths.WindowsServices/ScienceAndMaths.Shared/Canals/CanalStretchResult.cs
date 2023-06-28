using System.Collections.Generic;

namespace ScienceAndMaths.Shared.Canals
{
    public class CanalStretchResult
    {
        public CanalStretchResult()
        {
            CanalPointResults = new List<CanalPointResult>();
        }

        public List<CanalPointResult> CanalPointResults { get; set; }
        
        public double CriticalWaterLevel { get; set; }

        public double CriticalSlope { get; set; }

        public double NormalWaterLevel { get; set; }

        public string BackwaterCurve { get; set; }
    }
}
