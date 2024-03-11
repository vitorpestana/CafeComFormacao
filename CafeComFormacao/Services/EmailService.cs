using CafeComFormacao.Interfaces.Services;
using CafeComFormacao.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using CafeComFormacao.Interfaces.Util;
using CafeComFormacao.Interfaces.Repositories;


namespace CafeComFormacao.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHashService _hashService;
        private readonly IParticipanteRepository _participanteRepository;

        public EmailService(IOptions<EmailSettings> emailSettings, IHashService hashService, IParticipanteRepository participanteRepository)
        {
            _emailSettings = emailSettings.Value;
            _hashService = hashService;
            _participanteRepository = participanteRepository;
        }

        public async Task EnviarEmailAsync(string emailDeDestino, string assunto, string corpoDoEmailComHtmlOuNao)
        {
            MimeMessage emailQueVaiSerEnviado = new();

            emailQueVaiSerEnviado.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
            emailQueVaiSerEnviado.To.Add(new MailboxAddress("Café com Formação", emailDeDestino));
            emailQueVaiSerEnviado.Subject = assunto;

            BodyBuilder mensagemComLink = new();

            mensagemComLink.HtmlBody = corpoDoEmailComHtmlOuNao;
            emailQueVaiSerEnviado.Body = mensagemComLink.ToMessageBody();

            using (SmtpClient cliente = new())
            {
                await cliente.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
                await cliente.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await cliente.SendAsync(emailQueVaiSerEnviado);
                await cliente.DisconnectAsync(true);
            }
        }

        public string GerarCorpoDoEmail(string idParticipante)
        {
            string verificação = _hashService.GerarHashSHA256(idParticipante);

            _participanteRepository.InserirCodigoDeVerificacao(verificação);

            return $"<p>Olá!Agradecemos o seu interesse em fazer parte do nosso café!<br>Agora falta pouco!Copie o código abaixo e cole no lugar designado neste <a href='https://www.google.com.br/'>link</a> para validar o seu cadastro e tomar um café com muita formação conosco!<br><br><br>{verificação}</p>";
        }
    }
}
