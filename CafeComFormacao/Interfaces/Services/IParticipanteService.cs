using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces.Services
{
    public interface IParticipanteService
    {
        Task CriarParticipanteService(Cadastro participante);
        void InscreverEventoService(List<int> eventosSelecionados, int idUsuario);
        Task<List<Evento>> SelecaoEventoService();
        Task<List<ViewsModels>> UsuarioPorEventoService();
        Task<List<Participante>> ListarParcipantesService();
        Task<bool> VerificarExistenciaEmail(string email);
        Task<bool> VerificarExistenciaCelular(string celular);
    }
}
