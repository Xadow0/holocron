using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Holocron.Domain.Entities;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Infrastructure.Persistence;

namespace Holocron.Infrastructure.Repositories
{
    public class InhabitantRepository : IInhabitantRepository
    {
        private readonly ApplicationDbContext _context;

        public InhabitantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Inhabitant> GetByIdAsync(Guid id)
        {
            return await _context.Inhabitants
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Inhabitant>> GetAllAsync()
        {
            return await _context.Inhabitants.ToListAsync();
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
            var inhabitant = await GetByIdAsync(id);
            if (inhabitant != null)
            {
                _context.Inhabitants.Remove(inhabitant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Inhabitant>> GetRebelInhabitantsAsync()
        {
            return await _context.Inhabitants
                .Where(i => i.IsSuspectedRebel)
                .ToListAsync();
        }
    }
}