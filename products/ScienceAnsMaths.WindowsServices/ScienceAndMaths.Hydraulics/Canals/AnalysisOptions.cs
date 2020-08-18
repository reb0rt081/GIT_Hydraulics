using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Hydraulics.Canals
{
    public class AnalysisOptions
    {
        public AnalysisOptions()
        {
            InitialX = 0d;
            InitialWaterLevel = 0d;
            BackwardsAnalysis = false;
            ExecuteAnalysis = false;
            AnalysisSteps = 0;
        }

        public double InitialX { get; set; }

        public double InitialWaterLevel { get; set; }

        public bool BackwardsAnalysis { get; set; }

        public bool ExecuteAnalysis { get; set; }

        public int AnalysisSteps { get; set; }
    }
}
