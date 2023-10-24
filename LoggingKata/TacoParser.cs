namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
           // logger.LogInfo("Begin parsing");
            var cells = line.Split(',');
            if (cells.Length < 3)
            {
                logger.LogError("less than three cells");
                return null;
            }
            double latitude = double.Parse(cells[0]);
            double longitude = double.Parse(cells[1]);
            string name = cells[2];
            TacoBell localTBell = new TacoBell();
            Point localPoint = new Point();
            localPoint.Latitude = latitude;
            localPoint.Longitude = longitude;
            localTBell.Name = name;
            localTBell.Location = localPoint;
            return localTBell;
        }
    }
}