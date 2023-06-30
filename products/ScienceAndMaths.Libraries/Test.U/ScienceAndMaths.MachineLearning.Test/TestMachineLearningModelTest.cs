using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScienceAndMaths.MachineLearning.Test
{
    [TestClass]
    public class TestMachineLearningModelTest
    {
        [TestMethod]
        public void TestImage()
        {
            TestMachineLearningModel model = new TestMachineLearningModel();
            var result = model.ClassifyImage(@"./Picture/plaza_mayor_de_madrid_0.jpg");
            Assert.IsNotNull(result);
            Assert.AreEqual("Madrid", result.Prediction);
        }
    }
}