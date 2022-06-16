namespace DMS.Helper
{
    /// <summary>
    /// Generates names and surnames for our <see cref="Person"/> object.
    /// Internal class to be used only inside our assembly, we don´t want to expose this
    /// It could load from a JSON file, but I didnt found any in the internet that had sample data, so going just to throw some hardcoded values
    /// </summary>
    internal class NameGenerator
    {
        // as a private field so we avoid object creation on every iteration
        private static readonly Random rand = new();

        // Again this lists could be loaded from a File or API, but for sake of this development, they are hardcoded
        // https://www.ssa.gov/oact/babynames/ and https://www.ssa.gov/oact/babynames/decades/century.html
        private static readonly string[] possibleNames =
            {
            "Liam", "Noah", "Oliver", "Elijah", "James", "William",
            "Benjamin", "Lucas", "Henry", "Theodore", "Olivia", "Emma",
            "Charlotte", "Amelia", "Ava", "Sophia", "Isabella", "Mia",
            "Evelyn", "Harper", "James", "Mary", "Robert", "Patricia",
            "John", "Jennifer", "Michael", "Linda", "David", "Elizabeth",
            "William", "Barbara", "Richard", "Susan", "Joseph", "Jessica",
            "Thomas", "Sarah", "Charles", "Karen"};

        // https://www.al.com/news/2019/10/50-most-common-last-names-in-america.html
        private static readonly string[] possibleSurnames =
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia",
            "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez",
            "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore",
            "Jackson", "Martin", "Lee", "Perez", "Thompson", "White", "Harris",
            "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker",
            "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen",
            "Hill", "Flores", "Green", "Adams", "Nelson", "Baker", "Hall",
            "Rivera", "Campbell", "Mitchell", "Carter", "Roberts"
        };

        /// <summary>
        /// Generates a random name from a pre-defined list
        /// </summary>
        /// <returns></returns>
        public static string GenerateName()
        {
            return possibleNames[rand.Next(0, possibleNames.Length)];
        }

        /// <summary>
        /// Generates a random surname from a pre-defined list
        /// </summary>
        /// <returns></returns>
        public static string GenerateSurname()
        {
            return possibleSurnames[rand.Next(0, possibleSurnames.Length)];
        }
    }
}
