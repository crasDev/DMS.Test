namespace DMS.Helper
{
    /// <summary>
    /// Class to calculate statistical data for the files created
    /// </summary>
    internal static class StatisticsCalculus
    {
        private static readonly Dictionary<int, long> fileSizes = new();

        public static void AddFileInformation(int randomNumber, long fileSize)
        {
            // we could just have the filesize, but having also the randomNumber we can verify if something fishy happenend in the file creation
            // if we want just a clean method, we could have just the filesize and a list<long> for example. Or even u
            if (!fileSizes.TryGetValue(randomNumber, out long value))
            {
                fileSizes.Add(randomNumber, fileSize);
            }
            else
            {
                // we could create a specific exception for this FileValidationException : Exception
                throw new Exception($"The key {randomNumber} already exists, this represents that exist 2 concurrent files");
            }
        }

        public static double CalculateAverage()
        {
            // a little bit of linq, just to find the average
            return Queryable.Average(fileSizes.Values.AsQueryable());
        }

        // we could add here more statistical methods
    }
}
