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
            //  E/(1-v^2) = E / ((1-v)*(1+v) =	2,31E+06
            //  Modulo elastico acero: E = 2,10E+06
            //  Coeficiente de Poisson Acero: v = E/2G - 1 = 0.3
            //  Modulo de elasticidad transversal acero: G = E/(2*(1 + v)), este cálculo asume que Tension_tangencial = G * (2 * Deformacion_tangencial)
            //  La fórmula de arriba es la que aplicaremos para simplificar calculos en la matriz de derivadas, aunque en realidad: G = E /(1 + v)
            
            //  ElasticCoefficient = E / (1 - v^2)
            double elasticCoefficient = 2.31E+06;

            // D matrix: {Stress} = [D] * {Strain}
            //                        | 1   v      0    |   |   Strain_X    |
            //  [D] = E / (1 - v^2) * | v   1      0    | * |   Strain_Y    |
            //                        | 0   0   (1-v)/2 |   |   Strain_XY   |, Strain_XY = 2 * Actual_Strain_XY (That's why there is a 2 dividing (1-v).

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

            #endregion

            #region Element 2

            element2.SetDMatrix(dMatrix);

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

            //  Setting displacements
            double[][] uMatrix = MatrixOperations.MatrixCreate(8, 1);

            uMatrix[0][0] = 0.0;
            uMatrix[1][0] = 0.0;
            uMatrix[2][0] = 0.364E-2;
            uMatrix[3][0] = 0.742E-3;
            uMatrix[4][0] = 0.0;
            uMatrix[5][0] = 0.0;
            uMatrix[6][0] = 0.316E-2;
            uMatrix[7][0] = -0.259E-3;

            //  Obtaining global matrix
            double[][] fMatrix = kGlobal.MatrixProduct(uMatrix);

            //  Checking for absolute errors
            var absErrorF1X = Math.Abs(fMatrix[0][0] - (-3750.0));
            var absErrorF1Y = Math.Abs(fMatrix[1][0] - (-1198.0));
            var absErrorF2X = Math.Abs(fMatrix[2][0] - (3750));
            var absErrorF2Y = Math.Abs(fMatrix[3][0] - (0));
            var absErrorF3X = Math.Abs(fMatrix[4][0] - (-3750.0));
            var absErrorF3Y = Math.Abs(fMatrix[5][0] - (1198));
            var absErrorF4X = Math.Abs(fMatrix[6][0] - (3750.0));
            var absErrorF4Y = Math.Abs(fMatrix[7][0] - (0));

            //  Making sure absolute errors are less than 1%
            Assert.IsTrue(absErrorF1X < Math.Abs(fMatrix[0][0] * 0.01));
            Assert.IsTrue(absErrorF1Y < Math.Abs(fMatrix[1][0] * 0.01));
            Assert.IsTrue(absErrorF2X < Math.Abs(fMatrix[2][0] * 0.01));
            Assert.IsTrue(absErrorF2Y < 1.0);
            Assert.IsTrue(absErrorF3X < Math.Abs(fMatrix[4][0] * 0.01));
            Assert.IsTrue(absErrorF3Y < Math.Abs(fMatrix[5][0] * 0.01));
            Assert.IsTrue(absErrorF4X < Math.Abs(fMatrix[6][0] * 0.01));
            Assert.IsTrue(absErrorF4Y < 1.0);

            #endregion

        }

        //  todo adjust this test
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

            Assert.AreEqual(1.0, kGlobal[0][0]);
            Assert.AreEqual(-0.75, kGlobal[0][9]);
            Assert.AreEqual(-0.5, kGlobal[0][10]);

            Assert.AreEqual(-0.5, kGlobal[1][3]);
            Assert.AreEqual(-0.0, kGlobal[1][6]);
            Assert.AreEqual(-0.5, kGlobal[1][11]);

            Assert.AreEqual(2.0, kGlobal[2][2]);
            Assert.AreEqual(0.25, kGlobal[2][5]);
            Assert.AreEqual(-0.75, kGlobal[2][7]);

            Assert.AreEqual(0.25, kGlobal[3][0]);
            Assert.AreEqual(2.0, kGlobal[3][3]);
            Assert.AreEqual(-0.0, kGlobal[3][7]);

            Assert.AreEqual(0.0, kGlobal[8][0]);
            Assert.AreEqual(2.0, kGlobal[8][8]);
            Assert.AreEqual(-0.75, kGlobal[8][9]);

            #endregion
        }
    }
}
