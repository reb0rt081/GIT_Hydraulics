using System;
using System.Collections.Generic;

using ScienceAndMaths.Shared.MachineLearning;

namespace ScienceAndMaths.MachineLearning.MistakeBoundModel
{
    public class MistakeBoundConjunctionModel : IMistakeBoundConjunctionConcept, IMistakeBoundLearner<MistakeBoundChallenge>
    {
        public int Dimension { get; set; }

        public List<MistakeBoundChallenge> TrainingSet { get; set; }

        public List<MistakeBoundChallenge> ValidationSet { get; set; }

        public List<bool> Concept { get; set; }

        public MistakeBoundConjunctionModel(int dimension)
        {
            Dimension = dimension;
            List<bool> concept = new List<bool>();
            for (int i = 0; i < Dimension; i++) 
            { 
                concept.Add(true);
            }

            Concept = concept;
        }

        public void Learn(MistakeBoundChallenge challenge)
        {
            if(!challenge.Result && Predict(challenge))
            {
                for (int i = 0; i < Dimension; i++)
                {
                    if (challenge.Challenge[i])
                    {
                       Concept[i] = false;
                    }
                }
            }
        }

        public bool Predict(MistakeBoundChallenge challenge)
        {
            bool result = false;

            for (int i = 0; i < Dimension; i++)
            {
                if (challenge.Challenge[i])
                {
                    result |= Concept[i];
                }
            }

            return result;
        }

        public void Train(List<MistakeBoundChallenge> trainingSet)
        {
            foreach (MistakeBoundChallenge challenge in trainingSet)
            {
                Learn(challenge);
            }
        }
    }
}
