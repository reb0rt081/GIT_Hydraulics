using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAndMaths.Shared;

namespace ScienceAndMaths.Mathematics.FEM
{
    public class FiniteElementMethodModel
    {
        public FiniteElementMethodModel()
        {
            Elements = new List<IInterpolationElement>();
            Nodes = new List<INode>();
        }

        public List<IInterpolationElement> Elements { get; private set; }

        public List<INode> Nodes { get; private set; }

        public double[][] BuildGlobalKMatrix()
        {
            double[][] globalKMatrix = MatrixOperations.MatrixCreate(Nodes.Count, Nodes.Count);

            foreach (IInterpolationElement element in Elements)
            {
                //  2D simulation: {u1, v1, u2, v2, ..., un, vn}
                if(element is TriangularElement triangularElement)
                {
                    //  {u1, v1, u2, v2, u3, v3}
                    double[][] localKmatrix = triangularElement.GetKMatrix();

                    int indexVertex1 = Nodes.IndexOf(triangularElement.Vertex1);
                    int indexVertex2 = Nodes.IndexOf(triangularElement.Vertex2);
                    int indexVertex3 = Nodes.IndexOf(triangularElement.Vertex3);


                    //  Element local F1H
                    globalKMatrix[indexVertex1 * 2][indexVertex1 * 2] = globalKMatrix[indexVertex1 * 2][indexVertex1 * 2] + localKmatrix[0][0];
                    globalKMatrix[indexVertex1 * 2][indexVertex1 * 2 + 1] = globalKMatrix[indexVertex1 * 2][indexVertex1 * 2 + 1] + localKmatrix[0][1];
                    globalKMatrix[indexVertex1 * 2][indexVertex2 * 2] = globalKMatrix[indexVertex1 * 2][indexVertex2 * 2] + localKmatrix[0][2];
                    globalKMatrix[indexVertex1 * 2][indexVertex2 * 2 + 1] = globalKMatrix[indexVertex1 * 2][indexVertex2 * 2 + 1] + localKmatrix[0][3];
                    globalKMatrix[indexVertex1 * 2][indexVertex3 * 2] = globalKMatrix[indexVertex1 * 2][indexVertex3 * 2] + localKmatrix[0][4];
                    globalKMatrix[indexVertex1 * 2][indexVertex3 * 2 + 1] = globalKMatrix[indexVertex1 * 2][indexVertex3 * 2 + 1] + localKmatrix[0][5];

                    //  Element local F1V
                    globalKMatrix[indexVertex1 * 2 + 1][indexVertex1 * 2] = globalKMatrix[indexVertex1 * 2 + 1][indexVertex1 * 2] + localKmatrix[1][0];
                    globalKMatrix[indexVertex1 * 2 + 1][indexVertex1 * 2 + 1] = globalKMatrix[indexVertex1 * 2 + 1][indexVertex1 * 2 + 1] + localKmatrix[1][1];
                    globalKMatrix[indexVertex1 * 2 + 1][indexVertex2 * 2] = globalKMatrix[indexVertex1 * 2 + 1][indexVertex2 * 2] + localKmatrix[1][2];
                    globalKMatrix[indexVertex1 * 2 + 1][indexVertex2 * 2 + 1] = globalKMatrix[indexVertex1 * 2 + 1][indexVertex2 * 2 + 1] + localKmatrix[1][3];
                    globalKMatrix[indexVertex1 * 2 + 1][indexVertex3 * 2] = globalKMatrix[indexVertex1 * 2 + 1][indexVertex3 * 2] + localKmatrix[1][4];
                    globalKMatrix[indexVertex1 * 2 + 1][indexVertex3 * 2 + 1] = globalKMatrix[indexVertex1 * 2 + 1][indexVertex3 * 2 + 1] + localKmatrix[1][5];

                    //  Element local F2H
                    globalKMatrix[indexVertex2 * 2][indexVertex1 * 2] = globalKMatrix[indexVertex2 * 2][indexVertex1 * 2] + localKmatrix[2][0];
                    globalKMatrix[indexVertex2 * 2][indexVertex1 * 2 + 1] = globalKMatrix[indexVertex2 * 2][indexVertex1 * 2 + 1] + localKmatrix[2][1];
                    globalKMatrix[indexVertex2 * 2][indexVertex2 * 2] = globalKMatrix[indexVertex2 * 2][indexVertex2 * 2] + localKmatrix[2][2];
                    globalKMatrix[indexVertex2 * 2][indexVertex2 * 2 + 1] = globalKMatrix[indexVertex2 * 2][indexVertex2 * 2 + 1] + localKmatrix[2][3];
                    globalKMatrix[indexVertex2 * 2][indexVertex3 * 2] = globalKMatrix[indexVertex2 * 2][indexVertex3 * 2] + localKmatrix[2][4];
                    globalKMatrix[indexVertex2 * 2][indexVertex3 * 2 + 1] = globalKMatrix[indexVertex2 * 2][indexVertex3 * 2 + 1] + localKmatrix[2][5];

                    //  Element local F2V
                    globalKMatrix[indexVertex2 * 2 + 1][indexVertex1 * 2] = globalKMatrix[indexVertex2 * 2 + 1][indexVertex1 * 2] + localKmatrix[3][0];
                    globalKMatrix[indexVertex2 * 2 + 1][indexVertex1 * 2 + 1] = globalKMatrix[indexVertex2 * 2 + 1][indexVertex1 * 2 + 1] + localKmatrix[3][1];
                    globalKMatrix[indexVertex2 * 2 + 1][indexVertex2 * 2] = globalKMatrix[indexVertex2 * 2 + 1][indexVertex2 * 2] + localKmatrix[3][2];
                    globalKMatrix[indexVertex2 * 2 + 1][indexVertex2 * 2 + 1] = globalKMatrix[indexVertex2 * 2 + 1][indexVertex2 * 2 + 1] + localKmatrix[3][3];
                    globalKMatrix[indexVertex2 * 2 + 1][indexVertex3 * 2] = globalKMatrix[indexVertex2 * 2 + 1][indexVertex3 * 2] + localKmatrix[3][4];
                    globalKMatrix[indexVertex2 * 2 + 1][indexVertex3 * 2 + 1] = globalKMatrix[indexVertex2 * 2 + 1][indexVertex3 * 2 + 1] + localKmatrix[3][5];

                    //  Element local F3H
                    globalKMatrix[indexVertex3 * 2][indexVertex1 * 2] = globalKMatrix[indexVertex3 * 2][indexVertex1 * 2] + localKmatrix[4][0];
                    globalKMatrix[indexVertex3 * 2][indexVertex1 * 2 + 1] = globalKMatrix[indexVertex3 * 2][indexVertex1 * 2 + 1] + localKmatrix[4][1];
                    globalKMatrix[indexVertex3 * 2][indexVertex2 * 2] = globalKMatrix[indexVertex3 * 2][indexVertex2 * 2] + localKmatrix[4][2];
                    globalKMatrix[indexVertex3 * 2][indexVertex2 * 2 + 1] = globalKMatrix[indexVertex3 * 2][indexVertex2 * 2 + 1] + localKmatrix[4][3];
                    globalKMatrix[indexVertex3 * 2][indexVertex3 * 2] = globalKMatrix[indexVertex3 * 2][indexVertex3 * 2] + localKmatrix[4][4];
                    globalKMatrix[indexVertex3 * 2][indexVertex3 * 2 + 1] = globalKMatrix[indexVertex3 * 2][indexVertex3 * 2 + 1] + localKmatrix[4][5];

                    //  Element local F3V
                    globalKMatrix[indexVertex3 * 2 + 1][indexVertex1 * 2] = globalKMatrix[indexVertex3 * 2 + 1][indexVertex1 * 2] + localKmatrix[5][0];
                    globalKMatrix[indexVertex3 * 2 + 1][indexVertex1 * 2 + 1] = globalKMatrix[indexVertex3 * 2 + 1][indexVertex1 * 2 + 1] + localKmatrix[5][1];
                    globalKMatrix[indexVertex3 * 2 + 1][indexVertex2 * 2] = globalKMatrix[indexVertex3 * 2 + 1][indexVertex2 * 2] + localKmatrix[5][2];
                    globalKMatrix[indexVertex3 * 2 + 1][indexVertex2 * 2 + 1] = globalKMatrix[indexVertex3 * 2 + 1][indexVertex2 * 2 + 1] + localKmatrix[5][3];
                    globalKMatrix[indexVertex3 * 2 + 1][indexVertex3 * 2] = globalKMatrix[indexVertex3 * 2 + 1][indexVertex3 * 2] + localKmatrix[5][4];
                    globalKMatrix[indexVertex3 * 2 + 1][indexVertex3 * 2 + 1] = globalKMatrix[indexVertex3 * 2 + 1][indexVertex3 * 2 + 1] + localKmatrix[5][5];
                }
            }

            return globalKMatrix;
        }
    }
}
