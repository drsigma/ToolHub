using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


public class ToolRepository : IToolRepository
{
    private readonly MongoContext _context;

    public ToolRepository(MongoContext context)
    {
        _context = context;
    }

    public async Task<List<Tool>> GetAllAsync()
    {
        return await _context.Tools.Find(_ => true).ToListAsync();
    }

    public async Task<Tool> GetByIdAsync(Guid id)
    {
        return await _context.Tools.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Tool> GetByNameAsync(string name)
    {
        return await _context.Tools.Find(u => u.Name == name).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Tool tool)
    {
        await _context.Tools.InsertOneAsync(tool);
    }

    public async Task UpdateAsync(Guid id, Tool tool)
    {
        var filter = Builders<Tool>.Filter.Eq(u => u.Id, id);
        await _context.Tools.ReplaceOneAsync(filter, tool);
    }


    public async Task DeleteAsync(Guid id)
    {
        await _context.Users.DeleteOneAsync(u => u.Id == id);
    }
}
