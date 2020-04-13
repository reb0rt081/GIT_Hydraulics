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
            //  Arrange
            Func<double, double, double> differentialEquation = (x, y) => x - y;
            RungeKutta solver = new RungeKutta(0.2, differentialEquation);

            //  Act
            double result = solver.Solve(1, 2);

            //  Assert
            double error = result - 0.2 - 2 * Math.Pow(Math.E, -0.2);

            //  Error should be very small
            Assert.IsTrue(error < 0.00001);
        }
    }
}
