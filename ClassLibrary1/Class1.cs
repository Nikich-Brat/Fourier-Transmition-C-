using System;
namespace Cmplx
{
    public class Complex
    {
        public double re;
        public double im;
    }
    public class Complex2 : Complex
    {
        public Complex AddC(Complex a, Complex b)
        {
            Complex complex = null;
            complex.re = a.re + b.re;
            complex.im = a.im + b.im;
            return complex;
        }

        public Complex SubC(Complex a, Complex b)
        {
            Complex complex = null;
            complex.re = a.re - b.re;
            complex.im = a.im - b.im;
            return complex;
        }

        public Complex MulC(Complex a, Complex b)
        {
            Complex complex = null;
            complex.re = a.re * b.re;
            complex.im = a.im * b.im;
            return complex;
        }

        public Complex DivC(Complex a, Complex b)
        {
            Complex complex = null;
            double num = Math.Pow(b.re, 2.0) + Math.Pow(b.im, 2.0);
            if (num != 0.0)
            {
                complex.re = (a.re * b.re + a.im * b.im) / num;
                complex.im = (a.re * b.im + a.im * b.re) / num;
            }

            return complex;
        }

        public double AbsC(Complex a)
        {
            return a.re * a.re + a.im * a.im;
        }

        public Complex CnjC(Complex a)
        {
            Complex complex = null;
            complex.re = a.re;
            complex.im = 0.0 - a.im;
            return complex;
        }

        public Complex GetComplexOfAngleC(double a)
        {
            Complex complex = null;
            complex.re = Math.Cos(a);
            complex.im = Math.Sin(a);
            return complex;
        }
    }
}