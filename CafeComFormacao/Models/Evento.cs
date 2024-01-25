namespace CafeComFormacao.Models
{
    public class Evento : IEquatable<Evento>
    {
        [Key]
        public int Id { get; set; }
        public string NomeDoEvento { get; set; }
        public DateTime DataDoEvento { get; set; }
        public string HoraDoEvento { get; set; }
        public double ValorDoEvento { get; set; }
        public  bool Equals(Evento other)
        {
            if (other == null)
                return false;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Evento);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
 }

   

