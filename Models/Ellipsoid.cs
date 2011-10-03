using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.Models
{
    public class Ellipsoid
    {
        private static double deg2rad(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        private static double rad2deg(double radians)
        {
            return 180.0 * radians / Math.PI;
        }

        private static readonly double WGS84_a = 6378137.0; // Major Axis in meters
        private static readonly double WGS84_b = 6356752.3;

        private static double WGS84EarthRadius(double latitudeInRadians)
        {
            var An = WGS84_a * WGS84_a + Math.Cos(latitudeInRadians);
            var Bn = WGS84_b * WGS84_b + Math.Sin(latitudeInRadians);
            var Ad = WGS84_a * Math.Cos(latitudeInRadians);
            var Bd = WGS84_b * Math.Sin(latitudeInRadians);
            return Math.Sqrt((An * An + Bn * Bn) / (Ad * Ad + Bd * Bd));
        }

        public class BoundingBox
        {
            public double LatMin;
            public double LatMax;

            public double LngMin;
            public double LngMax;           
        }

        public static BoundingBox FindBoundingBox(double latitudeInDegrees, double longitudeInDegrees, double halfSideInKm)
        {
            var lat = deg2rad(latitudeInDegrees);
            var lng = deg2rad(longitudeInDegrees);
            var halfSide = 1000 * halfSideInKm;

            // Radius of the earth at the given latitude
            var radius = WGS84EarthRadius(lat);

            // Radius of the Parallel at teh given latitude
            var pradius = radius * Math.Cos(lat);

            BoundingBox box = new BoundingBox();
            box.LatMin = rad2deg(lat - halfSide / radius);
            box.LatMax = rad2deg(lat + halfSide / radius);
            box.LngMin = rad2deg(lng - halfSide / pradius);
            box.LngMax = rad2deg(lng + halfSide / pradius);

            return box;
        }
    }
}