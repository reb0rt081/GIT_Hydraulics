using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared
{
    public interface IInterpolationElement
    {
        /// <summary>
        /// Matrix "A" for a linear interpolation of the desired solution
        /// </summary>
        /// <returns></returns>
        double[][] GetInterpolationMatrix();

        double GetInterpolationMatrixDeterminant();

        double GetElementDimension();

        /// <summary>
        /// The resultant "B" matrix when the interpolation function is derived
        /// </summary>
        /// <returns></returns>
        double[][] GetBMatrix();
    }
}
