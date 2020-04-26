using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared
{
    public interface ICanalSection
    {
        /// <summary>
        /// Roughness coefficient applying Manning's rule
        /// </summary>
        double Roughness { get; set; }

        /// <summary>
        /// Slope of the canal section in 1/1 (m/m)
        /// </summary>
        double Slope { get; set; }

        /// <summary>
        /// Returns the flow of a canal section in Manning and uniform conditions.
        /// The water level will tend to this value.
        /// </summary>
        /// <param name="waterLevel"></param>
        /// <returns></returns>
        double GetManningFlow(double waterLevel);

        /// <summary>
        /// Returns the speed of the water at a given point by means of the Manning formula
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        double GetManningVelocity(double waterLevel);

        /// <summary>
        /// Returns the hydraulic radius
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        double GetHydraulicRadius(double waterLevel);

        /// <summary>
        /// Returns the Hydraulic depth that can be used to obtain the Froude number.
        /// HydraulicDepth = HydraulicArea / T, where T is the width of free surface
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        double GetHydraulicDepth(double waterLevel);

        /// <summary>
        /// Returns the Hydraulic area of the canal section
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        double GetHydraulicArea(double waterLevel);

        /// <summary>
        /// Returns the Hydraulic perimeter of the canal section
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        double GetHydraulicPerimeter(double waterLevel);
    }
}
