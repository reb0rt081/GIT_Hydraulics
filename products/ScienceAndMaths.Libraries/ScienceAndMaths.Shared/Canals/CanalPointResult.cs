using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared.Canals
{
    public class CanalPointResult
    {
        public CanalPointResult(double x, double waterLevel)
        {
            X = x;
            WaterLevel = waterLevel;
        }

        public double X { get; set; }

        public double WaterLevel { get; set; }
    }
}
