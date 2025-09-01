using Caminhoes.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caminhoes.Api.Data
{
    public class CaminhaoRepository : ICaminhaoRepository
    {
        private readonly CaminhoesDbContext _context;
        public CaminhaoRepository(CaminhoesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Caminhao>> GetAllAsync()
        {
            return await _context.Caminhoes.ToListAsync();
        }

        public async Task<Caminhao?> GetByIdAsync(string codigoChassi)
        {
            return await _context.Caminhoes.FindAsync(codigoChassi);
        }

        public async Task<Caminhao> AddAsync(Caminhao caminhao)
        {
            // Garante unicidade do código do chassi
            var exists = await _context.Caminhoes.AnyAsync(c => c.CodigoChassi == caminhao.CodigoChassi);
            if (exists)
                throw new System.Exception("Já existe um caminhão com este código de chassi.");
            _context.Caminhoes.Add(caminhao);
            await _context.SaveChangesAsync();
            return caminhao;
        }

        public async Task<Caminhao?> UpdateAsync(string codigoChassi, Caminhao caminhao)
        {
            var existing = await _context.Caminhoes.FindAsync(codigoChassi);
            if (existing == null) return null;
            existing.Modelo = caminhao.Modelo;
            existing.AnoFabricacao = caminhao.AnoFabricacao;
            existing.Cor = caminhao.Cor;
            existing.Planta = caminhao.Planta;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(string codigoChassi)
        {
            var caminhao = await _context.Caminhoes.FindAsync(codigoChassi);
            if (caminhao == null) return false;
            _context.Caminhoes.Remove(caminhao);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
