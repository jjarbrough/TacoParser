using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");
            var lines = File.ReadAllLines(csvPath);
            if (lines.Length == 0)
            {
                logger.LogFatal("zero lines");
            }
            else if (lines.Length == 1)
            {
                logger.LogWarning("one line");
            }
            logger.LogInfo($"Lines: {lines[0]}");
            var parser = new TacoParser();
            ITrackable tacoBellA = null;
            ITrackable tacoBellB = null;
            string nameA = "";
            string nameB = "";
            double furthestDistance = 0.0;
            for (int i = 0; i < lines.Length; i++)
            {
                tacoBellA = parser.Parse(lines[i]);
                GeoCoordinate corA = new GeoCoordinate(tacoBellA.Location.Latitude, tacoBellA.Location.Longitude);
                for (int j = 0; j < lines.Length; j++)
                {
                    tacoBellB = parser.Parse(lines[j]);
                    GeoCoordinate corB = new GeoCoordinate(tacoBellB.Location.Latitude, tacoBellB.Location.Longitude);
                    double distance = corA.GetDistanceTo(corB);
                    if (distance > furthestDistance)
                    {
                        furthestDistance = distance;
                        nameA = tacoBellA.Name;
                        nameB = tacoBellB.Name;
                    }
                }
            }
            Console.WriteLine($"The furthest distance between two taco bells given is {furthestDistance} meters");
            Console.WriteLine($"the first store is {nameA}, the second store is {nameB}");
        }
    }
}
