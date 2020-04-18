using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Mathematics
{
    public class FiniteElementMethodBasic
    {
        public class TriangularElement
        {
            public TriangularElement(double x1, double y1, double x2, double y2, double x3, double y3)
            {
                Vertex1 = new Node(x1, y1);

                Vertex2 = new Node(x2, y2);

                Vertex3 = new Node(x3, y3);
            }

            public Node Vertex1;

            public Node Vertex2;

            public Node Vertex3;

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
                
            public double[][] GetInverseInterpolationMatrix()
            {
                return GetInterpolationMatrix().MatrixInverse();
            }

        }

        public class Node
        {
            public Node(double x, double y)
            {
                X = x;
                Y = y;
            }

            public double X;
            public double Y;
        }
    }
}
