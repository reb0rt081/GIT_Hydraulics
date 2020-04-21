using ScienceAndMaths.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Mathematics.FEM
{
    public abstract class BaseInterpolationElement : IInterpolationElement
    {
        public double[][] DMatrix { get; private set; }

        public abstract double[][] GetBMatrix();

        public abstract double GetElementDimension();

        public abstract double[][] GetInterpolationMatrix();
      
        public virtual double GetInterpolationMatrixDeterminant()
        {
            var matrix = GetInterpolationMatrix();

            return matrix.MatrixDeterminant();
        }

        /// <summary>
        /// The resultant "K" matrix as [K] = [B]^T · [D] · [B] · ElementDimension
        /// du/dx = 1/|A| * [1  1] * [u1    u2] -> du/dx = B * [u1 u2]
        /// </summary>
        /// <returns></returns>
        public virtual double[][] GetKMatrix()
        {
            double el1Area = GetElementDimension();

            double[][] el1BMatrix = GetBMatrix();

            double[][] el1BMatrixTranspose = el1BMatrix.MatrixTranspose();

            double[][] el1Product1 = el1BMatrixTranspose.MatrixProduct(DMatrix);

            double[][] el1Product2 = el1Product1.MatrixProduct(el1BMatrix);

            double[][] kMatrix = el1Product2.MatrixProductByConstant(el1Area);

            return kMatrix;
        }

        public virtual void SetDMatrix(double[][] dMatrix)
        {
            DMatrix = dMatrix;
        }
    }
}
