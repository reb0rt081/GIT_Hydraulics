using System;
using ScienceAndMaths.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScienceAndMaths.Mathematics.Test
{
    [TestClass]
    public class RungeKuttaTest
    {
        [TestMethod]
        public void BasicTest()
        {
            Func<double, double, double> differentialEquation = (x, y) => x - y;
            RungeKutta solver = new RungeKutta(0.2, differentialEquation);

            double result = solver.Solve(1, 2);

            Assert.IsTrue(result > 1.83);
        }
    }
}
