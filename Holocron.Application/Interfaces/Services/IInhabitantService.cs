using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Holocron.Application.DTOs.Inhabitants;

namespace Holocron.Application.Interfaces.Services
{
    public interface IInhabitantService
    {
        Task<IEnumerable<InhabitantReadDto>> GetAllInhabitantsAsync();
        Task<InhabitantReadDto?> GetInhabitantByIdAsync(Guid id);
        Task CreateInhabitantAsync(InhabitantCreateDto dto);
        Task UpdateInhabitantAsync(Guid id, InhabitantCreateDto dto);
        Task DeleteInhabitantAsync(Guid id);
        Task<IEnumerable<InhabitantReadDto>> GetRebelsAsync();
        Task<IEnumerable<InhabitantReadDto>> SearchInhabitantsAsync(string query);

    }
}
