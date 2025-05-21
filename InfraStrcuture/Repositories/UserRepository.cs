using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


public class UserRepository : IUserRepository
{
    private readonly MongoContext _context;

    public UserRepository(MongoContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.Find(_ => true).ToListAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User> GetByNameAsync(string name)
    {
        return await _context.Users.Find(u => u.Name == name).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(User user)
    {
        await _context.Users.InsertOneAsync(user);
    }

    public async Task UpdateAsync(Guid id, User updatedUser)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, id);
        await _context.Users.ReplaceOneAsync(filter, updatedUser);
    }


    public async Task DeleteAsync(Guid id)
    {
        await _context.Users.DeleteOneAsync(u => u.Id == id);
    }
}
