using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Mathematics.FEM
{
    public class Node : INode
    {
        public Node(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Node(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Node(double x)
        {
            X = x;
        }

        /// <summary>
        /// Coordinate X of the node
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Coordinate Y of the node
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Coordinate Z of the node
        /// </summary>
        public double Z { get; set; }
    }
}
