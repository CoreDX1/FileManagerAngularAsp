using File.Domain.Entities;

namespace File.Infrastructure.Persistences.Interfaces;

public interface IUserRepository
{
    Task<bool> AddUser(User user);
}
