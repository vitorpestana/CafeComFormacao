using CafeComFormacao.Data;
using CafeComFormacao.Models;
using CafeComFormacao.Interfaces;

namespace CafeComFormacao.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly CafeComFormacaoContext _context;

        public EventoRepository(CafeComFormacaoContext context)
        {
            _context = context;
        }

        public async Task<List<Evento>> ListarEventos()
        {
            return await _context.Evento.ToListAsync();
        }

        public void AdicionarEventoAoUsuario(List<int> eventosSelecionados, int participanteId)
        {
            foreach (int idEvento in eventosSelecionados)
            {
                _context.UsuarioEvento.Add(new UsuarioEvento(participanteId, idEvento));
                _context.SaveChanges();
            }
        }

        public void CriarNovoEvento(Evento evento)
        {
            _context.Evento.Add(evento);
            _context.SaveChanges();
        }

        public async Task<List<Evento>> ListarEventosDoUsuario(int usuarioId)
        {

            return await (from Evento evento in _context.Evento
                          join UsuarioEvento usuarioEvento in _context.UsuarioEvento on evento.Id equals usuarioEvento.EventoId
                          where usuarioEvento.ParticipanteId == usuarioId
                          select evento).ToListAsync();
        }

        public async Task<Evento> EventoDaListaDeParticipantes(int idEvento)
        {
            return await (from Evento evento in _context.Evento
                          where evento.Id == idEvento
                          select evento).FirstOrDefaultAsync();
        }
    }
}
