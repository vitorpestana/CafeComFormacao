using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces
{
    public interface IEventoRepository
    {
        Task<List<Evento>> ListarEventos();
        void AdicionarEventoAoUsuario(List<int> eventosSelecionados, int participanteId);
        void CriarNovoEvento(Evento evento);
        Task<List<Evento>> ListarEventosDoUsuario(int usuarioId);
        Task<Evento> EventoDaListaDeParticipantes(int idEvento);
    }
}
