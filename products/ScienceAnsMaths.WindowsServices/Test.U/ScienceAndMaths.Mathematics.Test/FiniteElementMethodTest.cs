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

            Node node2 = new Node(15.0, 0.0);

            Node node3 = new Node(0.0, 15.0);

            Node node4 = new Node(15.0, 15.0);

            //  It looks it is important that all elements are set up with the same spin direction

            //  Check what happens in 3D how does it work

            //  node1 => node4 => node3
            TriangularElement element1 = new TriangularElement(node1, node4, node3);

            //  node1 => node2 => node4
            TriangularElement element2 = new TriangularElement(node1, node2, node4);

            var dMatrix = cMatrix.MatrixProductByConstant(elasticCoefficient);

            #region Element 1

            element1.SetDMatrix(dMatrix);

            var k1 = element1.GetKMatrix();

            #endregion

            #region Element 1

            element2.SetDMatrix(dMatrix);

            var k2 = element2.GetKMatrix();

            #endregion

            #region Kglobal

            FiniteElementMethodModel model = new FiniteElementMethodModel();

            model.Nodes.Add(node1);
            model.Nodes.Add(node2);
            model.Nodes.Add(node3);
            model.Nodes.Add(node4);

            model.Elements.Add(element1);
            model.Elements.Add(element2);

            var kGlobal = model.BuildGlobalKMatrix();

            double[][] uMatrix = MatrixOperations.MatrixCreate(8, 1);

            uMatrix[0][0] = 0.0;
            uMatrix[1][0] = 0.0;
            uMatrix[2][0] = 0.364E-2;
            uMatrix[3][0] = 0.742E-3;
            uMatrix[4][0] = 0.0;
            uMatrix[5][0] = 0.0;
            uMatrix[6][0] = 0.316E-2;
            uMatrix[7][0] = -0.259E-3;

            double[][] fMatrix = kGlobal.MatrixProduct(uMatrix);

            Assert.IsTrue(fMatrix[0][0] - (-3750.0) < 0.2);
            Assert.IsTrue(fMatrix[1][0] - (-1198.0) < 10);
            Assert.IsTrue(fMatrix[2][0] - (3750) < 10);
            Assert.IsTrue(fMatrix[3][0] - (0) < 1);
            Assert.IsTrue(fMatrix[4][0] - (-3750.0) < 10);
            Assert.IsTrue(fMatrix[5][0] - (1198) < 10);
            Assert.IsTrue(fMatrix[6][0] - (3750.0) < 10);
            Assert.IsTrue(fMatrix[7][0] - (0) < 1);

            #endregion

        }

        [TestMethod]
        public void GetKMatrix2Test()
        {
            Node node1 = new Node(0.0, 0.0);
            Node node2 = new Node(1.0, 0.0);
            Node node3 = new Node(2.0, 0.0);
            Node node4 = new Node(2.0, 1.0);
            Node node5 = new Node(1.0, 1.0);
            Node node6 = new Node(0.0, 1.0);

            TriangularElement element1 = new TriangularElement(node1, node2, node5);
            TriangularElement element2 = new TriangularElement(node2, node3, node4);
            TriangularElement element3 = new TriangularElement(node2, node4, node5);
            TriangularElement element4 = new TriangularElement(node1, node5, node6);

            double[][] dMatrix = MatrixOperations.MatrixCreate(3, 3);
            dMatrix[0][0] = 1.0;
            dMatrix[0][1] = 0.5;
            dMatrix[0][2] = 0.0;

            dMatrix[1][0] = 0.5;
            dMatrix[1][1] = 1.0;
            dMatrix[1][2] = 0.0;

            dMatrix[2][0] = 0.0;
            dMatrix[2][1] = 0.0;
            dMatrix[2][2] = 1.0;

            element1.SetDMatrix(dMatrix);
            element2.SetDMatrix(dMatrix);
            element3.SetDMatrix(dMatrix);
            element4.SetDMatrix(dMatrix);

            #region Kglobal

            FiniteElementMethodModel model = new FiniteElementMethodModel();

            model.Nodes.Add(node1);
            model.Nodes.Add(node2);
            model.Nodes.Add(node3);
            model.Nodes.Add(node4);
            model.Nodes.Add(node5);
            model.Nodes.Add(node6);

            model.Elements.Add(element1);
            model.Elements.Add(element2);
            model.Elements.Add(element3);
            model.Elements.Add(element4);

            var kGlobal = model.BuildGlobalKMatrix();

            #endregion
        }
    }
}
