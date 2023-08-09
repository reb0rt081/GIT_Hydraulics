using System;
using System.Collections.Generic;

using ScienceAndMaths.Shared.MachineLearning;

namespace ScienceAndMaths.MachineLearning.MistakeBoundModel
{
    /// <summary>
    /// Mistake bound model that tries to learn the concept of Monotone Disjunction: x1 || x2
    /// </summary>
    public class MistakeBoundDisjunctionModel : IMistakeBoundConjunctionConcept, IMistakeBoundLearner<MistakeBoundChallenge>
    {
        public int Dimension { get; }

        public List<MistakeBoundChallenge> TrainingSet { get; protected set; }

        public List<MistakeBoundChallenge> ValidationSet { get; protected set; }

        public List<bool> Concept { get; protected set; }

        public MistakeBoundDisjunctionModel(int dimension)
        {
            Dimension = dimension;
            List<bool> concept = new List<bool>();
            for (int i = 0; i < Dimension; i++)
            {
                concept.Add(true);
            }

            Concept = concept;
        }

        public virtual void Learn(MistakeBoundChallenge challenge)
        {
            if (!challenge.Result && Predict(challenge))
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

        public virtual bool Predict(MistakeBoundChallenge challenge)
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

        public virtual void Train()
        {
            Train(TrainingSet);
        }

        public virtual void Train(List<MistakeBoundChallenge> trainingSet)
        {
            foreach (MistakeBoundChallenge challenge in trainingSet)
            {
                Learn(challenge);
            }
        }
    }
}
