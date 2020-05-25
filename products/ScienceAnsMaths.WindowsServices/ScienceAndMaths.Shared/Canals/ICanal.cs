using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared.Canals
{
    public interface ICanal
    {
        /// <summary>
        /// Gets or sets the unique identifier for the canal.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Gets or sets the canal edges to define the boundary conditions.
        /// </summary>
        List<ICanalEdge> CanelEdges { get; set; }


        /// <summary>
        /// Gets or sets the canal homogeneous sections.
        /// </summary>
        List<ICanalStretchModel> CanalStretches { get; set; }
    }
}
