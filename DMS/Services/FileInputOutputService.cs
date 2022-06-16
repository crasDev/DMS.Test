using DMS.Models;

namespace DMS.Services
{
    /// <summary>
    /// Service responsable to create all the txt files. <see cref="IDisposable"/> gives me access to use this class inside a using block if needed and control of disposing of objects
    /// </summary>
    internal class FileInputOutputService : IDisposable, IFileInputOutputService
    {
        private IEnumerable<Person> persons;
        // Please ensure that the executable has writting access to this directory
        private readonly string folderPath = Path.Combine(Environment.CurrentDirectory, "People");
        private readonly string filePath;

        /// <summary>
        /// The generated file size to be available to statistics
        /// </summary>
        public long FileSize { get; private set; }

        /// <summary>
        /// In order to have some LINQ as the exercise requires, storing this value here to after make a call to use it to select if need to delete the file
        /// </summary>
        public int RandomNumberIndex { get; private set; }

        /// <summary>
        /// Creates an I/O service for this <see cref="IEnumerable{Person}"/> at a given <see cref="randomNumberIndex"/>
        /// </summary>
        /// <param name="persons">The persons that match the random number</param>
        /// <param name="randomNumberIndex">The index for the file name</param>
        public FileInputOutputService(IEnumerable<Person> persons, int randomNumberIndex)
        {
            this.persons = persons ?? throw new ArgumentNullException(nameof(persons));
            this.RandomNumberIndex = randomNumberIndex;
            CreateDirectory();
            this.filePath = Path.Combine(folderPath, $"{randomNumberIndex}.txt");
        }

        public void CreateFile()
        {
            try
            {

                // Check if file already exists. If yes, delete it. This is because MAYBE some past test thrown an exception or the proccess did not end correctly
                // so we might have a "zombieee file"
                Console.WriteLine($"Creating file in the following path {filePath}");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                // Create a new file. There is no risk of concurrency because each file represents a diferent set   
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    // it seems that the objective is to have a ; separated file in the end... should we add the headers?
                    foreach (Person person in persons)
                    {
                        // we could use StringBuilder but interpolation is also perfomant

                        // although in the contrat of the exercise person.DateOfBirth could be null, the model in get, as we didn´t care for the actual value inside
                        // for simplicity I coded the getter to return always a value, so no need to verify if null here, it´s not possible.

                        sw.WriteLine($"{person.Id};{person.Name};{person.Surname};{person.DateOfBirth.Value:yyyMMddHHmmss}");
                    }


                }


                // retrivingg the value of the size of the file and store the value in the property for further use. Also if memory was a concern, we could at this moment clear also the field persons to clear out memory
                // persons = null

                FileInfo fileInfo = new(filePath);
                FileSize = fileInfo.Length;

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                // not rethrowing to keep the exception to bubble up
            }
        }

        public void DeleteFile()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private void CreateDirectory()
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);

            if (!di.Exists)
            {
                Directory.CreateDirectory(folderPath);
            }
        }


        public void Dispose()
        {
            this.persons = null;
        }
    }
}
