using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Models;

namespace CafeComFormacao.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public void CriarNovoEventoService(string nomeDoEvento, DateTime dataDoEvento, string horaDoEvento, double valorDoEvento)
        {
            Evento evento = new()
            {
                NomeDoEvento = nomeDoEvento,
                DataDoEvento = dataDoEvento.Date,
                HoraDoEvento = horaDoEvento,
                ValorDoEvento = valorDoEvento
            };

            _eventoRepository.CriarNovoEvento(evento);
        }

        public async Task<List<Evento>> ListarTodosOsEventosDoUsuarioService(int idUsuario)
        {
            return await _eventoRepository.ListarEventosDoUsuario(idUsuario);
        }
    }
}
