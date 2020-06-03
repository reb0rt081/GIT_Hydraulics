using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScienceAndMaths.Mathematics.Test
{
    [TestClass]
    public class NewtonRaphsonTest
    {
        [TestMethod]
        public void BasicEquationSolve()
        {
            double epsilon = 0.0001;
            NewtonRaphson newtonRaphson = new NewtonRaphson((x => x + 5), x => 1);

            double result = newtonRaphson.Solve(0, epsilon);

            Assert.IsTrue(Math.Abs(-5 - result) < epsilon);
        }

        [TestMethod]
        public void BasicEquationNoDerivativeSolve()
        {
            double epsilon = 0.0001;
            NewtonRaphson newtonRaphson = new NewtonRaphson((x => x + 5));

            double result = newtonRaphson.Solve(0, epsilon);

            Assert.IsTrue(Math.Abs(-5 - result) < epsilon);
        }

        [TestMethod]
        public void SecondDegreeEquationNoDerivativeSolve()
        {
            double epsilon = 0.0001;
            NewtonRaphson newtonRaphson = new NewtonRaphson((x => x * x + 2 * x + 1));

            double result = newtonRaphson.Solve(0, epsilon);

            double error = Math.Abs(-1 - result);
            Assert.IsTrue(error <= epsilon * 10);
        }

        /// <summary>
        /// Finds closes value
        /// </summary>
        [TestMethod]
        public void SecondDegreeEquationNoDerivativeSolve2()
        {
            double epsilon = 0.0001;
            NewtonRaphson newtonRaphson = new NewtonRaphson((x => x * x - x -2));

            double result = newtonRaphson.Solve(10, epsilon);

            double error = Math.Abs(2 - result);
            Assert.IsTrue(error <= epsilon * 10);
        }
    }
}
