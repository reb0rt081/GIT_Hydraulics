using System;

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
            var sampleData = new AgbarMLModelBasic.ModelInput()
            {
                Postcode = "8010",
                Month = "1",
                Day_of_week = "7",
            };
            //Load model and predict output
            var result1 = AgbarMLModelBasic.Predict(sampleData);

            sampleData.Day_of_week = "1";
            var result2 = AgbarMLModelBasic.Predict(sampleData);

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

        [TestMethod]
        public void TestFinalAgbarModel()
        {
            //Load sample data
            var sampleData = new AgbarMLModel1.ModelInput()
            {
                Census_section= "0801902067",
                Month = @"7",
                Day_of_week = @"7",
                Population_higher16_rate = 85.51f,
                Population_highschool_rate = 40,
                Housing_biggerSize_rate = 1.64f,
                PERCENTAGE_RESERVOIR = 86.4f,
                Income_percapita = 22299,
            };

            //Load model and predict output
            //Load model and predict output
            var result = AgbarMLModel1.Predict(sampleData);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Score > -60 && result.Score < 0, $"Normalized consumption is : {result.Score}");
            Console.WriteLine($"Normalized consumption is : {result.Score/100}");
        }
    }
}