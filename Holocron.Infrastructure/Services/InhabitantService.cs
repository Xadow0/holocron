using Holocron.Domain.Entities;
using Holocron.Domain.Interfaces.Repositories;
using Holocron.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holocron.Application.DTOs.Inhabitants;
using AutoMapper;

namespace Holocron.Infrastructure.Services
{
    public class InhabitantService : IInhabitantService
    {
        private readonly IInhabitantRepository _inhabitantRepository;

        public InhabitantService(IInhabitantRepository inhabitantRepository)
        {
            _inhabitantRepository = inhabitantRepository;
        }

        public async Task<IEnumerable<InhabitantReadDto>> GetAllInhabitantsAsync()
        {
            var inhabitants = await _inhabitantRepository.GetAllAsync();
            return inhabitants.Select(i => new InhabitantReadDto
            {
                Id = i.Id,
                Name = i.Name,
                Species = i.Species,
                Origin = i.Origin,
                IsSuspectedRebel = i.IsSuspectedRebel,
                CreatedAt = i.CreatedAt
            });
        }

        public async Task<InhabitantReadDto?> GetInhabitantByIdAsync(Guid id)
        {
            var inhabitant = await _inhabitantRepository.GetByIdAsync(id);
            return inhabitant == null ? null : new InhabitantReadDto
            {
                Id = inhabitant.Id,
                Name = inhabitant.Name,
                Species = inhabitant.Species,
                Origin = inhabitant.Origin,
                IsSuspectedRebel = inhabitant.IsSuspectedRebel,
                CreatedAt = inhabitant.CreatedAt
            };
        }

        public async Task CreateInhabitantAsync(InhabitantCreateDto dto)
        {
            var inhabitant = new Inhabitant(dto.Name, dto.Species, dto.Origin, dto.IsSuspectedRebel);
            await _inhabitantRepository.AddAsync(inhabitant);
        }

        public async Task UpdateInhabitantAsync(Guid id, InhabitantCreateDto dto)
        {
            var inhabitant = await _inhabitantRepository.GetByIdAsync(id);
            if (inhabitant == null) return;

            inhabitant.Update(dto.Name, dto.Species, dto.Origin, dto.IsSuspectedRebel);
            await _inhabitantRepository.UpdateAsync(inhabitant);
        }

        public async Task DeleteInhabitantAsync(Guid id)
        {
            await _inhabitantRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<InhabitantReadDto>> GetRebelsAsync()
        {
            var rebels = await _inhabitantRepository.GetRebelsAsync();
            return rebels.Select(r => new InhabitantReadDto
            {
                Id = r.Id,
                Name = r.Name,
                Species = r.Species,
                Origin = r.Origin,
                IsSuspectedRebel = r.IsSuspectedRebel,
                CreatedAt = r.CreatedAt
            });
        }

        public async Task<IEnumerable<InhabitantReadDto>> SearchInhabitantsAsync(string query)
        {
            var inhabitants = await _inhabitantRepository.SearchInhabitantsAsync(query);
            return inhabitants.Select(i => new InhabitantReadDto
            {
                Id = i.Id,
                Name = i.Name,
                Species = i.Species,
                Origin = i.Origin,
                IsSuspectedRebel = i.IsSuspectedRebel,
                CreatedAt = i.CreatedAt
            });
        }


    }
}
