using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Hydraulics.Canals
{
    /// <summary>
    /// Represents a sluice canal gate as a boundary condition.
    /// </summary>
    public class SluiceCanalEdge : CanalEdge
    {
        /// <summary>
        /// Gets or sets the width of the gate
        /// </summary>
        public double GateWidth { get; set; }

        /// <summary>
        /// Gets or sets the contraction coefficient
        /// </summary>
        public double ContractionCoefficient { get; set; }

        /// <summary>
        /// Gets or sets the water level prior to the gate
        /// </summary>
        public double GateWaterLevel { get; set; }

        /// <summary>
        /// Gets or sets the water level in the edge
        /// </summary>
        public override double? WaterLevel { get; set; }

        
    }
}
