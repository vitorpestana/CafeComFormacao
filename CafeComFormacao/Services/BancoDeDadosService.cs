using CafeComFormacao.Data;
using CafeComFormacao.Models;

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
    }
}
