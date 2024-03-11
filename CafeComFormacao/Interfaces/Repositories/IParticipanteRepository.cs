using CafeComFormacao.Models;

namespace CafeComFormacao.Interfaces.Repositories
{
    public interface IParticipanteRepository
    {
        Task InserirParticipante(Participante participante);
        Task InserirCredenciais(CredenciaisParticipante participante);
        Task<List<Participante>> ListarParticipantes();
        Task<List<UsuarioEvento>> ListarRelacionamentoParticipanteEvento();
        Task<List<Participante>> ListarUsuariosPorEvento(int eventoId);
        Task<CredenciaisParticipante> VerificarCredenciais(string usuario);
        Task<CredenciaisAdm> VerificarSeEhAdm(string usuario);
        Task<int> ObterIdDoParticipante(Participante participante);
        Task<StatusPagamento> ObterStatusPagamento(int idEvento, int idParticipante);
        Task<bool> VerificarSeOEmailExiste(string email);
        Task<bool> VerificarSeOCelularExiste(string celular);
        Task InserirCodigoDeVerificacao(string codigoDeVerificacao);
        Task<string> ObterIdParticipante(string email);
        Task<bool> VerificarCodigoDeValidacao(string codigoDeVerificacao);
        Task DeletarCodigoDeVerificacao(string codigoDeVerificacao);
    }
}
