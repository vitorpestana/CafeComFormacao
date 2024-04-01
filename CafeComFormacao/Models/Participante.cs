
using System.ComponentModel;

namespace CafeComFormacao.Models
{
    public class Participante
    {
        [Key, ForeignKey("Cadastro")]
        public int Id { get; set; }
        public Cadastro Cadastro { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }

        [Display(Name = "Telefone de contato")]
        public string Celular { get; set; }

        [Display(Name = "Deseja obter o curso lidere")]
        public bool CursoLidere { get; set; }
    }
}


