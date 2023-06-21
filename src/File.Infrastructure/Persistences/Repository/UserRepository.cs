using File.Domain.Entities;
using File.Infrastructure.Persistences.Context;
using File.Infrastructure.Persistences.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<User> GetUserByEmail(string email)
    {
        User? user = await this.context.Users.FirstOrDefaultAsync(x => x.Email == email);
        return user!;
    }

    public async Task<User> LoginUser(User user)
    {
        User? code = await this.context.Users.FirstOrDefaultAsync(
            x => x.Email == user.Email && x.PasswordHash == user.PasswordHash
        );
        return code!;
    }
}
