using CafeComFormacao.Data;
using CafeComFormacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeComFormacao.Services
{
    public class BancoDeDadosService
    {
        private readonly CafeComFormacaoContext _context;

        public BancoDeDadosService(CafeComFormacaoContext context)
        {
            _context = context;
        }

        public void Inserir(Participante participante)
        {
            _context.Add(participante);
            _context.SaveChanges();
        }

        public async Task<List<Participante>> ListarParticipantes()
        {
            return await _context.Participante.ToListAsync();
        }

    }
}
