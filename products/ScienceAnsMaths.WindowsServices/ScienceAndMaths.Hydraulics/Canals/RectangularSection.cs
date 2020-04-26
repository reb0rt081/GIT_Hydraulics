using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Hydraulics.Canals
{
    public class RectangularSection : CanalSection
    {
        public RectangularSection(double width, double roughness, double slope)
        {
            Width = width;
            Roughness = roughness;
            Slope = slope;
        }

        public double Width { get; set; }

        public override double ConvertWaterLevelToHydraulicDepth(double waterLevel)
        {
            return waterLevel;
        }

        public override double ConvertHydraulicDepthToWaterLevel(double hydraulicDepth)
        {
            return hydraulicDepth;
        }

        public override double GetHydraulicArea(double waterLevel)
        {
            return waterLevel * Width;
        }

        public override double GetHydraulicPerimeter(double waterLevel)
        {
            return Width + 2 * waterLevel;
        }        
    }
}
