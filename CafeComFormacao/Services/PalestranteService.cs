using CafeComFormacao.Interfaces.Repositories;
using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Models;

namespace CafeComFormacao.Services
{
    public class PalestranteService : IPalestranteService
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly IEventoRepository _eventoRepository;

        public PalestranteService(IPalestranteRepository palestranteRepository, IEventoRepository eventoRepository)
        {
            _palestranteRepository = palestranteRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<ViewsModels> ListarPalestrantesEventos()
        {
            
            return new ViewsModels()
            {
                ListaPalestrante = await _palestranteRepository.ListarPalestrantes(),
                ListaDeEventos = await _eventoRepository.ListarEventos()
            };
        }

        public async Task InserirPalestranteNoEvento(int eventoId, int palestranteId)
        {
            EventoPalestrante eventoPalestrante = new(palestranteId, eventoId);

            await _palestranteRepository.InserirPalestranteNoEvento(eventoPalestrante);
        }
    }
}
