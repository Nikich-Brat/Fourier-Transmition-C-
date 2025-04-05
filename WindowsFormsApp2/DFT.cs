#define math
using System;
using System.Drawing;

namespace Base
{ 
    partial class DFT
    {
        const double Pi = 3.14159;
        public double GetSumReal(double[] mas, double[] phase, int i, int j)
        {
            return mas[j] * Math.Cos((phase[i] * Pi / 180) + 2 * Pi * j * i / (mas.Length));
        }

        public double GetSumImage(double[] mas, double[] phase, int i, int j)
        {
            return mas[j] * Math.Sin((phase[i] * Pi / 180) + 2 * Pi * j * i / (mas.Length));
        }
        public double[,] DFTDirect(double[] mas, int fd)
        {
            double sumreal, sumimage;
            int len = mas.Length;
            double[] emptyarr = new double[len];
            double[,] resmass = new double[5, len];
            for (int i = 0; i < len; i++)
            {
                sumreal = sumimage = 0;
                for (int j = 0; j < len; j++)
                {
                    sumreal = sumreal + GetSumReal(mas, emptyarr, i, j);
                    sumimage = sumimage + GetSumImage(mas, emptyarr, i, j);
                }
                resmass[0, i] = 2 * Math.Sqrt(sumreal * sumreal + sumimage * sumimage) / len;
                resmass[3, i] = 2 * sumreal / len;
                resmass[4, i] = 2 * sumimage / len;
                resmass[1, i] = fd * i / len; 
                if (sumreal != 0)
                {
                    resmass[2, i] = Math.Atan(sumimage / sumreal) * 180 / Pi;
                }
                else
                {
                    if (sumimage > 0)
                    {
                        resmass[2, i] = 90;
                    }
                    else if (sumimage == 0)
                    {
                        resmass[2, i] = 0;
                    }
                    else
                    {
                        resmass[2, i] = -90;
                    }

                }

            }
            return resmass;
        }

    }
}