using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Mathematics.Test
{
    [TestClass]
    public class MatrixOperationsTest
    {
        [TestMethod]
        public void MatrixOrder2Test()
        {
            double[][] matrix = MatrixOperations.MatrixCreate(2, 2);
            matrix[0][0] = 1.0;
            matrix[0][1] = 2.0;
            matrix[1][0] = -1.0;
            matrix[1][1] = 2.0;

            double det = matrix.MatrixDeterminant();

            Assert.AreEqual(matrix[0][0] * matrix[1][1] - matrix[1][0] * matrix[0][1], det);
        }

        [TestMethod]
        public void MatrixOrder3Test()
        {
            double[][] matrix = MatrixOperations.MatrixCreate(3, 3);
            matrix[0][0] = 1.0;
            matrix[0][1] = 2.0;
            matrix[0][2] = -2.0;

            matrix[1][0] = 1.0;
            matrix[1][1] = -2.0;
            matrix[1][2] = 5.0;

            matrix[2][0] = -8.0;
            matrix[2][1] = 5.0;
            matrix[2][2] = 4.0;

            double det = matrix.MatrixDeterminant();

            Assert.AreEqual((matrix[0][0] * matrix[1][1] * matrix[2][2] + matrix[0][1] * matrix[1][2] * matrix[2][0] +
                        matrix[1][0] * matrix[2][1] * matrix[0][2]) -
                       (matrix[0][2] * matrix[1][1] * matrix[2][0] + matrix[1][0] * matrix[0][1] * matrix[2][2] +
                        matrix[2][1] * matrix[1][2] * matrix[0][0]), det);
        }

        [TestMethod]
        public void MatrixOrder4Test()
        {
            double[][] matrix = MatrixOperations.MatrixCreate(4, 4);
            matrix[0][0] = 1.0;
            matrix[0][1] = 2.0;
            matrix[0][2] = 1.0;
            matrix[0][3] = 0.0;

            matrix[1][0] = 0.0;
            matrix[1][1] = 3.0;
            matrix[1][2] = 1.0;
            matrix[1][3] = 1.0;

            matrix[2][0] = -1.0;
            matrix[2][1] = 0.0;
            matrix[2][2] = 3.0;
            matrix[2][3] = 1.0;

            matrix[3][0] = 3.0;
            matrix[3][1] = 1.0;
            matrix[3][2] = 2.0;
            matrix[3][3] = 0.0;

            double det = matrix.MatrixDeterminant();

            Assert.AreEqual(16.0, det);
        }

        [TestMethod]
        public void MatrixOrder4AnotherTest()
        {
            double[][] matrix = MatrixOperations.MatrixCreate(4, 4);
            matrix[0][0] = 4.0;
            matrix[0][1] = 3.0;
            matrix[0][2] = 2.0;
            matrix[0][3] = 2.0;

            matrix[1][0] = 0.0;
            matrix[1][1] = 1.0;
            matrix[1][2] = -3.0;
            matrix[1][3] = 3.0;

            matrix[2][0] = 0.0;
            matrix[2][1] = -1.0;
            matrix[2][2] = 3.0;
            matrix[2][3] = 3.0;

            matrix[3][0] = 0.0;
            matrix[3][1] = 3.0;
            matrix[3][2] = 1.0;
            matrix[3][3] = 1.0;

            double det = matrix.MatrixDeterminant();

            Assert.AreEqual(-240.0, det);
        }
    }
}
