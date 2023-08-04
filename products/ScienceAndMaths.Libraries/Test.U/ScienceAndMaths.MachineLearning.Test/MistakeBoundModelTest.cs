using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScienceAndMaths.MachineLearning.MistakeBoundModel;
using ScienceAndMaths.Shared.MachineLearning;

namespace ScienceAndMaths.MachineLearning.Test
{
    /// <summary>
    /// Test for mistake bound models
    /// </summary>
    [TestClass]
    public class MistakeBoundModelTest
    {
        private MistakeBoundDisjunctionModel mistakeBoundDisjunctionModel;
        [TestInitialize]
        public void Initialize()
        {
            mistakeBoundDisjunctionModel = new MistakeBoundDisjunctionModel(2);
        }

        [TestMethod]
        public void PredictModelTest()
        {
            bool result = mistakeBoundDisjunctionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    true,
                    false
                },
                Result = true
            });

            Assert.IsTrue(result);
        }

        /// <summary>
        /// Prepares a training set to validate a monotone disjunction example that should be validated
        /// Test variables: {x1, x2}
        /// Expected result: {x1}, result does not depend on value of x2
        /// Test data:
        /// {0, 0} = 0
        /// {0, 1} = 0
        /// {1, 0} = 1
        /// {1, 1} = 1
        /// </summary>
        [TestMethod]
        public void TrainModelTest()
        {
            List<MistakeBoundChallenge> trainingSet = new List<MistakeBoundChallenge>
            {
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        false
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        false
                    },
                    Result = true
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        false,
                        true
                    },
                    Result = false
                },
                new MistakeBoundChallenge
                {
                    Challenge = new List<bool>
                    {
                        true,
                        true
                    },
                    Result = true
                }
            };

            mistakeBoundDisjunctionModel.Train(trainingSet);

            Assert.IsFalse(mistakeBoundDisjunctionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    false,
                    false
                },
                Result = true
            }));
            Assert.IsFalse(mistakeBoundDisjunctionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    false,
                    true
                },
                Result = true
            }));

            Assert.IsTrue(mistakeBoundDisjunctionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    true,
                    false
                },
                Result = false
            }));
            Assert.IsTrue(mistakeBoundDisjunctionModel.Predict(new MistakeBoundChallenge
            {
                Challenge = new List<bool>
                {
                    true,
                    true
                },
                Result = false
            }));
        }
    }
}
