using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineProj
{
    class Proj
    {
        public static double ProjectionX(double az, double xr, double yr, double xs, double ys)
        {
            double deg = az * (-1) + 90;
            
            double m = Math.Tan(deg * Math.PI / 180);

            if (az==0 || az == 180)
            {
                return xr;
            }

            else if (az==90 || az == 270)
            {

                return xs;

            }   
            else
            {
                double a = m * xr - yr;

                double b = ys + xs / m;

                double xp = (b + a) / (m + 1 / m);

                return xp;
            }
           
            
        }

        public static double ProjectionY(double az, double xr, double yr, double xs, double ys)
        {
            double deg = az * (-1) + 90;

            double m = Math.Tan(deg * Math.PI / 180);

            if (az == 0 || az == 180)
            {
                return ys;
            }
            else if (az == 90 || az == 270)
            {
                return yr;
            }
            else
            {
                double a = m * xr - yr;

                double b = ys + xs / m;

                double yp = m * ((b + a) / (m + 1 / m)) - a;

                return yp;
            }
            

        }

        public static double OffsetCalc(double az, double xr, double yr, double xs, double ys)
        {

            double dist;

            if (az<180 && az > 0 && xs > xr)
            {
                dist = Math.Sqrt((xr - xs) * (xr - xs) + (yr - ys) * (yr - ys));
            }
            else if (az == 0 && yr < ys)
            {
                dist = Math.Sqrt((xr - xs) * (xr - xs) + (yr - ys) * (yr - ys));
            }
            else if (az == 180 && ys < yr)
            {
                dist = Math.Sqrt((xr - xs) * (xr - xs) + (yr - ys) * (yr - ys));
            }
            else if (az > 180 && az < 360 && xs < xr)
            {
                dist = Math.Sqrt((xr - xs) * (xr - xs) + (yr - ys) * (yr - ys));
            }
            else
            {
                dist = (-1) * Math.Sqrt((xr - xs) * (xr - xs) + (yr - ys) * (yr - ys));
            }
            
            
            //double dist = Math.Sqrt((xr - xs)*(xr - xs) + (yr - ys)*(yr - ys));
            return dist;
        }
    }
}
