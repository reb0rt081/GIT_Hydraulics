using ScienceAndMaths.Shared.MachineLearning;
using System;
using System.Collections.Generic;

namespace ScienceAndMaths.MachineLearning.MistakeBoundModel
{
    /// <summary>
    /// 
    /// </summary>
    public class DecisionTreeModel : IMistakeBoundConjunctionConcept, IMistakeBoundLearner<MistakeBoundChallenge>
    {
        /// <summary>
        /// Number of nodes (variables) in the decision tree.
        /// </summary>
        public int Size { get; }

        public DecisionTreeNode RootNode { get;  }        
        /// <summary>
        /// Length of the longest path from root to leaf.
        /// </summary>
        public int Depth { get; }

        public List<MistakeBoundChallenge> TrainingSet { get; protected set; }

        public List<MistakeBoundChallenge> ValidationSet { get; protected set; }


        public Dictionary<int, DecisionTreeNode> Concept;

        public DecisionTreeModel(int size)
        {
            Size = size;
            //  Let us create the initial
            Concept = new Dictionary<int, DecisionTreeNode>();
            for (int i = 0; i < Size; i++)
            {
                Concept.Add(i, new DecisionTreeNode(i));
            }

            RootNode = Concept[0];
        }

        public bool Predict(MistakeBoundChallenge challenge)
        {
            DecisionTreeNode nodeToEvaluate = RootNode;

            return TravelDecisionTree(nodeToEvaluate, challenge);
        }

        public void Learn(MistakeBoundChallenge challenge)
        {
            throw new NotImplementedException();
        }

        public void Train(List<MistakeBoundChallenge> trainingSet)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the error rate in percentage
        /// </summary>
        /// <param name="validationSet"></param>
        /// <returns></returns>
        public double ErrorRate(List<MistakeBoundChallenge> validationSet)
        {
            int errors = 0;
            foreach (MistakeBoundChallenge challenge in validationSet)
            {
                if (Predict(challenge) != challenge.Result)
                {
                    errors++;
                }
            }

            return (double) errors / validationSet.Count * 100;
        }

        public bool TravelDecisionTree(DecisionTreeNode initialNode, MistakeBoundChallenge data)
        {
            DecisionTreeNode nodeToEvaluate = initialNode;
            int i = 0;
            while (i < Size || nodeToEvaluate != null)
            {
                if (data.Challenge[nodeToEvaluate.Id])
                {
                    //  If true we check the right (TRUE) side
                    if (nodeToEvaluate.RightNode != null)
                    {
                        nodeToEvaluate = nodeToEvaluate.RightNode;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    //  If false we check the left (FALSE) side
                    if (nodeToEvaluate.LeftNode != null)
                    {
                        nodeToEvaluate = nodeToEvaluate.LeftNode;
                    }
                    else
                    {
                        return false;
                    }
                }

                i++;
            }

            return false;
        }
    }
}
