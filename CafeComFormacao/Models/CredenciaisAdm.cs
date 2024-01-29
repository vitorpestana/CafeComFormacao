namespace CafeComFormacao.Models
{
    public class CredenciaisAdm
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string LoginEmail { get; set; }
        public string Senha { get; set; }
    }
}
