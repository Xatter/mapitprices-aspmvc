using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapItPrices.Models
{
    /// <summary>
    /// The distance type to return the results in.
    /// </summary>
    public enum DistanceType { Miles, Kilometers };
    /// <summary>
    /// Specifies a Latitude / Longitude point.
    /// </summary>
    public struct Position
    {
        public double Latitude;
        public double Longitude;
    }

    public class Haversine
    {

        public static double Distance(double? lat1, double? long1, double? lat2, double? long2)
        {
            if (lat1 == null || long1 == null || lat2 == null || long2 == null)
                return 0;

            Position pos1 = new Position() {
                Latitude = lat1 ?? 0,
                Longitude = long1 ?? 0
            };
            
            Position pos2 = new Position()
            {
                Latitude = lat2 ?? 0,
                Longitude = long2 ?? 0
            };

            return Haversine.Distance(pos1, pos2, DistanceType.Miles);
        }

        /// <summary>
        /// Returns the distance in miles or kilometers of any two
        /// latitude / longitude points.
        /// </summary>
        /// <param name=”pos1″></param>
        /// <param name=”pos2″></param>
        /// <param name=”type”></param>
        /// <returns></returns>
        public static double Distance(Position pos1, Position pos2, DistanceType type)
        {
            double R = (type == DistanceType.Miles) ? 3960 : 6371;
            double dLat = Haversine.toRadian(pos2.Latitude - pos1.Latitude);
            double dLon = Haversine.toRadian(pos2.Longitude - pos1.Longitude);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(Haversine.toRadian(pos1.Latitude)) * Math.Cos(Haversine.toRadian(pos2.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;
            return d;
        }
        /// <summary>
        /// Convert to Radians.
        /// </summary>
        /// <param name=”val”></param>
        /// <returns></returns>
        public static double toRadian(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
