
using CafeComFormacao.Data;
using CafeComFormacao.Models;
using CafeComFormacao.Interfaces;

namespace CafeComFormacao.Repositories
{
    public class ParticipanteRepository : IParticipanteRepository
    {
        private readonly CafeComFormacaoContext _context;
        private readonly IEventoRepository _eventoRepository;

        public ParticipanteRepository(CafeComFormacaoContext context, IEventoRepository eventoRepository)
        {
            _context = context;
            _eventoRepository = eventoRepository;
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

        public async Task<Dictionary<Evento, List<Participante>>> ListarCadaParticipantePorEvento(List<int> eventosIds)
        {
            Dictionary<Evento, List<Participante>> usuariosPorEvento = new();

            foreach (int idEvento in eventosIds)
            {
                usuariosPorEvento.Add(await _eventoRepository.EventoDaListaDeParticipantes(idEvento), await ListarUsuariosPorEvento(idEvento));
            }

            List<Evento> listaARemover = usuariosPorEvento.Where(listaParticipante => listaParticipante.Value.Count == 0).Select(parChaveValor => parChaveValor.Key).ToList();
            foreach (Evento chave in listaARemover)
            {
                usuariosPorEvento.Remove(chave);
            };

            return usuariosPorEvento;
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
    }
}
