namespace CafeComFormacao.Models
{
    [NotMapped]
    public class Cadastro
    {
        [Key]
        public int ParticipanteId { get; set; }

        [Required(ErrorMessage = "O seu nome é neessário. Preencha-o para prosseguir.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é um dado necessário. Preencha-o para prosseguir.")]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "O número de celular é dado necessário. Preencha-o para prosseguir.")]
        [StringLength(maximumLength: 20, MinimumLength = 8)]
        public string Celular { get; set; }

        [Required(ErrorMessage = "É necessário cadastrar uma senha. Cadestre-a para prosseguir.")]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "É necessário confirmar sua senha. Confirme-a para prosseguir.")]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "É necessário informar seu interesse em obter algum curso do Lidere.")]
        public bool CursoLidere { get; set; }
    }
}
