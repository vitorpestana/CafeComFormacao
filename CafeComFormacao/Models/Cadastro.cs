using System.ComponentModel;

namespace CafeComFormacao.Models
{
    [NotMapped]
    public class Cadastro
    {
        [Key]
        public int ParticipanteId { get; set; }

        [Required(ErrorMessage = "O seu nome é necessário. Preencha-o para prosseguir.")]
        [DisplayName("Nome completo")]
        [StringLength(maximumLength: 80, MinimumLength = 10, ErrorMessage = "O nome completo deve ter pelo menos 10 caracteres.")]
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s]+$", ErrorMessage = "O nome só pode contar letras. Retire qualquer número ou símbolo para prosseguir.")]
        public string Nome { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "O e-mail é um dado necessário. Preencha-o para prosseguir.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "O email não corresponde ao padrão usuario@servidor.dominio. Forneça um email válido para prosseguir.")]
        public string Email { get; set; }

        [DisplayName("Número para contato")]
        [Required(ErrorMessage = "O número para contato é um dado necessário. Preencha-o para prosseguir.")]
        [StringLength(maximumLength: 11, MinimumLength = 8, ErrorMessage = "O número de contato deve ter entre 8 e 11 caracteres.")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "É necessário cadastrar uma senha. Cadestre-a para prosseguir.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{8,50}$", ErrorMessage = "A senha deve conter pelo menos 8 caracteres, um número, um caractere não-alfanumérico (tal como como '@') e uma letra maiúscula e minúscula")]
        public string Senha { get; set; }

        [DisplayName("Confirme sua senha")]
        [Required(ErrorMessage = "É necessário confirmar sua senha. Confirme-a para prosseguir.")]
        [Compare("Senha", ErrorMessage = "A confirmação de senha não coincide com a senha que deseja-se cadastrar.")]
        public string ConfirmarSenha { get; set; }

        public bool CursoLidere { get; set; }
    }
}
