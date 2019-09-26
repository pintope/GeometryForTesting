namespace GeometryForTesting
{
    using System;


    public class MathTools
    {
        public virtual double Div(double a, double b)
        {
            return a / b;
        }

        public virtual double Mul(double a, double b)
        {
            return a * b;
        }

        public virtual double Pow(double b, double exp)
        {
            return Math.Pow(b, exp);
        }

        public virtual double Sin(double a)
        {
            return Math.Sin(a);
        }


        public virtual double PI { get; } = Math.PI;
    }
}