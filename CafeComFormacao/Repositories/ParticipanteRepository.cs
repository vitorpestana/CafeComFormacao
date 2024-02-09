
using CafeComFormacao.Data;
using CafeComFormacao.Models;
using CafeComFormacao.Interfaces.Repositories;

namespace CafeComFormacao.Repositories
{
    public class ParticipanteRepository : IParticipanteRepository
    {
        private readonly CafeComFormacaoContext _context;

        public ParticipanteRepository(CafeComFormacaoContext context)
        {
            _context = context;
        }

        public async Task InserirParticipante(Cadastro participante)
        {
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
        }

        public async Task InserirCredenciais(Cadastro participante)
        {
            CredenciaisParticipante credenciaisParticipante = new()
            {
                Id = participante.ParticipanteId,
                LoginEmail = participante.Email,
                Senha = participante.Senha
            };

            _context.CredenciaisParticipante.Add(credenciaisParticipante);

            await _context.SaveChangesAsync();
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
            return await _context.CredenciaisParticipante.Where(x => x.LoginEmail.Trim() == usuario).FirstOrDefaultAsync();
        }

        public async Task<CredenciaisAdm> VerificarSeEhAdm(string usuario, string senha)
        {
            return await _context.CredenciaisAdm.Where(x => x.LoginEmail.Trim() == usuario).FirstOrDefaultAsync();
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

        public async Task<bool> VerificarSeOEmailExiste(string email)
        {
            return await _context.CredenciaisParticipante.AnyAsync(x => x.LoginEmail == email);
        }

        public async Task<bool> VerificarSeOCelularExiste(string celular)
        {
            return await _context.Participante.AnyAsync(x => x.Celular == celular);
        }
    }
}
