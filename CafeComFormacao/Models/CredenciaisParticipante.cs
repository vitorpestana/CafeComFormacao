using CafeComFormacao.Interfaces.Services;

namespace CafeComFormacao.Models
{
    public class CredenciaisParticipante
    {
        [Key, ForeignKey("Cadastro")]
        public int Id { get; set; }
        public string LoginEmail { get; set; }
        public string Senha { get; set; }
    }
}
