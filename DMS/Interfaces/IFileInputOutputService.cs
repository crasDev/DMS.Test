namespace DMS.Services
{
    internal interface IFileInputOutputService : IDisposable
    {
        long FileSize { get; }
        int RandomNumberIndex { get; }

        void CreateFile();
        void DeleteFile();

    }
}