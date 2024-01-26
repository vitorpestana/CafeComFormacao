using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces
{
    public interface IParticipanteRepository
    {
        Task<int> Inserir(Participante participante);
        Task<List<Participante>> ListarParticipantes();
        Task<List<UsuarioEvento>> ListarRelacionamentoParticipanteEvento();
        Task<List<Participante>> ListarUsuariosPorEvento(int eventoId);
        Task<Dictionary<Evento, List<Participante>>> ListarCadaParticipantePorEvento(List<int> eventosIds);
        Task<Participante> VerificarCredenciais(string usuario, string senha);
    }
}
