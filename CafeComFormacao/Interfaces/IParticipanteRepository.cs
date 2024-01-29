using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces
{
    public interface IParticipanteRepository
    {
        Task<Participante> Inserir(Cadastro participante);
        Task<List<Participante>> ListarParticipantes();
        Task<List<UsuarioEvento>> ListarRelacionamentoParticipanteEvento();
        Task<List<Participante>> ListarUsuariosPorEvento(int eventoId);
        Task<Dictionary<Evento, List<Participante>>> ListarCadaParticipantePorEvento(List<int> eventosIds);
        Task<CredenciaisParticipante> VerificarCredenciais(string usuario, string senha);
        Task<CredenciaisAdm> VerificarSeEhAdm(string usuario, string senha);
        Task<int> ObterIdDoParticipante(Participante participante);
    }
}
