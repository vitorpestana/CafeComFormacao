using CafeComFormacao.Data;
using CafeComFormacao.Models;

namespace CafeComFormacao.Services
{
    public class LoginService
    {

        private readonly CafeComFormacaoContext _context;

        public LoginService(CafeComFormacaoContext context)
        {
            _context = context;
        }

        public async Task<(bool, Participante)> VerificarUsuario(string usuario, string senha)
        {
            Participante participante = await _context.Participante.Where(x => x.Nome.Trim() == usuario && x.Senha.Trim() == senha).FirstOrDefaultAsync();

            return (participante.Admin, participante);
        }
        
        public async Task<bool> VerificarSeEhAdmin(string usuario, string senha)
        {
            var ehAdmin = await _context.Participante.Where(x => x.Nome.Trim() == usuario && x.Senha.Trim() == senha && x.Admin == true).ToListAsync();
            return ehAdmin.Any();
        }
    }
}
