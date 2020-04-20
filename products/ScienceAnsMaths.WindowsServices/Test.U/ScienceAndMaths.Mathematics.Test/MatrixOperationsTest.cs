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

        [TestMethod]
        public void MatrixInverseTest()
        {
            double[][] matrix = MatrixOperations.MatrixCreate(3, 3);
            matrix[0][0] = 1.0;
            matrix[0][1] = -1.0;
            matrix[0][2] = 0.0;

            matrix[1][0] = 0.0;
            matrix[1][1] = 1.0;
            matrix[1][2] = 0.0;

            matrix[2][0] = 2.0;
            matrix[2][1] = 0.0;
            matrix[2][2] = 1.0;

            double[][] inv = matrix.MatrixInverse();

            var identity = MatrixOperations.MatrixIdentity(3);

            double[][] result = inv.MatrixProduct(matrix);

            Assert.AreEqual(identity[0][0], result[0][0]);
            Assert.AreEqual(identity[0][1], result[0][1]);
            Assert.AreEqual(identity[0][2], result[0][2]);

            Assert.AreEqual(identity[1][0], result[1][0]);
            Assert.AreEqual(identity[1][1], result[1][1]);
            Assert.AreEqual(identity[1][2], result[1][2]);

            Assert.AreEqual(identity[2][0], result[2][0]);
            Assert.AreEqual(identity[2][1], result[2][1]);
            Assert.AreEqual(identity[2][2], result[2][2]);
        }

        [TestMethod]
        public void MatrixInverse2Test()
        {
            double error = 0.000000000000001;
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

            double[][] inv = matrix.MatrixInverse();

            var identity = MatrixOperations.MatrixIdentity(4);

            double[][] result = inv.MatrixProduct(matrix);

            Assert.IsTrue(Math.Abs(identity[0][0] - result[0][0]) < error);
            Assert.IsTrue(Math.Abs(identity[0][1] - result[0][1]) < error);
            Assert.IsTrue(Math.Abs(identity[0][2] - result[0][2]) < error);
            Assert.IsTrue(Math.Abs(identity[0][3] - result[0][3]) < error);

            Assert.IsTrue(Math.Abs(identity[1][0] - result[1][0]) < error);
            Assert.IsTrue(Math.Abs(identity[1][1] - result[1][1]) < error);
            Assert.IsTrue(Math.Abs(identity[1][2] - result[1][2]) < error);
            Assert.IsTrue(Math.Abs(identity[1][3] - result[1][3]) < error);

            Assert.IsTrue(Math.Abs(identity[2][0] - result[2][0]) < error);
            Assert.IsTrue(Math.Abs(identity[2][1] - result[2][1]) < error);
            Assert.IsTrue(Math.Abs(identity[2][2] - result[2][2]) < error);
            Assert.IsTrue(Math.Abs(identity[2][3] - result[2][3]) < error);

            Assert.IsTrue(Math.Abs(identity[3][0] - result[3][0]) < error);
            Assert.IsTrue(Math.Abs(identity[3][1] - result[3][1]) < error);
            Assert.IsTrue(Math.Abs(identity[3][2] - result[3][2]) < error);
            Assert.IsTrue(Math.Abs(identity[3][3] - result[3][3]) < error);
        }
    }
}
