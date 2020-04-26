using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared
{
    public interface ICanalStretch
    {
        /// <summary>
        /// Gets or sets the unique identifier for the canal.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the source node for this canal stretch.
        /// </summary>
        ICanalEdge FromNode { get; set; }

        /// <summary>
        /// Gets or sets the destination node for this canal stretch.
        /// </summary>
        ICanalEdge ToNode { get; set; }

        /// <summary>
        /// Gets or sets the length of the canal
        /// </summary>
        double Length { get; set; }

        /// <summary>
        /// Gets or sets the flow of the canal
        /// </summary>
        double Flow { get; set; }

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
        /// Returns the critical depth of the canal stretch when Froude Number = 1
        /// </summary>
        /// <param name="waterLvel"></param>
        /// <returns></returns>
        double GetCriticalHydraulicDepth(double waterLevel);

        /// <summary>
        /// Returns the flow equation
        /// </summary>
        /// <returns></returns>
        Func<double, double, double> FlowEquation();
    }
}
