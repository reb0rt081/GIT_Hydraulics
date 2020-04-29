using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared.Canals
{
    public class CanalSimulationResult
    {
        public CanalSimulationResult()
        {
            CanalPointResults = new List<CanalPointResult>();
        }

        public List<CanalPointResult> CanalPointResults { get; set; }

        public void AddCanalPointResult(double x, double waterLevel)
        {
            CanalPointResults.Add(new CanalPointResult(x, waterLevel));
        }
    }
}
