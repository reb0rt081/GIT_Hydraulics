using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Mathematics;
using ScienceAndMaths.Shared;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Hydraulics.Canals
{
    /// <summary>
    /// Represents a canal configuration with a set of edges and stretches.
    /// </summary>
    public class Canal : ICanal
    {
        public Canal()
        {
            CanelEdges = new List<ICanalEdge>();
            CanalStretches = new List<ICanalStretchModel>();
        }

        /// <summary>
        /// Gets or sets the unique identifier for the canal.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets or sets the canal edges to define the boundary conditions.
        /// </summary>
        public List<ICanalEdge> CanelEdges { get; set; }

        /// <summary>
        /// Gets or sets the canal homogeneous sections.
        /// </summary>
        public List<ICanalStretchModel> CanalStretches { get; set; }

        /// <summary>
        /// Executes the canal simulation
        /// </summary>
        /// <returns></returns>
        public CanalSimulationResult ExecuteCanalSimulation()
        {
            var result = new CanalSimulationResult();

            RungeKutta solver = new RungeKutta();

            double flow = CanalStretches.Where(cs => cs.Flow > 0).Select(cs => cs.Flow).FirstOrDefault();

            if (!(flow > 0))
            {
                flow = CanelEdges.OfType<SluiceCanalEdge>().Select(sce => sce.GetFreeFlow).FirstOrDefault();
            }

            CanalStretches.ForEach(cs => cs.Flow = flow);

            foreach (ICanalStretchModel canalStretch in CanalStretches)
            {
                CanalStretchResult canalStretchResult = result.GetCanalStretchResult(canalStretch.Id);
                canalStretchResult.CriticalSlope = canalStretch.CanalSection.GetCriticalSlope(canalStretch.Flow);
                canalStretchResult.CriticalWaterLevel = canalStretch.CanalSection.GetCriticalWaterLevel(canalStretch.Flow);
                canalStretchResult.NormalWaterLevel = canalStretch.CanalSection.GetNormalWaterLevel(canalStretch.Flow);

                //  Subcritical flow 
                if (canalStretch.CanalSection.Slope < canalStretchResult.CriticalSlope)
                {
                    if (canalStretch.ToNode.WaterLevel.HasValue)
                    {
                        double x = canalStretch.Length;
                        double waterLevel = canalStretch.ToNode.WaterLevel.Value;
                        result.AddCanalPointResult(canalStretch.Id, canalStretch.Length, waterLevel);

                        int steps = (int)(canalStretch.Length > 10000
                            ? 10000
                            : canalStretch.Length);

                        // Regimen lento se impone aguas abajo
                        // Regimen rapido se impone aguas arriba

                        solver.Interval = canalStretch.Length / steps;
                        solver.Equation = canalStretch.FlowEquation();

                        for (int i = 1; i <= steps; i++)
                        {
                            waterLevel = solver.SolveBackwards(x, waterLevel);
                            x = x - solver.Interval;

                            result.AddCanalPointResult(canalStretch.Id, x, waterLevel);
                        }
                    }
                    else if (canalStretch.FromNode.WaterLevel.HasValue)
                    {
                        double x = 0.0;
                        double waterLevel = canalStretch.FromNode.WaterLevel.Value;
                        result.AddCanalPointResult(canalStretch.Id, 0.0, waterLevel);

                        int steps = (int)(canalStretch.Length > 10000
                            ? 10000
                            : canalStretch.Length);

                        // Regimen lento se impone aguas abajo
                        // Regimen rapido se impone aguas arriba

                        solver.Interval = canalStretch.Length / steps;
                        solver.Equation = canalStretch.FlowEquation();

                        for (int i = 1; i <= steps; i++)
                        {
                            waterLevel = solver.Solve(x, waterLevel);
                            x = x + solver.Interval;

                            result.AddCanalPointResult(canalStretch.Id, x, waterLevel);
                        }

                        canalStretch.FromNode.WaterLevel = waterLevel;
                    }
                }
                //  Supercritical flow
                else
                {
                    
                }
            }

            return result;
        }
    }
}
