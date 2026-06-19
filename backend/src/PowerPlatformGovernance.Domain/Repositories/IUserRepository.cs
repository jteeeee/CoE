using PowerPlatformGovernance.Domain.Entities;

namespace PowerPlatformGovernance.Domain.Repositories;

public interface IUserRepository
{
    Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<User?> GetByIdAsync(string userId, CancellationToken cancellationToken = default);
}
