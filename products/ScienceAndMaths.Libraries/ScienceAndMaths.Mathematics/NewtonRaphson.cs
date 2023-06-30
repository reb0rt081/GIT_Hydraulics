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
    public class NewtonRaphson
    {
        public NewtonRaphson(Func<double, double> function, Func<double, double> derivativeFunction = null)
        {
            Function = function;

            DerivativeFunction = derivativeFunction;
        }

        public Func<double, double> Function { get; set; }

        public Func<double, double> DerivativeFunction { get; set; }

        public double Solve(double initialValue, double epsilon)
        {
            double x = initialValue;
            
            double h = Function(x) / GetDerivativeValue(x, epsilon);
            while (Math.Abs(h) >= epsilon)
            {
                h = Function(x) / GetDerivativeValue(x, epsilon);

                // x(i+1) = x(i) - f(x) / f'(x)   
                x = x - h;
            }

            return x;
        }

        private double GetDerivativeValue(double x, double epsilon)
        {
            double derivativeValue;

            if (DerivativeFunction == null)
            {
                derivativeValue = (Function(x + epsilon) - Function(x)) / epsilon;
            }
            else
            {
                derivativeValue = DerivativeFunction(x);
            }

            return derivativeValue;
        }
    }
}
