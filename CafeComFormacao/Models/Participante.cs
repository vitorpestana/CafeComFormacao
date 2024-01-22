
using CafeComFormacao.Services;

namespace CafeComFormacao.Models
 {
        public class Participante
        {
        private readonly BancoDeDadosService _bancoService;

            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "O nome é um dado necessário. Preencha-o para prosseguir.")]
            [StringLength(maximumLength: 50, MinimumLength = 2)]
            public string Nome { get; set; }

            [Required(ErrorMessage = "O email é um dado necessário. Preencha-o para prosseguir.")]
            [StringLength(maximumLength: 50, MinimumLength = 2)]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "O número de celular é dado necessário. Preencha-o para prosseguir.")]
            [StringLength(maximumLength: 20, MinimumLength = 8)]
            public string Celular { get; set; }
            
            [Required(ErrorMessage = "É necessário cadastrar uma senha. Cadestre-a para prosseguir.")]
            public string Senha { get; set; }

            [Required(ErrorMessage = "É necessário informar seu interesse em obter algum curso do Lidere.")]
            public bool CursoLidere { get; set; }

            public bool StatusPagamento { get; set; } = false;

            public bool Admin { get; set; } = false;

            public Task<List<Participante>> Listar()
            {
                return _bancoService.ListarParticipantes();
            } 

        }
 }


