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
            CanalStretch canal = new CanalStretch("stretch1", 602, 20.32, new RectangularSection(5, 0.028, 0));

            double rh = canal.CanalSection.GetHydraulicRadius(2.9);
            double mv = canal.CanalSection.GetManningVelocity(2.9);
            double fr2 = Math.Pow(canal.GetFroudeNumber(2.9), 2.0);
            double criticalDepth = canal.CanalSection.GetCriticalWaterLevel(20.32);
            RungeKutta solver = new RungeKutta(1, canal.FlowEquation());

            //  Act
            double result = solver.Solve(0, 2.9);

            double actualRh = (5.0 * 2.9) / (5.0 + 2.0 * 2.9);
            Assert.IsTrue(rh - actualRh <= double.Epsilon);
            Assert.IsTrue(mv - 0 <= double.Epsilon);
            Assert.IsTrue(fr2 - 0.0690 <= 0.0001);
            Assert.IsTrue(result - 2.89888 <= 0.00001);
            Assert.IsTrue(Math.Abs(criticalDepth - 1.1897678822813287) <= double.Epsilon);
        }

        [TestMethod]
        public void BasicRectangularFlowTest2()
        {
            CanalStretch canal = new CanalStretch("stretch1", 602, 2.5486, new RectangularSection(4, 0.014, 0.0004));

            double rh = canal.CanalSection.GetHydraulicRadius(0.694);
            double mv = canal.CanalSection.GetManningVelocity(0.694);
            double fr2 = Math.Pow(canal.GetFroudeNumber(0.694), 2.0);
            double flow = canal.CanalSection.GetManningFlow(0.694);
            double criticalDepth = canal.CanalSection.GetCriticalWaterLevel(flow);
            RungeKutta solver = new RungeKutta(1, canal.FlowEquation());

            //  Act
            double result = solver.Solve(0, 0.694);

            double actualRh = (4.0 * 0.694) / (4.0 + 2.0 * 0.694);
            Assert.IsTrue(rh - actualRh <= double.Epsilon);
            Assert.IsTrue(mv - 0.91811 <= 0.0001);
            Assert.IsTrue(fr2 - 1.90669 <= 0.0001);
            Assert.IsTrue(result - 0.694 <= 0.00001);
            Assert.IsTrue(Math.Abs(criticalDepth - 0.34593708152888353) <= double.Epsilon);
        }
    }
}
