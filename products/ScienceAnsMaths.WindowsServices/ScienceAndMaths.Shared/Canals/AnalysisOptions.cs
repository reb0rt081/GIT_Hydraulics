namespace ScienceAndMaths.Shared.Canals
{
    public class AnalysisOptions
    {
        public AnalysisOptions()
        {
            InitialX = 0d;
            InitialWaterLevel = 0d;
            FinalWaterLevel = 0d;
            BackwardsAnalysis = false;
            AnalysisFeasible = false;
            AnalysisSteps = 0;
        }

        public double InitialX { get; set; }

        public double InitialWaterLevel { get; set; }

        public double FinalWaterLevel { get; set; }

        public bool BackwardsAnalysis { get; set; }

        public bool AnalysisFeasible { get; set; }

        public int AnalysisSteps { get; set; }

        public bool HydraulicJumpOccurs { get; set; }
    }
}
