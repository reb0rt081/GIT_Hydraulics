using ScienceAndMaths.Shared.MachineLearning;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScienceAndMaths.MachineLearning.MistakeBoundModel
{
    /// <summary>
    /// Mistake bound model that tries to learn the concept of Monotone Disjunction with negations: x1 || !x2
    /// </summary>
    public class MistakeBoundDisjunctionExtensionModel : MistakeBoundDisjunctionModel
    {
        public MistakeBoundDisjunctionExtensionModel(int dimension)
            : base(2*dimension)
        {
            List<bool> concept = new List<bool>();
            for (int i = 0; i < Dimension; i++)
            {
                concept.Add(true);
            }

            Concept = concept;
        }

        public override bool Predict(MistakeBoundChallenge challenge)
        {
            return base.Predict(ExtendChallenge(challenge));
        }

        public override void Learn(MistakeBoundChallenge challenge)
        {
            base.Learn(ExtendChallenge(challenge));
        }

        public override void Train(List<MistakeBoundChallenge> trainingSet)
        {
            List<MistakeBoundChallenge> extendedTrainingSet = new List<MistakeBoundChallenge>();
            foreach (var challenge in trainingSet)
            {
                extendedTrainingSet.Add(ExtendChallenge(challenge));
            }

            base.Train(extendedTrainingSet);
        }

        public override double ErrorRate(List<MistakeBoundChallenge> trainingSet)
        {
            List<MistakeBoundChallenge> extendedTrainingSet = new List<MistakeBoundChallenge>();
            foreach (var challenge in trainingSet)
            {
                extendedTrainingSet.Add(ExtendChallenge(challenge));
            }

            return base.ErrorRate(extendedTrainingSet);
        }

        private static MistakeBoundChallenge ExtendChallenge(MistakeBoundChallenge challenge)
        {
            MistakeBoundChallenge extensionChallenge = new MistakeBoundChallenge();
            extensionChallenge.Challenge = new List<bool>();
            extensionChallenge.Result = challenge.Result;

            foreach (var data in challenge.Challenge)
            {
                extensionChallenge.Challenge.Add(data);
                extensionChallenge.Challenge.Add(!data);
            }

            return extensionChallenge;
        }
    }
}
