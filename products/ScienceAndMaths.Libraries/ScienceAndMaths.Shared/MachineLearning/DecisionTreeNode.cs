namespace ScienceAndMaths.Shared.MachineLearning
{
    /// <summary>
    /// Decision tree node where FALSE answers go to LEFT node and TRUE answers go RIGHT.
    /// </summary>
    public class DecisionTreeNode
    {
        /// <summary>
        /// Unique Id for this decision tree node that corresponds to the position of the input data.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Next node in the decision tree if answer is FALSE.
        /// </summary>
        public DecisionTreeNode LeftNode { get; set; }

        /// <summary>
        /// Next node in the decision tree if answer is TRUE
        /// </summary>
        public DecisionTreeNode RightNode { get; set; }

        public DecisionTreeNode(int id)
        {
            Id = id;
        }
    }
}
