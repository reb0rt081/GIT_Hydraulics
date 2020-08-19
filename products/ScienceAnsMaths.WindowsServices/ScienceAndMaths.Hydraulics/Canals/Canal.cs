using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization.Formatters;
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
        private const double Sensibility = 0.002;

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
        /// Returns the active canal stretch from a given distance x
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public ICanalStretchModel GetCanalStretchForAbsoluteX(double x)
        {
            ICanalStretchModel canalStretch = CanalStretches.FirstOrDefault(csfirst => CanalStretches.All(cs => cs.ToNode.Id != csfirst.FromNode.Id));
            double relativeDistance = x;
            bool found = false;

            while(!found && relativeDistance > 0 && canalStretch != null)
            {
                if(relativeDistance < canalStretch.Length)
                {
                    found = true;
                }
                else
                {
                    relativeDistance -= canalStretch.Length;
                    
                    var nextStretch = CanalStretches.FirstOrDefault(cs => cs.FromNode.Id == canalStretch.ToNode.Id);

                    if (relativeDistance > 0 || nextStretch != null)
                    {
                        canalStretch = nextStretch;
                    }
                }
            }

            return canalStretch;
        }

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

                ICanalStretchModel preCanalStretch = CanalStretches.FirstOrDefault(cs => cs.ToNode.Id == canalStretch.FromNode.Id);
                ICanalStretchModel postCanalStretch = CanalStretches.FirstOrDefault(cs => cs.FromNode.Id == canalStretch.ToNode.Id);

                bool postCriticalSection = false;
                
                if (preCanalStretch != null)
                {

                }

                if(postCanalStretch != null)
                {
                    postCriticalSection = postCanalStretch.CanalSection.GetNormalWaterLevel(postCanalStretch.Flow) < canalStretchResult.CriticalWaterLevel;
                }

                AnalysisOptions options = new AnalysisOptions();
                options.AnalysisSteps = (int) (canalStretch.Length > 10000
                    ? 10000
                    : canalStretch.Length);

                //  M flow 
                if (canalStretch.CanalSection.Slope < canalStretchResult.CriticalSlope)
                {
                    // M1
                    // Regimen lento se impone aguas abajo
                    if (canalStretch.ToNode.WaterLevel.HasValue && canalStretch.ToNode.WaterLevel.Value > canalStretchResult.CriticalWaterLevel)
                    {
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + canalStretch.Length;
                        options.InitialWaterLevel = canalStretch.ToNode.WaterLevel.Value;
                        options.BackwardsAnalysis = true;
                        options.ExecuteAnalysis = true;
                    }
                    //  M2 Flow
                    // Regimen lento se impone aguas abajo
                    else if (postCriticalSection)
                    {
                        canalStretch.ToNode.WaterLevel = canalStretchResult.CriticalWaterLevel;
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + canalStretch.Length;
                        options.InitialWaterLevel = canalStretchResult.CriticalWaterLevel + Sensibility /* Salvando numéricamente por la izquierda el problema */;
                        options.BackwardsAnalysis = true;
                        options.ExecuteAnalysis = true;
                    }
                    // M3 Flow
                    // Regimen rapido se impone aguas arriba
                    else if (canalStretch.FromNode.WaterLevel.HasValue)
                    {
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + 0.0;
                        options.InitialWaterLevel = canalStretch.FromNode.WaterLevel.Value;
                        options.BackwardsAnalysis = false;
                        options.ExecuteAnalysis = true;
                    }
                }
                //  S flow
                else if(canalStretch.CanalSection.Slope > canalStretchResult.CriticalSlope)
                {
                    //  S1 Flow
                    // Regimen lento se impone aguas abajo
                    if (canalStretch.ToNode.WaterLevel.HasValue && canalStretch.ToNode.WaterLevel.Value > canalStretchResult.CriticalWaterLevel)
                    {
                        throw new NotImplementedException();
                    }
                    // S2 flow
                    // Regimen rapido se impone aguas arriba
                    //  TODO el simbolo <= debe ser mejorado
                    else if (canalStretch.FromNode.WaterLevel.HasValue && canalStretch.FromNode.WaterLevel.Value <= canalStretchResult.CriticalWaterLevel && canalStretch.FromNode.WaterLevel.Value > canalStretchResult.NormalWaterLevel)
                    {
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + 0.0;
                        options.InitialWaterLevel = canalStretch.FromNode.WaterLevel.Value - Sensibility /* Salvando numéricamente por la derecha el problema */;
                        options.BackwardsAnalysis = false;
                        options.ExecuteAnalysis = true;
                    }
                }
                //  H flow
                else
                {
                    if (canalStretch.FromNode.WaterLevel.HasValue)
                    {
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + 0.0;
                        options.InitialWaterLevel = canalStretch.FromNode.WaterLevel.Value;
                        options.BackwardsAnalysis = false;
                        options.ExecuteAnalysis = true;
                    }
                }

                if(options.ExecuteAnalysis)
                {
                    double x = options.InitialX;
                    double waterLevel = options.InitialWaterLevel;

                    int steps = options.AnalysisSteps;

                    solver.Interval = canalStretch.Length / steps;
                    solver.Equation = canalStretch.FlowEquation();

                    if (options.BackwardsAnalysis)
                    {
                        result.AddCanalPointResult(canalStretch.Id, x, waterLevel);

                        for (int i = 1; i <= steps; i++)
                        {
                            waterLevel = solver.SolveBackwards(x, waterLevel);
                            x = x - solver.Interval;

                            result.AddCanalPointResult(canalStretch.Id, x, waterLevel);
                        }

                        canalStretch.FromNode.WaterLevel = waterLevel;
                    }
                    else
                    {
                        result.AddCanalPointResult(canalStretch.Id, x, waterLevel);

                        // Regimen lento se impone aguas abajo
                        // Regimen rapido se impone aguas arriba

                        for (int i = 1; i <= steps; i++)
                        {
                            waterLevel = solver.Solve(x, waterLevel);
                            x = x + solver.Interval;

                            result.AddCanalPointResult(canalStretch.Id, x, waterLevel);
                        }

                        canalStretch.ToNode.WaterLevel = waterLevel;
                    }
                }
            }

            return result;
        }

        private double GetAbsoluteInitialLength(List<ICanalStretchModel> canalStretches, ICanalStretchModel activeCanalStretch)
        {
            double length = 0d;
            ICanalStretchModel currentCanalStretch = activeCanalStretch;

            do
            {
                ICanalStretchModel priorCanalStretch = canalStretches.FirstOrDefault(cs => cs.ToNode.Id == currentCanalStretch.FromNode.Id);

                if(priorCanalStretch != null)
                {
                    length += priorCanalStretch.Length;
                }

                currentCanalStretch = priorCanalStretch;

            } while (currentCanalStretch != null);

            return length;
        }
    }
}
