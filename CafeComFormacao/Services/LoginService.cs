using CafeComFormacao.Data;

namespace CafeComFormacao.Services
{
    public class LoginService
    {

        private readonly CafeComFormacaoContext _context;

        public LoginService(CafeComFormacaoContext context)
        {
            _context = context;
        }

        public async Task<bool> VerificarUsuario(string usuario, string senha)
        {
            var ehUsuario = await _context.Participante.Where(x => x.Nome.Trim() == usuario && x.Senha.Trim() == senha).ToListAsync();
            return ehUsuario.Any();
        }
        
        public async Task<bool> VerificarSeEhAdmin(string usuario, string senha)
        {
            var ehAdmin = await _context.Participante.Where(x => x.Nome.Trim() == usuario && x.Senha.Trim() == senha && x.Admin == true).ToListAsync();
            return ehAdmin.Any();
        }
    }
}
