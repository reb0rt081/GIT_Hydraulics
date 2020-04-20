using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared
{
    public interface INode
    {
        /// <summary>
        /// Coordinate X of the node
        /// </summary>
        double X { get; set; }

        /// <summary>
        /// Coordinate Y of the node
        /// </summary>
        double Y { get; set; }

        /// <summary>
        /// Coordinate Z of the node
        /// </summary>
        double Z { get; set; }
    }
}
