public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByNameAsync(string name);
    Task CreateAsync(User user);
    Task UpdateAsync(Guid id, User updatedUser);
    Task DeleteAsync(Guid id);
}
