using System;
using ScienceAndMaths.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScienceAndMaths.Hydraulics.Canals;

namespace ScienceAndMaths.Hydraulics.Test
{
    [TestClass]
    public class CanalTest
    {
        [TestMethod]
        public void BasicFlowTest()
        {
            Canal canal = new Canal(602, 20.32, new RectangularSection(5, 0.028, 0));

            double rh = canal.CanalSection.GetHydraulicRadius(2.9);
            double mv = canal.CanalSection.GetManningVelocity(2.9);
            double fr = canal.CanalSection.GetFroudeNumber(2.9);

            RungeKutta solver = new RungeKutta(1, canal.FlowEquation());

            //  Act
            double result = solver.Solve(0, 2.9);
        }
    }
}
