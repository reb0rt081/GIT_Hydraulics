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
        private MistakeBoundConjunctionModel mistakeBoundConjunctionModel;
        [TestInitialize]
        public void Initialize()
        {
            mistakeBoundConjunctionModel = new MistakeBoundConjunctionModel(2);
        }

        [TestMethod]
        public void PredictModelTest()
        {
            bool result = mistakeBoundConjunctionModel.Predict(new MistakeBoundChallenge
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
    }
}
