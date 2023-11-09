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
            var sampleData = new AgbarMLModel1.ModelInput()
            {
                Dia_semana = 4,
                Mes = 12,
                Año = 2020,
                Census_section = 101220,
                Postcode = 8010
            };

            //Load model and predict output
            var result = AgbarMLModel1.Predict(sampleData);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Score > 180 && result.Score < 200);
        }
    }
}