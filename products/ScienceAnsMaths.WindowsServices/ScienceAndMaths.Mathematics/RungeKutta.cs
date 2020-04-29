using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Mathematics
{
    public class RungeKutta
    {
        /// <summary>
        /// Interval variation for the calculation
        /// </summary>
        public double Interval { get; set; }

        /// <summary>
        /// Differential equation of first order in the form: dy/dx = f(x,y)
        /// </summary>
        public Func<double, double, double> Equation { get; set; }

        public RungeKutta()
        {

        }

        public RungeKutta(double interval,Func<double,double, double> equation)
        {
            Interval = interval;
            Equation = equation;
        }

        /// <summary>
        /// Returns the next calculates Y value in X + Interval
        /// </summary>
        /// <param name="initX"></param>
        /// <param name="initY"></param>
        /// <returns></returns>
        public double Solve(double initX, double initY)
        {
            double k1 = Equation(initX, initY);
            double k2 = Equation(initX + 0.5 * Interval, initY + 0.5 * k1 * Interval);
            double k3 = Equation(initX + 0.5 * Interval, initY + 0.5 * k2 * Interval);
            double k4 = Equation(initX + Interval, initY + k3 * Interval);

            return initY + 1.0 / 6.0 * Interval * (k1 + 2.0 * k2 + 2.0 * k3 + k4);
        }
    }
}
