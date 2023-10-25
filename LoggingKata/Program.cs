using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Media;
using System.Threading;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            SoundPlayer player = new SoundPlayer("NzY3NjM2OTc2NzY3NzU4_7Fm3oTONyUQ.wav");
            player.Load();
            player.Play();
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
            var distanceMiles = (furthestDistance / 1609.3);
            Console.WriteLine($"The furthest distance between two of the taco bells given is {Math.Round(furthestDistance)} meters");
            Console.WriteLine($"thats {Math.Round(distanceMiles)} Miles!");
            Console.WriteLine($"the first store is {nameA}, the second store is {nameB}");
            Console.WriteLine($"Driving at 60 miles per hour it would take you about {Math.Round(distanceMiles/60)} hours to get there");
            Console.WriteLine($"If you were to eat a taco every two minutes you would need {Math.Round((distanceMiles/60) * 30)} tacos for the trip!");
            Console.WriteLine("Whew, thats a lot of tacos!");
            Console.WriteLine($"flying at 500 miles per hour it would take you {distanceMiles / 500} hours to get there");
            Console.WriteLine($"Thats only {Math.Round((distanceMiles / 500)*60)} minutes");
            Console.WriteLine($"You would only need {Math.Round((distanceMiles / 500) * 30)} tacos for the flight");
            Console.WriteLine(@"
  _______                  _ 
 |__   __|                | |
    | | __ _  ___ ___  ___| |
    | |/ _` |/ __/ _ \/ __| |
    | | (_| | (_| (_) \__ \_|
    |_|\__,_|\___\___/|___(_)");
            Console.WriteLine(@"
               ._-'-_ .
          . '  /_-_-_\   ` .
       .'     |-_-_-_-|      `.
      (       `.-_-_-.'        )
      !`.                    .'!
        ! ` .            . ' !
          ! ! ! ! ! ! ! !  !
            / /       \ \
          _-| \___ ___/ /-_
         (_ )__\_)\(_/__( _)
             ))))\X\ ((((
               \/ \/");
            Thread.Sleep( 30000 );

        }
    }
}
