using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Mathematics;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Hydraulics.Canals
{
    /// <summary>
    /// Represents a canal configuration with a set of edges and stretches.
    /// </summary>
    public class Canal
    {
        public Canal()
        {
            CanelEdges = new List<CanalEdge>();
            CanalStretches = new List<CanalStretch>();
        }

        /// <summary>
        /// Gets or sets the unique identifier for the canal.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets or sets the canal edges to define the boundary conditions.
        /// </summary>
        public List<CanalEdge> CanelEdges;

        /// <summary>
        /// Gets or sets the canal homogeneous sections.
        /// </summary>
        public List<CanalStretch> CanalStretches;

        /// <summary>
        /// Executes the canal simulation
        /// </summary>
        /// <returns></returns>
        public CanalSimulationResult ExecuteCanalSimulation()
        {
            var result = new CanalSimulationResult();

            RungeKutta solver = new RungeKutta();

            foreach (CanalStretch canalStretch in CanalStretches)
            {
                if(canalStretch.Flow > 0 && canalStretch.FromNode.WaterLevel.HasValue)
                {
                    double waterLevel = canalStretch.FromNode.WaterLevel.Value;
                    result.AddCanalPointResult(0.0, waterLevel);

                    int steps = (int) (canalStretch.Length > 500
                        ? canalStretch.Length
                        : 500);

                    solver.Interval = canalStretch.Length / steps;
                    solver.Equation = canalStretch.FlowEquation();
                    
                    for(int i = 1; i <= steps; i++)
                    {
                        double x = i * solver.Interval;

                        waterLevel = solver.Solve(x, waterLevel);

                        result.AddCanalPointResult(x, waterLevel);
                    }
                }
            }

            return result;
        }
    }
}
