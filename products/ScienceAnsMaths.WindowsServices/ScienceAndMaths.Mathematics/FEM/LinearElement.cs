﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Mathematics.FEM
{
    /// <summary>
    /// For 1D problems this represents a 1D element where we want to find a function u(x) in a discrete set of elements (u1, u2)
    /// </summary>
    public class LinearElement : IInterpolationElement
    {
        public LinearElement(Node node1, Node node2)
        {
            Vertex1 = node1;

            Vertex2 = node2;
        }

        public double[][] DMatrix { get; private set; }

        public double SectionArea { get; set; }

        public Node Vertex1 { get; set; }

        public Node Vertex2 { get; set; }

        /// <summary>
        /// Sets the matrix that relates two functions {A(x,y,z)} = [D] · {B(x,y,z)}
        /// </summary>
        /// <param name="dMatrix"></param>
        public void SetDMatrix(double[][] dMatrix)
        {
            DMatrix = dMatrix;
        }

        /// <summary>
        /// Matrix "A" for a linear interpolation of the desired solution
        /// [1  x1]
        /// [1  x2]
        /// </summary>
        /// <returns></returns>
        public double[][] GetInterpolationMatrix()
        {
            var matrix = MatrixOperations.MatrixCreate(2, 2);

            matrix[0][0] = 1;
            matrix[0][1] = Vertex1.X;

            matrix[1][0] = 1;
            matrix[1][1] = Vertex2.X;

            return matrix;
        }

        public double GetInterpolationMatrixDeterminant()
        {
            var matrix = GetInterpolationMatrix();

            return matrix[0][0] * matrix[1][1] - matrix[1][0] * matrix[0][1];
        }

        public double GetElementDimension()
        {
            return GetInterpolationMatrixDeterminant();
        }

        /// <summary>
        /// The resultant "B" matrix when the interpolation function is derived
        /// du/dx = 1/|A| * [1  1] * [u1    u2] -> du/dx = B * [u1 u2]
        /// </summary>
        /// <returns></returns>
        public double[][] GetBMatrix()
        {
            var matrix = MatrixOperations.MatrixCreate(1, 2);
            double determinant = GetInterpolationMatrixDeterminant();

            matrix[0][0] = -1.0 / determinant;
            matrix[0][1] = 1.0 / determinant;

            return matrix;
        }

        /// <summary>
        /// The resultant "K" matrix as [K] = [B]^T · [D] · [B] · ElementDimension
        /// du/dx = 1/|A| * [1  1] * [u1    u2] -> du/dx = B * [u1 u2]
        /// </summary>
        /// <returns></returns>
        public double[][] GetKMatrix()
        {
            double el1Area = GetElementDimension();

            double[][] el1BMatrix = GetBMatrix();

            double[][] el1BMatrixTranspose = el1BMatrix.MatrixTranspose();

            double[][] el1Product1 = el1BMatrixTranspose.MatrixProduct(DMatrix);

            double[][] el1Product2 = el1Product1.MatrixProduct(el1BMatrix);

            double[][] k1 = el1Product2.MatrixProductByConstant(el1Area);

            return k1;
        }
    }
}
