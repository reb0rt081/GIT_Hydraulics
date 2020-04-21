﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Mathematics.FEM
{
    /// <summary>
    /// For 2D problems this represents a 2D element where we want to find a function u(x,y) and v(x,y) in a discrete set of elements ({u1, v1}, {u2, v2} {u3, v3})
    /// </summary>
    public class TriangularElement : IInterpolationElement
    {
        public TriangularElement(Node node1, Node node2, Node node3)
        {
            Vertex1 = node1;

            Vertex2 = node2;

            Vertex3 = node3;
        }

        public double[][] DMatrix { get; private set; }

        public double Thickness { get; set; }

        public Node Vertex1 { get; set; }

        public Node Vertex2 { get; set; }

        public Node Vertex3 { get; set; }

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
        /// [1  x1  y1]
        /// [1  x2  y2]
        /// [1  x3  y3]
        /// </summary>
        /// <returns></returns>
        public double[][] GetInterpolationMatrix()
        {
            var matrix = MatrixOperations.MatrixCreate(3, 3);

            matrix[0][0] = 1;
            matrix[0][1] = Vertex1.X;
            matrix[0][2] = Vertex1.Y;

            matrix[1][0] = 1;
            matrix[1][1] = Vertex2.X;
            matrix[1][2] = Vertex2.Y;

            matrix[2][0] = 1;
            matrix[2][1] = Vertex3.X;
            matrix[2][2] = Vertex3.Y;

            return matrix;
        }

        public double GetInterpolationMatrixDeterminant()
        {
            var matrix = GetInterpolationMatrix();

            return (matrix[0][0] * matrix[1][1] * matrix[2][2] + matrix[0][1] * matrix[1][2] * matrix[2][0] +
                    matrix[1][0] * matrix[2][1] * matrix[0][2]) -
                   (matrix[0][2] * matrix[1][1] * matrix[2][0] + matrix[1][0] * matrix[0][1] * matrix[2][2] +
                    matrix[2][1] * matrix[1][2] * matrix[0][0]);
        }

        public double GetElementDimension()
        {
            return 0.5 * GetInterpolationMatrixDeterminant();
        }

        /// <summary>
        /// The resultant "B" matrix when the interpolation function is derived
        /// du/dx = 1/|A| * [y2 - y3    0   y3 - y1 0   y1 - y2 0] * [u1 v1 u2 v2 u3 v3] -> du/dx = B * [u1 v1 u2 v2 u3 v3]
        /// dv/dy = 1/|A| * [0      -(x2 - x3)    0   -(x3 - x1)    0   -(x1 -x2)] * [u1 v1 u2 v2 u3 v3] -> dv/dy = B * [u1 v1 u2 v2 u3 v3]
        /// 0.5 * (du/dy + dv/dx) = 1/|A| * [-(x2 - x3)    y2-y3   -(x3 - x1)    y3 - y1   -(x1 -x2)    y1 - y2] * [u1 v1 u2 v2 u3 v3] -> dv/dy = B * [u1 v1 u2 v2 u3 v3]
        /// </summary>
        /// <returns></returns>
        public double[][] GetBMatrix()
        {
            var matrix = MatrixOperations.MatrixCreate(3, 6);
            double determinant = GetInterpolationMatrixDeterminant();

            matrix[0][0] = (Vertex2.Y - Vertex3.Y) / determinant;
            matrix[0][1] = 0.0;
            matrix[0][2] = (Vertex3.Y - Vertex1.Y) / determinant;
            matrix[0][3] = 0.0;
            matrix[0][4] = (Vertex1.Y - Vertex2.Y) / determinant;
            matrix[0][5] = 0.0;

            matrix[1][0] = 0.0;
            matrix[1][1] = -(Vertex2.X - Vertex3.X) / determinant;
            matrix[1][2] = 0.0;
            matrix[1][3] = -(Vertex3.X - Vertex1.X) / determinant;
            matrix[1][4] = 0.0;
            matrix[1][5] = -(Vertex1.X - Vertex2.X) / determinant;

            matrix[2][0] = -(Vertex2.X - Vertex3.X) / determinant;
            matrix[2][1] = (Vertex2.Y - Vertex3.Y) / determinant;
            matrix[2][2] = -(Vertex3.X - Vertex1.X) / determinant;
            matrix[2][3] = (Vertex3.Y - Vertex1.Y) / determinant;
            matrix[2][4] = -(Vertex1.X - Vertex2.X) / determinant;
            matrix[2][5] = (Vertex1.Y - Vertex2.Y) / determinant;

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
