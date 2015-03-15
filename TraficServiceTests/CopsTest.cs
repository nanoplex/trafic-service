using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TraficService;

namespace TraficServiceTests
{
    [TestClass]
    public class CopsTest
    {
        static double Radius = Convert.ToDouble("0.00016");

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

        [TestMethod]
        public void MeterTest()
        {
            bool a;
            var testClose = Close.Where(c =>
            {
                return TestRadiusClose(c, Start, Radius);
            });
            bool b;
            var testFar = Far.Where(c =>
            {
                return TestRadiusFar(c, Start, Radius);
            });

            Assert.AreEqual(testClose.Count(), 1);
            Assert.AreEqual(testFar.Count(), 1);
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
