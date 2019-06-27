using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Hydraulics
{
    public class RectangularSection : CanalSection
    {
        public double Width { get; set; }

        public override double GetHydraulicDepth(double refX)
        {
            return GetWaterLevel(refX);
        }

        public override double GetHydraulicArea(double refX)
        {
            return GetWaterLevel(refX) * Width;
        }

        public override double GetHydraulicPerimeter(double refX)
        {
            return Width + 2 * GetWaterLevel(refX);
        }
    }
}
