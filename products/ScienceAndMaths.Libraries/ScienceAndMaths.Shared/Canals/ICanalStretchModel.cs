using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared.Canals
{
    public interface ICanalStretchModel : ICanalStretch
    {
        /// <summary>
        /// Gets or sets the source node for this canal stretch.
        /// </summary>
        ICanalEdge FromNode { get; set; }

        /// <summary>
        /// Gets or sets the destination node for this canal stretch.
        /// </summary>
        ICanalEdge ToNode { get; set; }

        /// <summary>
        /// Gets or sets the canal section
        /// </summary>
        ICanalSectionModel CanalSection { get; set; }

        /// <summary>
        /// Returns the Froude number of a canal stretch at a given length
        /// </summary>
        /// <param name="waterLevel">The level of the water in the canal section</param>
        /// <returns></returns>
        double GetFroudeNumber(double waterLevel);

        /// <summary>
        /// Returns the flow equation
        /// </summary>
        /// <returns></returns>
        Func<double, double, double> FlowEquation();

        /// <summary>
        /// Returns the associated depth downstream (y2) by means of the Belanger Equation that yields the relationship between the depth upstream (y1) and downstream (y2) in a Hydraulic Jump
        /// </summary>
        /// <returns></returns>
        double GetHydraulicJumpDownstreamDepth(double y1);

        /// <summary>
        /// Gets or sets the analysis options for this canal section
        /// </summary>
        AnalysisOptions AnalysisOptions { get; set; }

        /// <summary>
        /// Gets or sets the main values regarding the canal stretch
        /// </summary>
        CanalStretchResult CanalStretchResult { get; set; }
    }
}
