using Holocron.Domain.Entities;

namespace Holocron.Domain.Interfaces.Repositories
{
    public interface IInhabitantRepository
    {
        Task<Inhabitant> GetByIdAsync(Guid id);
        Task<IEnumerable<Inhabitant>> GetAllAsync();
        Task<Inhabitant> AddAsync(Inhabitant inhabitant);
        Task UpdateAsync(Inhabitant inhabitant);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Inhabitant>> GetRebelsAsync();
        Task<IEnumerable<Inhabitant>> SearchInhabitantsAsync(string query);

    }
}
