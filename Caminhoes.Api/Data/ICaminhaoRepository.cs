using Caminhoes.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caminhoes.Api.Data
{
    public interface ICaminhaoRepository
    {
        Task<IEnumerable<Caminhao>> GetAllAsync();
        Task<Caminhao?> GetByIdAsync(string codigoChassi);
        Task<Caminhao> AddAsync(Caminhao caminhao);
        Task<Caminhao?> UpdateAsync(string codigoChassi, Caminhao caminhao);
        Task<bool> DeleteAsync(string codigoChassi);
    }
}
