using ScienceAndMaths.Shared.MachineLearning;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public DecisionTreeNode RootNode { get; protected set; }        
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
            List<NodeErrorRate> errorList = new List<NodeErrorRate>();
            foreach (DecisionTreeNode node in Concept.Values.Where(node => !node.IsLeaf))
            {
                RootNode = node;
                errorList.Add(new NodeErrorRate(node, ErrorRate(trainingSet)));
            }

            NodeErrorRate bestCurrentNode = errorList.OrderBy(ol => ol.ErrorRate).First();
            RootNode = bestCurrentNode.Node;
            RootNode.IsLeaf = true;

            while (Concept.Values.Any(node => !node.IsLeaf))
            {
                NodeErrorRate bestLeftNode;
                bool leftNodeFound = TryGetBestNexLeftTreeNode(bestCurrentNode.Node, bestCurrentNode.ErrorRate,
                    Concept.Values.Where(node => !node.IsLeaf).ToList(), trainingSet, out bestLeftNode);

                NodeErrorRate bestRightNode;
                bool rightNodeFound = TryGetBestNextRightTreeNode(bestCurrentNode.Node, bestCurrentNode.ErrorRate,
                    Concept.Values.Where(node => !node.IsLeaf).ToList(), trainingSet, out bestRightNode);

                if (leftNodeFound)
                {
                    bestCurrentNode.Node.LeftNode = bestLeftNode.Node;
                    bestCurrentNode.Node.LeftNode.IsLeaf = true;
                    bestCurrentNode = bestLeftNode;
                }
                else if (rightNodeFound)
                {
                    bestCurrentNode.Node.RightNode = bestRightNode.Node;
                    bestCurrentNode.Node.RightNode.IsLeaf = true;
                    bestCurrentNode = bestRightNode;
                }
                else
                {
                    //  TODO check if by the time we have arrived here we have skipped some nodes
                    break;
                }
            }
            
        }

        public bool TryGetBestNextRightTreeNode(DecisionTreeNode currentNode, double currentErrorRate, List<DecisionTreeNode> nodes, List<MistakeBoundChallenge> trainingSet, out NodeErrorRate bestNextNode)
        {
            List<NodeErrorRate> errorList = new List<NodeErrorRate>();
            foreach (DecisionTreeNode node in nodes)
            {
                currentNode.RightNode = node;
                errorList.Add(new NodeErrorRate(node, ErrorRate(trainingSet)));
            }

            currentNode.RightNode = null;
            
            NodeErrorRate bestNode = errorList.OrderBy(ol => ol.ErrorRate).First();
            if (bestNode.ErrorRate < currentErrorRate)
            {
                bestNextNode = bestNode;
                bestNextNode.Node.IsLeaf = true;
                return true;
            }
            else
            {
                bestNextNode = null;
                return false;
            }

        }

        public bool TryGetBestNexLeftTreeNode(DecisionTreeNode currentNode, double currentErrorRate, List<DecisionTreeNode> nodes, List<MistakeBoundChallenge> trainingSet, out NodeErrorRate bestNextNode)
        {
            List<NodeErrorRate> errorList = new List<NodeErrorRate>();
            foreach (DecisionTreeNode node in nodes)
            {
                currentNode.LeftNode = node;
                errorList.Add(new NodeErrorRate(node, ErrorRate(trainingSet)));
            }

            currentNode.LeftNode = null;

            NodeErrorRate bestNode = errorList.OrderBy(ol => ol.ErrorRate).First();
            if (bestNode.ErrorRate < currentErrorRate)
            {
                bestNextNode = bestNode;
                bestNextNode.Node.IsLeaf = true;
                return true;
            }
            else
            {
                bestNextNode = null;
                return false;
            }

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
                bool result = Predict(challenge);
                if (result != challenge.Result)
                {
                    errors++;
                }
            }

            return (double) errors / validationSet.Count * 100;
        }

        public double NegativeGiniRate(List<MistakeBoundChallenge> validationSet, double oldGain, DecisionTreeNode node)
        {
            int negativeExamples = validationSet.Count(vs => !vs.Challenge[node.Id]);
            int positiveExamples = validationSet.Count(vs => vs.Challenge[node.Id]);
            return (double) negativeExamples / validationSet.Count * GiniIndex((double) validationSet.Count(vs => !vs.Result && !vs.Challenge[node.Id]) / negativeExamples)
                + (double) positiveExamples / validationSet.Count * GiniIndex((double) validationSet.Count(vs => !vs.Result && vs.Challenge[node.Id]) / positiveExamples);
        }

        /// <summary>
        /// Returns the gini index for certin value
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public double GiniIndex(double a)
        {
            return 2 * a * (1 - a);
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
