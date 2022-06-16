using DMS.Factories;
using DMS.Helper;

namespace DMS.Services
{
    /// <summary>
    /// BL aggregator
    /// </summary>
    internal class DMSService
    {
        
        private readonly PersonFactory factory;
        private List<FileInputOutputService> fileServices = new();

        // will consider that we could have a DI engine and so we would receive the factory and the service on the constructor
        public DMSService(PersonFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));

        }

        public void Execute()
        {
            // Generate the 2.000.000 persons
            factory.GenerateData();

            // create files
            if (factory.PersonsByRandomNumber == null)
            {
                string message = $"For some reason we do not have any person created, and the in memory data is null. Please check {nameof(factory.PersonsByRandomNumber)}";
                Console.WriteLine(message);
                throw new Exception(message);
            }

            foreach (var item in factory.PersonsByRandomNumber)
            {
                FileInputOutputService fileService = new(item.Value, item.Key);
                fileService.CreateFile();
                fileServices.Add(fileService);

                // we add already information of this file to make the iteration easier
                StatisticsCalculus.AddFileInformation(item.Key, fileService.FileSize);
            }

            // Calculate average and remove all files below the average number

            double average = StatisticsCalculus.CalculateAverage();
            Console.WriteLine($"The average size of the generated files is {average}");

            foreach (var item in fileServices)
            {
                if (item.FileSize < average)
                {
                    Console.WriteLine($"File with name {item.RandomNumberIndex}.txt was deleted. Size {item.FileSize} < Average {average}");
                    item.DeleteFile();
                }
            }

            Console.WriteLine($"Thanks for using our system. Hope you have enjoyed");
        }
    }
}
