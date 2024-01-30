using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces
{
    public interface IEventoService
    {
        void CriarNovoEventoService(string nomeDoEvento, DateTime dataDoEvento, string horaDoEvento, double valorDoEvento);
        Task<List<Evento>> ListarTodosOsEventosDoUsuarioService(int idUsuario);
    }
}
