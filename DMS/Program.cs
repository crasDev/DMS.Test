// Although this is a .net 6 project, I imported the old template for console apps

using DMS.Factories;
using DMS.Helper;
using DMS.Services;

namespace DMS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // create an instance of the factory. DI would be good for that one =) 

                PersonFactory factory = new();

                // we should clean the contents of the folder, so everytime this runs, we have fresh new data

                FolderCleanup.CleanUpFolder();

                DMSService mainService = new(factory);

                mainService.Execute();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"The system found an exception. Message : {ex.Message} Stack Trace: {ex.StackTrace}");
                throw;
            } 
        }
    }
}