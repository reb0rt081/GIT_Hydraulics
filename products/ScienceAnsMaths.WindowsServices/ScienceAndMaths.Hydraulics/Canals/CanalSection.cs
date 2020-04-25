using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Hydraulics.Canals
{
    /// <summary>
    /// This class describes an uniform canal section of a given length an shape
    /// </summary>
    public abstract class CanalSection : ICanalSection
    {
        /// <summary>
        /// Roughness coefficient applying Manning's rule
        /// </summary>
        public double Roughness { get; set; }

        /// <summary>
        /// Slope of the canal section in 1/1 (m/m)
        /// Slope = dz/dx = elevation viariation / length variation (hypotenuse)
        /// </summary>
        public double Slope { get; set; }

        /// <summary>
        /// Returns the Froude number of a canal section at a given length
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        public double GetFroudeNumber(double waterLevel)
        {
            return GetManningVelocity(waterLevel) / Math.Sqrt(Constants.GravityAcceleration * GetHydraulicDepth(waterLevel));
        }

        /// <summary>
        /// Returns the speed of the water at a given point by means of the Manning formula
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        public double GetManningVelocity(double waterLevel)
        {
            return (1 / Roughness) * Math.Pow(GetHydraulicRadius(waterLevel), 2.0/3.0) * Math.Sqrt(Slope);
        }

        /// <summary>
        /// Returns the hydraulic radius
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        public double GetHydraulicRadius(double waterLevel)
        {
            return GetHydraulicArea(waterLevel) / GetHydraulicPerimeter(waterLevel); 
        }

        /// <summary>
        /// Returns the Hydraulic depth that can be used to obtain the Froude number.
        /// HydraulicDepth = HydraulicArea / T, where T is the width of free surface
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        public abstract double GetHydraulicDepth(double waterLevel);

        /// <summary>
        /// Returns the Hydraulic area of the canal section
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        public abstract double GetHydraulicArea(double waterLevel);

        /// <summary>
        /// Returns the Hydraulic perimeter of the canal section
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        public abstract double GetHydraulicPerimeter(double waterLevel);
 
    }
}
