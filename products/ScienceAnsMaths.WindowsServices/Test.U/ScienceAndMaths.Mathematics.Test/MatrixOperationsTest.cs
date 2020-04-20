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
    }
}
