using System;
using System.Collections.Generic;
using System.Text;

namespace ScienceAndMaths.Shared.MachineLearning
{
    public interface IMistakeBoundConcept<TChallenge, TResult>
    {
        TResult Predict(TChallenge challenge);

        void Learn(TChallenge challenge);
    }
}
