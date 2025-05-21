public interface IToolRepository
{
    Task<List<Tool>> GetAllAsync();
    Task<Tool> GetByIdAsync(Guid id);
    Task<Tool> GetByNameAsync(string name);
    Task CreateAsync(Tool tool);
    Task UpdateAsync(Guid id, Tool tool);
    Task DeleteAsync(Guid id);
}
