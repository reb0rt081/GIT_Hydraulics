using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Hydraulics
{
    /// <summary>
    /// This class describes an uniform canal section of a given length an shape
    /// </summary>
    public abstract class CanalSection
    {
        /// <summary>
        /// Roughness coefficient applyin Manning's rule
        /// </summary>
        public double Roughness { get; set; }

        /// <summary>
        /// Length of the canal section
        /// </summary>
        public double Length { get; set; }
        
        /// <summary>
        /// Slope of the canal section in 1/1
        /// </summary>
        public double Slope { get; set; }

        /// <summary>
        /// Returns the Froude number of a canal section at a given length
        /// </summary>
        /// <param name="refX"></param>
        /// <returns></returns>
        public double GetFroudeNumber(double refX)
        {
            return GetManningVelocity(refX) / Math.Sqrt(Constants.GravityAcceleration * GetHydraulicDepth(refX));
        }

        /// <summary>
        /// Returns the speed of the water at a given point by means of the Manning formula
        /// </summary>
        /// <param name="refX"></param>
        /// <returns></returns>
        public double GetManningVelocity(double refX)
        {
            return (1 / Slope) * Math.Pow(GetHydraulicRadius(refX), 2.0/3.0) * Math.Sqrt(Slope);
        }

        /// <summary>
        /// Returns the hydraulic radius
        /// </summary>
        /// <param name="refX"></param>
        /// <returns></returns>
        public double GetHydraulicRadius(double refX)
        {
            return GetHydraulicArea(refX) / GetHydraulicPerimeter(refX); 
        }
        
        /// <summary>
        /// Returns the Hydraulic depth that can be used to obtain the Froude number
        /// </summary>
        /// <param name="refX"></param>
        /// <returns></returns>
        public abstract double GetHydraulicDepth(double refX);

        public abstract double GetHydraulicArea(double refX);

        public abstract double GetHydraulicPerimeter(double refX);

        public double GetWaterLevel(double refX)
        {
            return refX;
        }
        
    }
}
