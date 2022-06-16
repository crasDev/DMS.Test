using DMS.Models;
using System.Collections.Concurrent;

namespace DMS.Factories
{
    /// <summary>
    /// Factory for the required number of <see cref="Person"/>
    /// Also internal as it´s something just for this Assembly, if this would be a real project we would expose results, using another public class, but leaving the factory isolated
    /// </summary>
    internal class PersonFactory : IDisposable, IPersonFactory
    {
        // this value could come from a configuration file =)
        private int maxNumberPersons = 2000000;

        // as we are going to generate the 20000000 in multiple threads, so to avoid concurrency issues and also have a thread-safe environment we can store them here
        /// <summary>
        /// <see cref="ConcurrentDictionary{int, ConcurrentBag{Person}}"/> to hold as key the random number and value a <see cref="ConcurrentBag{Person}"/> to make the creation of the files easier
        /// </summary>
        public ConcurrentDictionary<int, ConcurrentBag<Person>>? PersonsByRandomNumber = new();

        private int objectsCreated = 0;

        public void Dispose()
        {
            PersonsByRandomNumber = null;
        }

        /// <summary>
        /// Data generation. Although it´s run with a parallel.For and also inside a Task.Factory, async is not needed.
        /// </summary>
        public void GenerateData()
        {

            if (PersonsByRandomNumber == null)
            {
                PersonsByRandomNumber = new();
            }

            Console.WriteLine($"Creating data, please wait... ");

            // Getting out of Main Thread
            var tasks = Task.Factory.StartNew(() =>
            {
                Parallel.For(0, maxNumberPersons, i =>
                {
                    Person createdPerson = new();
                    if (PersonsByRandomNumber.TryGetValue(createdPerson.RandomNumber, out ConcurrentBag<Person> personBag))
                    {
                        personBag.Add(createdPerson);
                        PersonsByRandomNumber[createdPerson.RandomNumber] = personBag;
                    }
                    else
                    {
                        PersonsByRandomNumber.TryAdd(createdPerson.RandomNumber, new ConcurrentBag<Person> { createdPerson });
                    }


                });

                // although super efficient against a normal for loop, the Parallel.For, even when we indicate a value as upper limit of number of iterations, sometimes it fails to generate all iterations
                // as the toExclusive parameter is aproximate, but for a gain of almost 60% in generating this data, it´s affordable to do like this, generating the max possible results in multi-thread
                // as the processor can handle, and the rest that is normally a very reduced number like 1 to 9 iterations, can be added in a normal for loop
                // Attention: not always Parallel.For gives us such performance, I did test the results using my personal computer, and they seemed very profitable in terms of performance

                foreach (var item in PersonsByRandomNumber)
                {
                    objectsCreated += item.Value.Count;
                }

                if (objectsCreated != maxNumberPersons)
                {
                    for (int i = 0; i < maxNumberPersons - objectsCreated; i++)
                    {
                        Person createdPerson = new();
                        if (PersonsByRandomNumber.TryGetValue(createdPerson.RandomNumber, out ConcurrentBag<Person> personBag))
                        {
                            personBag.Add(createdPerson);
                            PersonsByRandomNumber[createdPerson.RandomNumber] = personBag;
                        }
                        else
                        {
                            PersonsByRandomNumber.TryAdd(createdPerson.RandomNumber, new ConcurrentBag<Person> { createdPerson });
                        }
                        
                    }
                }
            });

            tasks.Wait();

            // using a parallel foreach to generate the data the fastest way possible, using multiple threads


            Console.Clear();
            Console.WriteLine($"Creation of person data completed with success");
        }
    }
}
