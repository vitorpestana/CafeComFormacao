namespace CafeComFormacao.Models
{
    public class Evento
    {
        [Key]
        public int Id { get; set; }
        public string NomeDoEvento { get; set; }
        public DateTime DataDoEvento { get; set; }
        public DateTime HoraDoEvento { get; set; }
        public double ValorDoEvento { get; set; }
    }
}
