using ScienceAndMaths.Shared;
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
            // En el caso trapezoidal seria el tirante hidraulico A/T
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

        public override double GetCriticalWaterLevel(double flow)
        {
            return ConvertHydraulicDepthToWaterLevel(Math.Pow((Math.Pow(flow, 2) / (Constants.GravityAcceleration * Math.Pow(Width, 2))), 1.0 / 3.0));
        }
    }
}
