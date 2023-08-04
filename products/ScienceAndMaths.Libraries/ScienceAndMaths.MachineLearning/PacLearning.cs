using System;
using System.Collections.Generic;
using System.Text;

namespace ScienceAndMaths.MachineLearning
{
    //  Permite deducir conceptos a través de ejemplos
    //  C: concept we have to learn
    //  L: learner
    //  H: hypothesis
    /// <summary>
    /// Model to learn an concept C by a learner L via using hypothesis H
    /// </summary>
    public class PacLearning
    {
        public int[][] TrainingSet { get; set; }

        public int[][] ValidationSet { get; set; }
    }
}
