using System;
using System.Collections.Generic;
using System.Text;

namespace ScienceAndMaths.Shared.MachineLearning
{
    public interface IMistakeBoundLearner<TChallenge>
    {
        void Train(List<TChallenge> trainingSet);
    }
}
