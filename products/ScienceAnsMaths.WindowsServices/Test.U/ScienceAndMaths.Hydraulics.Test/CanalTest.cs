using System;
using ScienceAndMaths.Shared;
using ScienceAndMaths.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScienceAndMaths.Hydraulics.Canals;

namespace ScienceAndMaths.Hydraulics.Test
{
    [TestClass]
    public class CanalTest
    {
        [TestMethod]
        public void BasicRectangularFlowTest()
        {
            CanalStretch canal = new CanalStretch(602, 20.32, new RectangularSection(5, 0.028, 0));

            double rh = canal.CanalSection.GetHydraulicRadius(2.9);
            double mv = canal.CanalSection.GetManningVelocity(2.9);
            double fr2 = Math.Pow(canal.GetFroudeNumber(2.9), 2.0);

            RungeKutta solver = new RungeKutta(1, canal.FlowEquation());

            //  Act
            double result = solver.Solve(0, 2.9);

            double actualRh = (5.0 * 2.9) / (5.0 + 2.0 * 2.9);
            Assert.IsTrue(rh - actualRh <= double.Epsilon);
            Assert.IsTrue(mv - 0 <= double.Epsilon);
            Assert.IsTrue(fr2 - 0.0690 <= 0.0001);
            Assert.IsTrue(result - 2.89888 <= 0.00001);
        }
    }
}
