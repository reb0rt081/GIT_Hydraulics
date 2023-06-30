using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Shared;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Hydraulics.Canals
{
    /// <summary>
    /// Represents a canal of equal dimensions with certain length
    /// </summary>
    public class CanalStretch : ICanalStretchModel
    {
        public CanalStretch(string id, double length, double flow, CanalSection canalSection)
        {
            Id = id;
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
        public ICanalEdge FromNode { get; set; }

        /// <summary>
        /// Gets or sets the destination node for this canal stretch.
        /// </summary>
        public ICanalEdge ToNode { get; set; }

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
        public ICanalSectionModel CanalSection { get; set; }

        /// <summary>
        /// Gets or sets the analysis options for this canal section
        /// </summary>
        public AnalysisOptions AnalysisOptions { get; set; }

        /// <summary>
        /// Gets or sets the main values regarding the canal stretch
        /// </summary>
        public CanalStretchResult CanalStretchResult { get; set; }

        /// <summary>
        /// Returns the Froude number of a canal stretch at a given length
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        public double GetFroudeNumber(double waterLevel)
        {
            return Flow / (Math.Sqrt(Constants.GravityAcceleration * CanalSection.ConvertWaterLevelToHydraulicDepth(waterLevel)) * CanalSection.GetHydraulicArea(waterLevel));
        }

        /// <summary>
        /// Returns the flow equation
        /// </summary>
        /// <returns></returns>
        public Func<double, double, double> FlowEquation()
        {
            return (x, y) => (CanalSection.Slope - (Math.Pow(Flow, 2) * Math.Pow(CanalSection.Roughness, 2)) / (Math.Pow(CanalSection.GetHydraulicArea(y), 2) * Math.Pow(CanalSection.GetHydraulicRadius(y), 4.0 / 3.0))) / (Math.Cos(Math.Asin(CanalSection.Slope)) - Math.Pow(GetFroudeNumber(y), 2));
        }

        /// <summary>
        /// Returns the associated depth downstream (y2) by means of the Belanger Equation that yields the relationship between the depth upstream (y1) and downstream (y2) in a Hydraulic Jump
        /// </summary>
        /// <returns></returns>
        public double GetHydraulicJumpDownstreamDepth(double y1)
        {
            return y1 * 0.5 * (Math.Sqrt(1 + 8 * Math.Pow(GetFroudeNumber(y1), 2) - 1));
        }
    }
}
