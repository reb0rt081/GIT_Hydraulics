using ScienceAndMaths.Shared.MachineLearning;
using System;
using System.Collections.Generic;
using Tensorflow;
using Tensorflow.Contexts;

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
        }

        public bool Predict(MistakeBoundChallenge challenge)
        {
            int i = 0;
            DecisionTreeNode nodeToEvaluate = Concept[i];
            while (i < Size || nodeToEvaluate != null)
            {
                if (challenge.Challenge[nodeToEvaluate.Id])
                {
                    //  If true we check the right (TRUE) side
                    if(nodeToEvaluate.RightNode != null)
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
                    if(nodeToEvaluate.LeftNode != null)
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

        public void Learn(MistakeBoundChallenge challenge)
        {
            throw new NotImplementedException();
        }

        public void Train(List<MistakeBoundChallenge> trainingSet)
        {
            throw new NotImplementedException();
        }
    }
}
