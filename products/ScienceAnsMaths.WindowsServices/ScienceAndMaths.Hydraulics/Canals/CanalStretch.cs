using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string Id { get; set; }

        public CanalEdge FromNode { get; set; }

        public CanalEdge ToNode { get; set; }

        /// <summary>
        /// Gets or sets the lenght of the canal
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
        /// Returns the flow equation
        /// </summary>
        /// <returns></returns>
        public Func<double, double, double> FlowEquation()
        {
            return (x, y) => (CanalSection.Slope - (Math.Pow(Flow, 2) * Math.Pow(CanalSection.Roughness, 2)) / (Math.Pow(CanalSection.GetHydraulicArea(y), 2) * Math.Pow(CanalSection.GetHydraulicRadius(y), 2.0 / 3.0))) / (1 - Math.Pow(CanalSection.GetFroudeNumber(y), 2));
        }
    }
}
