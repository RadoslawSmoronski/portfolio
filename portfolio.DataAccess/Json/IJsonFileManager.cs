namespace portfolio.DataAccess.Json
{
    public interface IJsonFileManager
    {
        string RootDirectoryPath { get; }
        public T Get<T>();
        public void Save<T>(T entity);
    }
}
