using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScienceAndMaths_MachineLearning;

namespace ScienceAndMaths.MachineLearning.Test
{
    [TestClass]
    public class MachineLearningModelTest
    {
        [TestMethod]
        public void TestImage()
        {
            ImageClassifierMachineLearning model = new ImageClassifierMachineLearning();
            var result = model.ClassifyImage(@"./Picture/plaza_mayor_de_madrid_0.jpg");
            Assert.IsNotNull(result);
            Assert.AreEqual("Madrid", result.Prediction);
        }

        [TestMethod]
        public void TestAgbar()
        {
            //Load sample data
            //Load sample data
            var sampleData = new AgbarMLModel1.ModelInput()
            {
                Postcode = "8001",
                Month = "1",
                Day_of_week = "7",
            };

            //Load model and predict output
            //var sampleData = new AgbarMLModel1.ModelInput
            //{
            //    Day_week = 4,
            //    Month = 12,
            //    Year = 2020,
            //    Census_section = 101220,
            //    Postcode = 8010
            //};

            //Load model and predict output
            var result1 = AgbarMLModel1.Predict(sampleData);

            sampleData.Day_of_week = "1";
            var result2 = AgbarMLModel1.Predict(sampleData);

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsTrue(result2.Score > result1.Score);
        }

        [TestMethod]
        public void TestAgbarTestModel()
        {
            var sampleData = new AgbarMLModelTest.ModelInput()
            {
                Day_week = "4",
                Month = "12",
                Year = 2020,
                Census_section = "101220",
                Postcode = "8010",
            };

            //Load model and predict output
            var result = AgbarMLModelTest.Predict(sampleData);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Score > 180 && result.Score < 200);

        }
    }
}