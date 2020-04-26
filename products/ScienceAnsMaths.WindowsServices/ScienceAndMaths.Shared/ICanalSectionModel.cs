using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared
{
    /// <summary>
    /// Interface for canal flow calculation
    /// </summary>
    public interface ICanalSectionModel : ICanalSection
    {
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
        /// Returns the hydraulic depth for a given canal section based on the water level.
        /// HydraulicDepth = HydraulicArea / T, where T is the width of free surface
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        double ConvertWaterLevelToHydraulicDepth(double waterLevel);

        /// <summary>
        /// Returns the water level for a given canal section based on the hydraulic depth.
        /// HydraulicDepth = HydraulicArea / T, where T is the width of free surface
        /// </summary>
        /// <param name="hydraulicDepth">The hydraulic depth in the canal section</param>
        /// <returns></returns>
        double ConvertHydraulicDepthToWaterLevel(double hydraulicDepth);

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
