using ScienceAndMaths.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Hydraulics.Canals
{
    /// <summary>
    /// Represents the canal boundary conditions.
    /// </summary>
    public class CanalEdge : ICanalEdge
    {
        /// <summary>
        /// Gets or sets the unique identifier for the canal edge.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the water level in the edge
        /// </summary>
        public double? WaterLevel { get; set; }
    }
}
