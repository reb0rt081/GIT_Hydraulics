using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Mathematics
{
    /// <summary>
    /// Represents a class to solve non-linear equations in one variable: 0 = f(x)
    /// </summary>
    public class BisectionMethod
    {
        public BisectionMethod(Func<double, double> function, double intervalBegin, double intervalEnd)
        {
            IntervalBegin = intervalBegin;
            IntervalEnd = intervalEnd;
            Function = function;
        }

        public double IntervalBegin { get; set; }

        public double IntervalEnd { get; set; }

        public Func<double, double> Function { get; set; }

        public double Solve(double epsilon)
        {
            double middle = double.MaxValue;

            if (Function(IntervalBegin) * Function(IntervalEnd) > 0.0d)
            {
                return double.MaxValue;
            }

            while (Math.Abs(IntervalBegin - IntervalEnd) > epsilon)
            {
                middle = (IntervalBegin + IntervalEnd) / 2.0d;
                
                if (Function(IntervalBegin) * Function(middle) < 0.0d)
                {
                    IntervalEnd = middle;
                }
                else
                {
                    IntervalBegin = middle;
                }
            }

            return middle;
        }
    }
}
