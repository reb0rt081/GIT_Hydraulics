using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Hydraulics.Canals
{
    /// <summary>
    /// Represents a canal of equal dimensions with certain length
    /// </summary>
    public class CanalStretch
    {
        public CanalStretch(double length, double flow, CanalSection canalSection)
        {
            Length = length;
            Flow = flow;
            CanalSection = canalSection;
        }

        public CanalStretch()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for the canal.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the source node for this canal stretch.
        /// </summary>
        public CanalEdge FromNode { get; set; }

        /// <summary>
        /// Gets or sets the destination node for this canal stretch.
        /// </summary>
        public CanalEdge ToNode { get; set; }

        /// <summary>
        /// Gets or sets the length of the canal
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the flow of the canal
        /// </summary>
        public double Flow { get; set; }

        /// <summary>
        /// Gets or sets the canal section
        /// </summary>
        public CanalSection CanalSection { get; set; }

        /// <summary>
        /// Returns the critical depth of the canal stretch when Fr = 1
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public double GetCriticalDepth(double y)
        {
            return Math.Pow(Flow, 2) / (Constants.GravityAcceleration * CanalSection.GetHydraulicArea(y)); 
        }

        /// <summary>
        /// Returns the flow equation
        /// </summary>
        /// <returns></returns>
        public Func<double, double, double> FlowEquation()
        {
            return (x, y) => (CanalSection.Slope - (Math.Pow(Flow, 2) * Math.Pow(CanalSection.Roughness, 2)) / (Math.Pow(CanalSection.GetHydraulicArea(y), 2) * Math.Pow(CanalSection.GetHydraulicRadius(y), 2.0 / 3.0))) / (Math.Cos(Math.Asin(CanalSection.Slope)) - Math.Pow(CanalSection.GetFroudeNumber(y), 2));
        }
    }
}
