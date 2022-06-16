using DMS.Helper;

namespace DMS.Models
{
    internal class Person
    {
        #region Private fields to hold values
        private Guid id = Guid.Empty;
        private string name = string.Empty;
        private string surname = string.Empty;
        private DateTime? dateOfBirth = null;
        // we want it to run in the first time
        private int randomNumber = -1;
        #endregion

        #region Public properties
        /// <summary>
        /// <see cref="Id"/> property of <see cref="Person"/>
        /// It is the unique identifier for the instance, and it´s generated in an automated manner
        /// </summary>
        public Guid Id
        {
            get
            {
                if (id == Guid.Empty)
                {
                    id = Guid.NewGuid();
                }
                return id;
            }
        }

        /// <summary>
        /// <see cref="Name"/> property of <see cref="Person"/>
        /// It´s a placeholder, so it´s initialized with a random string 
        /// </summary>
        public string Name
        {
            get 
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = NameGenerator.GenerateName();
                }
                return name;
            }
        }

        /// <summary>
        /// <see cref="Surname"/> property of <see cref="Person"/>
        /// It´s a placeholder, so it´s initialized with a random string 
        /// </summary>
        public string Surname
        {
            get
            {
                if (string.IsNullOrEmpty(surname))
                {
                    surname = NameGenerator.GenerateSurname();
                }
                return surname;
            }
        }

        /// <summary>
        /// <see cref="DateOfBirth"/> property of <see cref="Person"/>
        /// It´s not important the content of this property, so it generates a <see cref="DateTime"/> using the current date ticks value minus the <see cref="RandomNumber"/>
        /// </summary>
        public DateTime? DateOfBirth
        {
            get
            {
                if (dateOfBirth == null || !dateOfBirth.HasValue)
                {
                    dateOfBirth = new DateTime(DateTime.Now.Ticks - randomNumber);
                }
                return dateOfBirth;
            }
        }

        /// <summary>
        /// <see cref="RandomNumber"/> property of <see cref="Person"/>
        /// A random number is hold in this property using <see cref="Random"/> with min = 0 and max = 100
        /// </summary>
        public int RandomNumber
        {
            get
            {
                if (randomNumber < 0 || randomNumber > 100)
                {
                    // this syntax is C# 9.0. Just noting in case of compilation errors on code instatiation of object that have only the new keyword without the type, its C# 9.0
                    Random random = new();
                    randomNumber = random.Next(0, 100);
                }
                return randomNumber;
            }
        }

        #endregion
    }
}
