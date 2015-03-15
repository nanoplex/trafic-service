using System;
using System.Collections.Generic;
using System.Linq;
using TraficService;

namespace ConsoleApplication
{
    class Program
    {
        static double Radius = Convert.ToDouble("0");

        static Location Start
        {
            get
            {
                return new Location
                {
                    Latitude = Convert.ToDouble("56.148281"),
                    Longitude = Convert.ToDouble("9.889964")
                };
            }
        }
        static List<Location> Close
        {
            get
            {
                return new List<Location>()
                {new Location() {
                    Latitude = Convert.ToDouble("56.14827"),
                    Longitude = Convert.ToDouble("9.891592")
                }};
            }
        }
        static List<Location> Far
        {
            get
            {
                return new List<Location>
                {new Location() {
                    Latitude = Convert.ToDouble("56.148273"),
                    Longitude = Convert.ToDouble("9.891627")
                }};
            }
        }

        static void Main(string[] args)
        {

            bool a;
            var testClose = Close.Where(c =>
            {
                a = TestRadiusClose(c, Start, Radius);
                return a;
            });
            bool b;
            var testFar = Far.Where(c =>
            {
                b = TestRadiusFar(c, Start, Radius);
                return b;
            });

            var t = testClose.Count() + " " + testFar.Count();
            t = t;
        }

        private static bool TestRadiusFar(Location c, Location start, double radius)
        {
            return (Math.Pow((c.Latitude - start.Latitude), 2) + Math.Pow((c.Longitude - start.Longitude), 2) < Math.Pow(radius, 2));
        }

        private static bool TestRadiusClose(Location c, Location start, double radius)
        {
            return (Math.Pow((c.Latitude - start.Latitude), 2) + Math.Pow((c.Longitude - start.Longitude), 2) > Math.Pow(radius, 2));
        }

    }
}
