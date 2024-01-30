using CafeComFormacao.Interfaces;
using CafeComFormacao.Models;

namespace CafeComFormacao.Services
{
    public class ViewsModelsService : IViewsModelsService
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IEventoRepository _eventoRepository;

        public ViewsModelsService(IParticipanteRepository participanteRepository, IEventoRepository eventoRepository)
        {
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<List<ViewsModels>> ListarCadaParticipantePorEvento(List<int> eventosIds)
        {
            List<ViewsModels> usuariosPorEvento = new();

            foreach (int idEvento in eventosIds)
            {

                List<Participante> participantes = await _participanteRepository.ListarUsuariosPorEvento(idEvento);

                if (participantes.Count > 0)
                {
                    ViewsModels viewsModels = new()
                    {
                        Evento = await _eventoRepository.EventoDaListaDeParticipantes(idEvento),
                        ListaDeParticipantes = participantes,
                    };

                    StatusPagamento statusPagamento = await _participanteRepository.ObterStatusPagamento(idEvento, viewsModels.ListaDeParticipantes.Select(lista => lista.Id).FirstOrDefault());
                    viewsModels.StatusPagamento = statusPagamento.ToString();

                    usuariosPorEvento.Add(viewsModels);
                }
            }

            return usuariosPorEvento;
        }

    }
}