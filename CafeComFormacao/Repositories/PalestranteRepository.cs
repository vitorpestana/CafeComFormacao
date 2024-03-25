using CafeComFormacao.Data;
using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeComFormacao.Repositories
{
    public class PalestranteRepository : IPalestranteRepository
    {
        private readonly CafeComFormacaoContext _context;

        public PalestranteRepository(CafeComFormacaoContext context)
        {
            _context = context;
        }

        public async Task InserirPalestrante(Palestrante palestrante)
        {
            _context.Palestrante.Add(palestrante);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Palestrante>> ListarPalestrantes()
        {
            return await _context.Palestrante.ToListAsync();
        }

        public async Task InserirPalestranteNoEvento(EventoPalestrante eventoPalestrante)
        {
            _context.EventoPalestrante.Add(eventoPalestrante);

            await _context.SaveChangesAsync();
        }
    }
}
