using System;
using System.Collections.Generic;
using System.Linq;

using ScienceAndMaths.Mathematics;
using ScienceAndMaths.Shared.Canals;

namespace ScienceAndMaths.Hydraulics.Canals
{
    /// <summary>
    /// Represents a canal configuration with a set of edges and stretches.
    /// </summary>
    public class Canal : ICanal
    {
        private const double Sensibility = 0.005;

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

            while (!found && relativeDistance > 0 && canalStretch != null)
            {
                if (relativeDistance < canalStretch.Length)
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
            List<ICanalStretchModel> canalStretchSorted = new List<ICanalStretchModel>();

            double flow = CanalStretches.Where(cs => cs.Flow > 0).Select(cs => cs.Flow).FirstOrDefault();

            if (!(flow > 0))
            {
                flow = CanelEdges.OfType<SluiceCanalEdge>().Select(sce => sce.GetFreeFlow).FirstOrDefault();
            }

            CanalStretches.ForEach(cs => cs.Flow = flow);

            foreach (ICanalStretchModel canalStretch in CanalStretches)
            {
                CanalStretchResult canalStretchResult = new CanalStretchResult();
                canalStretchResult.CriticalSlope = canalStretch.CanalSection.GetCriticalSlope(canalStretch.Flow);
                canalStretchResult.CriticalWaterLevel = canalStretch.CanalSection.GetCriticalWaterLevel(canalStretch.Flow);
                canalStretchResult.NormalWaterLevel = canalStretch.CanalSection.GetNormalWaterLevel(canalStretch.Flow);

                double normalWaterLevelConnectedStrech;
                ICanalStretchModel preCanalStretch = CanalStretches.FirstOrDefault(cs => cs.ToNode.Id == canalStretch.FromNode.Id);
                bool preCriticalSection = false;

                if (preCanalStretch != null)
                {
                    normalWaterLevelConnectedStrech = preCanalStretch.CanalSection.GetNormalWaterLevel(preCanalStretch.Flow);

                    preCriticalSection = normalWaterLevelConnectedStrech <= canalStretchResult.CriticalWaterLevel;
                }

                ICanalStretchModel postCanalStretch = CanalStretches.FirstOrDefault(cs => cs.FromNode.Id == canalStretch.ToNode.Id);
                bool postCriticalSection = false;
                if (postCanalStretch != null)
                {
                    normalWaterLevelConnectedStrech = postCanalStretch.CanalSection.GetNormalWaterLevel(postCanalStretch.Flow);

                    postCriticalSection = normalWaterLevelConnectedStrech <= canalStretchResult.CriticalWaterLevel;
                }

                AnalysisOptions options = new AnalysisOptions();
                options.AnalysisSteps = (int)(canalStretch.Length > 10000
                    ? 10000
                    : canalStretch.Length);

                //  M flow 
                if (canalStretch.CanalSection.Slope > 0 && canalStretch.CanalSection.Slope < canalStretchResult.CriticalSlope)
                {
                    // M1
                    // Regimen lento se impone aguas abajo
                    if (canalStretch.ToNode.WaterLevel.HasValue && canalStretch.ToNode.WaterLevel.Value > canalStretchResult.CriticalWaterLevel)
                    {
                        canalStretchResult.BackwaterCurve = "M1";
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + canalStretch.Length;
                        options.FinalWaterLevel = canalStretch.ToNode.WaterLevel.Value;
                        options.BackwardsAnalysis = true;
                        options.AnalysisFeasible = true;
                    }
                    //  M2 Flow
                    // Regimen lento se impone aguas abajo
                    else if (postCriticalSection)
                    {
                        canalStretchResult.BackwaterCurve = "M2";
                        canalStretch.ToNode.WaterLevel = canalStretchResult.CriticalWaterLevel;
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + canalStretch.Length;
                        options.FinalWaterLevel = canalStretchResult.CriticalWaterLevel + Sensibility /* Salvando numéricamente por la izquierda el problema */;
                        options.BackwardsAnalysis = true;
                        options.AnalysisFeasible = true;
                    }

                    // M3 Flow
                    // Regimen rapido se impone aguas arriba
                    if (preCriticalSection || canalStretch.FromNode.WaterLevel.HasValue && canalStretch.FromNode.WaterLevel.Value < canalStretchResult.CriticalWaterLevel)
                    {
                        canalStretchResult.BackwaterCurve = "M3";
                        //  Hydraulic jump will occur as we have in the same stretch both conditions for both flows
                        options.HydraulicJumpOccurs = options.AnalysisFeasible;
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + 0.0;
                        options.BackwardsAnalysis = false;

                        if (canalStretch.FromNode.WaterLevel.HasValue)
                        {
                            options.InitialWaterLevel = canalStretch.FromNode.WaterLevel.Value;
                            options.AnalysisFeasible = true;
                        }
                    }
                }
                //  S flow
                else if (canalStretch.CanalSection.Slope > canalStretchResult.CriticalSlope)
                {
                    //  S1 Flow
                    // Regimen lento se impone aguas abajo
                    if (canalStretch.ToNode.WaterLevel.HasValue && canalStretch.ToNode.WaterLevel.Value > canalStretchResult.CriticalWaterLevel)
                    {
                        canalStretchResult.BackwaterCurve = "S1";
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + canalStretch.Length;
                        options.FinalWaterLevel = canalStretch.ToNode.WaterLevel.Value;
                        options.BackwardsAnalysis = true;
                        options.AnalysisFeasible = true;
                    }
                    // S2 flow
                    // Regimen rapido se impone aguas arriba
                    //  TODO el simbolo <= debe ser mejorado
                    else if (canalStretch.FromNode.WaterLevel.HasValue && canalStretch.FromNode.WaterLevel.Value <= canalStretchResult.CriticalWaterLevel && canalStretch.FromNode.WaterLevel.Value > canalStretchResult.NormalWaterLevel)
                    {
                        canalStretchResult.BackwaterCurve = "S2";
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + 0.0;
                        options.InitialWaterLevel = canalStretch.FromNode.WaterLevel.Value - Sensibility /* Salvando numéricamente por la derecha el problema */;
                        options.BackwardsAnalysis = false;
                        options.AnalysisFeasible = true;
                    }
                    // S3 flow
                    // Regimen rapido se impone aguas arriba
                    //  TODO el simbolo <= debe ser mejorado
                    else if (canalStretch.FromNode.WaterLevel.HasValue && canalStretch.FromNode.WaterLevel.Value <= canalStretchResult.CriticalWaterLevel && canalStretch.FromNode.WaterLevel.Value < canalStretchResult.NormalWaterLevel)
                    {
                        canalStretchResult.BackwaterCurve = "S3";
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + 0.0;
                        options.InitialWaterLevel = canalStretch.FromNode.WaterLevel.Value - Sensibility /* Salvando numéricamente por la derecha el problema */;
                        options.BackwardsAnalysis = false;
                        options.AnalysisFeasible = true;
                    }
                }
                //  H flow
                else
                {
                    // H2 flow
                    // Regimen lento se impone aguas abajo
                    //  TODO el simbolo <= debe ser mejorado
                    if (canalStretch.ToNode.WaterLevel.HasValue && canalStretch.ToNode.WaterLevel > canalStretchResult.CriticalWaterLevel)
                    {
                        canalStretchResult.BackwaterCurve = "H2";
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + canalStretch.Length;
                        options.FinalWaterLevel = canalStretch.ToNode.WaterLevel.Value;
                        options.BackwardsAnalysis = true;
                        options.AnalysisFeasible = true;
                    }
                    // H3 flow
                    // Regimen rapido se impone aguas arriba
                    //  TODO el simbolo <= debe ser mejorado
                    else if (canalStretch.FromNode.WaterLevel.HasValue && canalStretch.FromNode.WaterLevel <= canalStretchResult.CriticalWaterLevel)
                    {
                        canalStretchResult.BackwaterCurve = "H3";
                        options.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + 0.0;
                        options.InitialWaterLevel = canalStretch.FromNode.WaterLevel.Value - Sensibility /* Salvando numéricamente por la derecha el problema */;
                        options.BackwardsAnalysis = false;
                        options.AnalysisFeasible = true;
                    }
                }

                canalStretch.AnalysisOptions = options;
                canalStretch.CanalStretchResult = canalStretchResult;
                canalStretchSorted.Add(canalStretch);
            }

            return PerformAnalysis(canalStretchSorted);
        }

        private Func<double, double> GetHydraulicJumpEquation(List<CanalPointResult> downstreamAnalysisResult, List<CanalPointResult> conjugatedResult)
        {
            return x =>
            {
                List<CanalPointResult> downstreamWaterLevel = downstreamAnalysisResult.OrderBy(y => Math.Abs(y.X - x)).Take(2).OrderBy(y => y.X).ToList();
                List<CanalPointResult> conjugatedWaterLevel = conjugatedResult.OrderBy(y => Math.Abs(y.X - x)).Take(2).OrderBy(y => y.X).ToList();

                double downstremInterpolatedValue =
                    (downstreamWaterLevel.Last().WaterLevel - downstreamWaterLevel.First().WaterLevel) / (downstreamWaterLevel.Last().X - downstreamWaterLevel.First().X)
                    * (x - downstreamWaterLevel.First().X) + downstreamWaterLevel.Last().WaterLevel;

                double conjugatedInterpolatedValue =
                    (conjugatedWaterLevel.Last().WaterLevel - conjugatedWaterLevel.First().WaterLevel) / (conjugatedWaterLevel.Last().X - conjugatedWaterLevel.First().X)
                    * (x - conjugatedWaterLevel.First().X) + conjugatedWaterLevel.Last().WaterLevel;

                return downstremInterpolatedValue - conjugatedInterpolatedValue;
            };
        }

        private double GetAbsoluteInitialLength(List<ICanalStretchModel> canalStretches, ICanalStretchModel activeCanalStretch)
        {
            double length = 0d;
            ICanalStretchModel currentCanalStretch = activeCanalStretch;

            do
            {
                ICanalStretchModel priorCanalStretch = canalStretches.FirstOrDefault(cs => cs.ToNode.Id == currentCanalStretch.FromNode.Id);

                if (priorCanalStretch != null)
                {
                    length += priorCanalStretch.Length;
                }

                currentCanalStretch = priorCanalStretch;

            } while (currentCanalStretch != null);

            return length;
        }

        private CanalSimulationResult PerformAnalysis(List<ICanalStretchModel> canalStretches)
        {
            var result = new CanalSimulationResult();
            RungeKutta solver = new RungeKutta();

            //  Solving the canal
            foreach (ICanalStretchModel canalStretch in canalStretches
                         .OrderByDescending(cs => cs.AnalysisOptions.AnalysisFeasible)
                         .ThenBy(cs => cs.AnalysisOptions.InitialX))
            {
               SolveCanalStrech(canalStretch, result, solver);
            }

            return result;
        }

        private void SolveCanalStrech(ICanalStretchModel canalStretch, CanalSimulationResult result, RungeKutta solver)
        {
            //  Resetting points for safety
            canalStretch.CanalStretchResult.CanalPointResults = new List<CanalPointResult>();

            CanalStretchResult canalStretchResult = canalStretch.CanalStretchResult;
            result.CanalStretchResults[canalStretch.Id] = canalStretchResult;

            AnalysisOptions options = canalStretch.AnalysisOptions;

            double x = options.InitialX;

            if (options.InitialWaterLevel < 0 && canalStretch.FromNode.WaterLevel.HasValue)
            {
                options.InitialWaterLevel = canalStretch.FromNode.WaterLevel.Value;
            }

            if (options.FinalWaterLevel < 0 && canalStretch.ToNode.WaterLevel.HasValue)
            {
                options.FinalWaterLevel = canalStretch.ToNode.WaterLevel.Value;
            }

            double waterLevel = options.BackwardsAnalysis ? options.FinalWaterLevel : options.InitialWaterLevel;

            int steps = options.AnalysisSteps;

            solver.Interval = canalStretch.Length / steps;
            solver.Equation = canalStretch.FlowEquation();

            if (options.HydraulicJumpOccurs && canalStretch.FromNode.WaterLevel.HasValue && canalStretch.ToNode.WaterLevel.HasValue)
            {
                List<CanalPointResult> backwardsAnalysisResult = new List<CanalPointResult>();
                List<CanalPointResult> conjugateWaterLevelResult = new List<CanalPointResult>();
                List<CanalPointResult> frontAnalysisResult = new List<CanalPointResult>();
                double x2 = options.InitialX + canalStretch.Length;

                waterLevel = options.FinalWaterLevel;

                for (int i = 1; i <= steps; i++)
                {
                    waterLevel = solver.SolveBackwards(x2, waterLevel);
                    x2 = x2 - solver.Interval;

                    backwardsAnalysisResult.Add(new CanalPointResult(x2, waterLevel));
                }

                waterLevel = options.InitialWaterLevel;

                while (waterLevel < canalStretchResult.CriticalWaterLevel - Sensibility && x < options.InitialX + canalStretch.Length)
                {
                    waterLevel = solver.Solve(x, waterLevel);
                    x = x + solver.Interval;

                    conjugateWaterLevelResult.Add(new CanalPointResult(x, canalStretch.GetHydraulicJumpDownstreamDepth(waterLevel)));
                    frontAnalysisResult.Add(new CanalPointResult(x, waterLevel));
                }

                Func<double, double> conjugatedEquation = GetHydraulicJumpEquation(backwardsAnalysisResult, conjugateWaterLevelResult);
                BisectionMethod findHydraulicJump = new BisectionMethod(conjugatedEquation, options.InitialX + Sensibility, conjugateWaterLevelResult.OrderByDescending(wl => wl.X).First().X);
                double hydraulicJumpX = findHydraulicJump.Solve(0.01);

                // Found result, otherwise it could be "desagüe anegado"
                if (hydraulicJumpX < double.MaxValue)
                {
                    result.AddCanalPointResult(canalStretch.Id, options.InitialX, options.InitialWaterLevel);
                    result.AddRangeCanalPointResult(canalStretch.Id, frontAnalysisResult.Where(ar => ar.X < hydraulicJumpX).ToList());
                    result.AddRangeCanalPointResult(canalStretch.Id, backwardsAnalysisResult.Where(ar => ar.X >= hydraulicJumpX).ToList());
                }
                //  No result found, hydraulic jump must be upstream in upper stretch
                else
                {
                    canalStretch.AnalysisOptions.BackwardsAnalysis = true;
                    canalStretch.AnalysisOptions.InitialX = GetAbsoluteInitialLength(CanalStretches, canalStretch) + canalStretch.Length;
                    canalStretch.AnalysisOptions.AnalysisFeasible = true;
                    canalStretch.AnalysisOptions.HydraulicJumpOccurs = false;
                    SolveCanalStrech(canalStretch, result, solver);

                    var upstreamCanalStretch = CanalStretches.FirstOrDefault(cs => cs.ToNode == canalStretch.FromNode);
                    if(upstreamCanalStretch != null)
                    {
                        upstreamCanalStretch.AnalysisOptions.BackwardsAnalysis = false;
                        upstreamCanalStretch.AnalysisOptions.InitialX = GetAbsoluteInitialLength(CanalStretches, upstreamCanalStretch);
                        upstreamCanalStretch.AnalysisOptions.AnalysisFeasible = true;
                        upstreamCanalStretch.AnalysisOptions.HydraulicJumpOccurs = true;
                        SolveCanalStrech(upstreamCanalStretch, result, solver);
                    }
                }

            }
            else if (options.BackwardsAnalysis)
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
}
