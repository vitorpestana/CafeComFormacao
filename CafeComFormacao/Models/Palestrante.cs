namespace CafeComFormacao.Models
{
    public class Palestrante
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string TelefoneDeContato { get; set; }
        public string Email { get; set; }
    }
}
