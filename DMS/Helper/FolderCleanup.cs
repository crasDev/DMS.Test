namespace DMS.Helper
{
    internal static class FolderCleanup
    {
        public static void CleanUpFolder()
        {
            string folderPath = Path.Combine(Environment.CurrentDirectory, "People");

            if (Directory.Exists(folderPath))
            {
                Console.WriteLine($"Previous data found. Eliminating files and folder for fresh data incoming");
                Directory.Delete(folderPath, true);
            }
        }
    }
}
