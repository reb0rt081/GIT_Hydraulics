using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared
{
    public interface IInterpolationElement
    {
        /// <summary>
        /// Sets the matrix that relates two functions {A(x,y,z)} = [D] · {B(x,y,z)}
        /// </summary>
        /// <param name="dMatrix"></param>
        void SetDMatrix(double[][] dMatrix); 

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

        /// <summary>
        /// The resultant "K" matrix as [K] = [B]^T · [D] · [B] · ElementDimension
        /// du/dx = 1/|A| * [1  1] * [u1    u2] -> du/dx = B * [u1 u2]
        /// </summary>
        /// <returns></returns>
        double[][] GetKMatrix();
    }
}
