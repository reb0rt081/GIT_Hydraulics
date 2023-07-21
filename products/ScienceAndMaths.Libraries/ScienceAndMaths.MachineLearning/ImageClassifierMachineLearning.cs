using System;

using ScienceAndMaths_MachineLearning;
using static ScienceAndMaths_MachineLearning.MLModel;

namespace ScienceAndMaths.MachineLearning
{
    public class ImageClassifierMachineLearning
    {
        /// <summary>
        /// @"D:\rbo\Pictures\ML\Eslovenia\IMG-20230527-WA0000.jpeg"
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public ModelOutput ClassifyImage(string fullPath)
        {
            //Load sample data
            var sampleData = new MLModel.ModelInput()
            {
                ImageSource = fullPath
            };

            //Load model and predict output
            return MLModel.Predict(sampleData);
        }
        
    }
}
