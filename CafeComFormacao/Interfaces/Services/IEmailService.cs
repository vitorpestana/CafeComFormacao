namespace CafeComFormacao.Interfaces.Services
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string emailDeDestino, string assunto, string corpoDoEmailComHtmlOuNao);
        string GerarCorpoDoEmail(string idParticipante);
    }
}
