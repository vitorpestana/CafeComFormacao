
namespace CafeComFormacao.Models
{
    public class Participante
    {
        [Key, ForeignKey("Cadastro")]
        public int Id { get; set; }
        public Cadastro Cadastro {  get; set; }
        
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public bool CursoLidere { get; set; }
    }
}


