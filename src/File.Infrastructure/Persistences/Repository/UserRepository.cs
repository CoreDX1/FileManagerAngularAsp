using File.Domain.Entities;
using File.Infrastructure.Persistences.Context;
using File.Infrastructure.Persistences.Interfaces;

namespace File.Infrastructure.Persistences.Repository;

public class UserRepository : IUserRepository
{
    private FileManagerContext context;

    public UserRepository(FileManagerContext context)
    {
        this.context = context;
    }

    public async Task<bool> AddUser(User user)
    {
        await this.context.AddAsync(user);
        int result = await this.context.SaveChangesAsync();
        return result > 0;
    }
}
