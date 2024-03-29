﻿using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeWVSP
{
    class Spetrum
    {


        public static Complex[] CalculateFT(Complex[] x)
        {
            int N = x.Length;
            Complex[] X = new Complex[N];

            Parallel.For(0, N, k =>
           {
               X[k] = new Complex(0, 0);

               Parallel.For(0, N, r =>
              {
                  X[k] += x[r] * Complex.Exp(-Complex.ImaginaryOne * ((2 * Math.PI * k * r) / N));
              });
                   

               X[k] /= N;
           });

            return X;
        }

        public static Complex[] CalculateFT(double[] x)
        {
            int N = x.Length;
            Complex[] xc = new Complex[N];

            Parallel.For (0, N, i =>
            {
                xc[i] = (Complex)x[i];
            });            

            return CalculateFT(xc);
        }

        private static Complex w(int k, int N)
        {
            if (k % N == 0) return 1;
            double arg = -2 * Math.PI * k / N;
            return new Complex(Math.Cos(arg), Math.Sin(arg));
        }

        public static Complex[] CalculateFFT(Complex[] x)
        {
            Complex[] X;
            int N = x.Length;
            if (N == 2)
            {
                X = new Complex[2];
                X[0] = x[0] + x[1];
                X[1] = x[0] - x[1];
            }
            else
            {
                Complex[] x_even = new Complex[N / 2];
                Complex[] x_odd = new Complex[N / 2];
                for (int i = 0; i < N / 2; i++)
                {
                    x_even[i] = x[2 * i];
                    x_odd[i] = x[2 * i + 1];
                }
                Complex[] X_even = CalculateFFT(x_even);
                Complex[] X_odd = CalculateFFT(x_odd);
                X = new Complex[N];
                for (int i = 0; i < N / 2; i++)
                {
                    X[i] = X_even[i] + w(i, N) * X_odd[i];
                    X[i + N / 2] = X_even[i] - w(i, N) * X_odd[i];
                }
            }
            return X;
        }

        public static Complex[] CalculateFFT(double[] x)
        {
            int N = x.Length;
            double DegreeOf2 = Math.Log(N, 2);
            int i = 0;

            List<Complex> cx = new List<Complex>();

            for (; i < x.Length; i++)
                cx.Add(new Complex(x[i], 0));

            if (Math.Floor(DegreeOf2) != DegreeOf2)
            {
                N = (int)(Math.Pow(2, Math.Floor(DegreeOf2) + 1));

                for (; i < N; i++)
                    cx.Add(new Complex(0, 0));
            }

            return CalculateFFT(cx.ToArray());
        }


        public static List<double> Spectr(List<double> series)
        {
            double Re;
            double Im;
            //double F;
            double K;
            //double tLen = (series.Count - 1) * incX;
            List<double> sp = new List<double>();
            for (double k = 0; k < series.Count / 2; k++)
            {
                Re = 0;
                Im = 0;
                //F = 0;
                K = k;
                for (int i = 0; i < series.Count; i++)
                {
                    //double n = i * incX;
                    Re += series[i] * Math.Cos(2.0 * Math.PI * i * k / series.Count);
                    Im += -series[i] * Math.Sin(2.0 * Math.PI * i * k / series.Count);
                }
                sp.Add(Convert.ToSingle(Math.Sqrt(Re * Re + Im * Im)));
                //ff.Add(Convert.ToSingle(F));
            }
            return sp;

        }

    }


    
}
