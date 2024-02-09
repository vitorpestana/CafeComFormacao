using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces.Repositories
{
    public interface IParticipanteRepository
    {
        Task InserirParticipante(Cadastro participante);
        Task InserirCredenciais(Cadastro participante);
        Task<List<Participante>> ListarParticipantes();
        Task<List<UsuarioEvento>> ListarRelacionamentoParticipanteEvento();
        Task<List<Participante>> ListarUsuariosPorEvento(int eventoId);
        Task<CredenciaisParticipante> VerificarCredenciais(string usuario, string senha);
        Task<CredenciaisAdm> VerificarSeEhAdm(string usuario, string senha);
        Task<int> ObterIdDoParticipante(Participante participante);
        Task<StatusPagamento> ObterStatusPagamento(int idEvento, int idParticipante);
        Task<bool> VerificarSeOEmailExiste(string email);
        Task<bool> VerificarSeOCelularExiste(string celular);
    }
}
