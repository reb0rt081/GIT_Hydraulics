using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Hydraulics.Canals
{
    /// <summary>
    /// Represents a sluice canal gate as a boundary condition.
    /// </summary>
    public class SluiceCanalEdge : CanalEdge
    {
        private double? waterLevel;

        /// <summary>
        /// Gets or sets the opening of the gate
        /// </summary>
        public double GateOpening { get; set; }

        /// <summary>
        /// Gets or sets the contraction coefficient
        /// </summary>
        public double ContractionCoefficient { get; set; }

        /// <summary>
        /// Gets or sets the water level prior to the gate
        /// </summary>
        public double RetainedWaterLevel { get; set; }

        /// <summary>
        /// Gets or sets the water level in the edge
        /// </summary>
        public override double? WaterLevel {
            get
            {
                if (!waterLevel.HasValue)
                {
                    waterLevel = ContractionCoefficient * GateOpening;
                }

                return waterLevel;
            }
            set { waterLevel = value; }
        }

        /// <summary>
        /// Returns the flow per unit (m3/m) when the sluice gate is far enough from the imposed level downstream
        /// </summary>
        public double GetFreeFlow
        {
            get
            {
                return (ContractionCoefficient / (Math.Sqrt(1 + (ContractionCoefficient * GateOpening) / RetainedWaterLevel))) 
                       * GateOpening 
                       * Math.Sqrt(2 * Constants.GravityAcceleration * RetainedWaterLevel);
            }
        }

        /// <summary>
        /// Returns the flow per unit (m3/m) when the sluice gate is affected by the imposed level downstream, less capacity
        /// </summary>
        public double GetImposedFlow
        {
            get
            {
                return (ContractionCoefficient / Math.Sqrt(1 - Math.Pow((ContractionCoefficient * GateOpening) / RetainedWaterLevel, 2))) 
                       * GateOpening 
                       * Math.Sqrt(2 * Constants.GravityAcceleration * (RetainedWaterLevel - ContractionCoefficient * GateOpening));
            }
        }
    }
}
 