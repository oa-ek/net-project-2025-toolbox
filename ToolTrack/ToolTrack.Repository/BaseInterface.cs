namespace ToolTrack.Repository
{
    public interface BaseInterface<T>
    {
        Task CreateAsync(T entity);
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
