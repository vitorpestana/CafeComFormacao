
using CafeComFormacao.Data;
using CafeComFormacao.Models;
using CafeComFormacao.Interfaces;

namespace CafeComFormacao.Repositories
{
    public class ParticipanteRepository : IParticipanteRepository
    {
        private readonly CafeComFormacaoContext _context;

        public ParticipanteRepository(CafeComFormacaoContext context)
        {
            _context = context;
        }

        public async Task<Participante> Inserir(Cadastro participante)
        {
            _context.CredenciaisParticipante.Add(new CredenciaisParticipante()
            {
                Id = participante.ParticipanteId,
                LoginEmail = participante.Email,
                Senha = participante.Senha
            });

            Participante participanteCadastrado = new()
            {
                Id = participante.ParticipanteId,
                Nome = participante.Nome,
                Email = participante.Email,
                Celular = participante.Celular,
                CursoLidere = participante.CursoLidere
            };

            _context.Participante.Add(participanteCadastrado);

            await  _context.SaveChangesAsync();

            return participanteCadastrado;
        }

        public async Task<List<Participante>> ListarParticipantes()
        {
            return await _context.Participante.ToListAsync();
        }

        public async Task<List<UsuarioEvento>> ListarRelacionamentoParticipanteEvento()
        {
            return await _context.UsuarioEvento.OrderBy(x => x.EventoId).ToListAsync();
        }

        public async Task<List<Participante>> ListarUsuariosPorEvento(int eventoId)
        {

            return await (from Participante participante in _context.Participante
                          join UsuarioEvento usuarioEvento in _context.UsuarioEvento on participante.Id equals usuarioEvento.ParticipanteId
                          join Evento evento in _context.Evento on usuarioEvento.EventoId equals evento.Id
                          where usuarioEvento.EventoId == eventoId
                          select participante).ToListAsync();

        }

        public async Task<CredenciaisParticipante> VerificarCredenciais(string usuario, string senha)
        {
            return await _context.CredenciaisParticipante.Where(x => x.LoginEmail.Trim() == usuario && x.Senha.Trim() == senha).FirstOrDefaultAsync();
        }

        public async Task<CredenciaisAdm> VerificarSeEhAdm(string usuario, string senha)
        {
            return await _context.CredenciaisAdm.Where(x => x.LoginEmail.Trim() == usuario && x.Senha.Trim() == senha).FirstOrDefaultAsync();
        }

        public async Task<int> ObterIdDoParticipante(Participante participante)
        {
            return await _context.Participante.Where(y => y.Email.Trim() == participante.Email.Trim() && participante.Id == y.Id).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<StatusPagamento> ObterStatusPagamento(int idParticipante, int idEvento)
        {
            return await (from UsuarioEvento usuarioEvento in _context.UsuarioEvento
                          where usuarioEvento.ParticipanteId == idParticipante && usuarioEvento.EventoId == idEvento
                          select usuarioEvento.PagamentoStatus).FirstOrDefaultAsync();
        }
    }
}
