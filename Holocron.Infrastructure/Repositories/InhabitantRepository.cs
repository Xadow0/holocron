using Holocron.Domain.Entities;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Holocron.Infrastructure.Repositories
{
    public class InhabitantRepository : IInhabitantRepository
    {
        private readonly ApplicationDbContext _context;

        public InhabitantRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Inhabitant>> GetAllAsync()
        {
            return await _context.Inhabitants.AsNoTracking().ToListAsync();
        }

        public async Task<Inhabitant?> GetByIdAsync(Guid id)
        {
            return await _context.Inhabitants.FindAsync(id);
        }

        public async Task<Inhabitant> AddAsync(Inhabitant inhabitant)
        {
            await _context.Inhabitants.AddAsync(inhabitant);
            await _context.SaveChangesAsync();
            return inhabitant;
        }

        public async Task UpdateAsync(Inhabitant inhabitant)
        {
            _context.Inhabitants.Update(inhabitant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var inhabitant = await _context.Inhabitants.FindAsync(id);
            if (inhabitant != null)
            {
                _context.Inhabitants.Remove(inhabitant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Inhabitant>> GetRebelsAsync()
        {
            return await _context.Inhabitants
                .Where(i => i.IsSuspectedRebel)
                .ToListAsync();
        }

        public async Task<IEnumerable<Inhabitant>> SearchInhabitantsAsync(string query)
        {
            return await _context.Inhabitants
                .Where(i => i.Name.Contains(query) || i.Species.Contains(query))
                .ToListAsync();
        }

    }
}
