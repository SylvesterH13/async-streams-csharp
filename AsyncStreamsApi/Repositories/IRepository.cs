namespace AsyncStreamsApi.Repositories
{
    public interface IRepository<T>
    {
        IAsyncEnumerable<T> GetAsync();
    }
}
