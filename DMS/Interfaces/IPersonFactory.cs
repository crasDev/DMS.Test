namespace DMS.Factories
{
    /// <summary>
    /// Interface for IPersonFactory, for DI or try other approaches
    /// </summary>
    internal interface IPersonFactory : IDisposable
    {
        void GenerateData();
    }
}