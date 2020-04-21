using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScienceAndMaths.Mathematics.FEM;

namespace ScienceAndMaths.Mathematics.Test
{
    [TestClass]
    public class FiniteElementMethodTest
    {
        [TestMethod]
        public void GetKMatrixTest()
        {
            //  E/(1-v^2) =	2,31E+06
            //  Modulo elastico acero: E = 2,10E+06
            //  Coeficiente de Poisson Acero: v = E/2G - 1 = 0.3
            //  Modulo de elasticidad transversal acero: G = E/(2*(1 + v))
            //  ElasticCoefficient = E / (1 - v^2)
            double elasticCoefficient = 2.31E+06;

            double[][] cMatrix = MatrixOperations.MatrixCreate(3, 3);
            cMatrix[0][0] = 1.0;
            cMatrix[0][1] = 0.3;
            cMatrix[0][2] = 0.0;

            cMatrix[1][0] = 0.3;
            cMatrix[1][1] = 1.0;
            cMatrix[1][2] = 0.0;

            cMatrix[2][0] = 0.0;
            cMatrix[2][1] = 0.0;
            cMatrix[2][2] = 0.35;
            
            Node node1 = new Node(0.0, 0.0);

            Node node2 = new Node(0.0, 15.0);

            Node node3 = new Node(15.0, 0.0);

            Node node4 = new Node(15.0, 15.0);

            TriangularElement element1 = new TriangularElement(node1, node2, node4);

            TriangularElement element2 = new TriangularElement(node1, node3, node4);

            var dMatrix = cMatrix.MatrixProductByConstant(elasticCoefficient);

            #region Element 1

            element1.SetDMatrix(dMatrix);

            var k1 = element1.GetKMatrix();

            #endregion

            #region Element 1

            element2.SetDMatrix(dMatrix);

            var k2 = element2.GetKMatrix();

            #endregion

        }

        [TestMethod]
        public void GetKMatrix2Test()
        {
            Node node1 = new Node(0.0, 0.0);

            Node node2 = new Node(1.0, 0.0);

            Node node3 = new Node(1.0, 1.0);

            TriangularElement element1 = new TriangularElement(node1, node2, node3);

            var interpolationMatrix = element1.GetInterpolationMatrix();

            var elementArea = element1.GetElementDimension();

            var bMatrix = element1.GetBMatrix();

            double[][] cMatrix = MatrixOperations.MatrixCreate(3, 3);
            cMatrix[0][0] = 1.0;
            cMatrix[0][1] = 0.5;
            cMatrix[0][2] = 0.0;

            cMatrix[1][0] = 0.5;
            cMatrix[1][1] = 1.0;
            cMatrix[1][2] = 0.0;

            cMatrix[2][0] = 0.0;
            cMatrix[2][1] = 0.0;
            cMatrix[2][2] = 1.0;

            var bMatrixTranspose = bMatrix.MatrixTranspose();

            var product1 = bMatrixTranspose.MatrixProduct(cMatrix);

            var product2 = product1.MatrixProduct(bMatrix);

            var result = product2.MatrixProductByConstant(elementArea);
        }
    }
}
