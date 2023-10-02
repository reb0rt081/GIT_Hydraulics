using System;
using System.Collections.Generic;
using System.Text;

namespace ScienceAndMaths.Shared.MachineLearning
{
    public class NodeErrorRate
    {
        public NodeErrorRate() { }
        public NodeErrorRate(DecisionTreeNode node, double errorRate)
        {
            Node = node;
            ErrorRate = errorRate;
        }
        public DecisionTreeNode Node { get; set; }

        public double ErrorRate { get; set; }
    }
}
