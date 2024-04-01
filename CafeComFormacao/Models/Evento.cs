using System.ComponentModel;

namespace CafeComFormacao.Models
{
    public class Evento : IEquatable<Evento>
    {
        [Key]
        public int Id { get; set; }
        
        [DisplayName("Nome")]
        public string NomeDoEvento { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.Date)]
        public DateTime DataDoEvento { get; set; }

        [DisplayName("Hora")]
        [RegularExpression(@"^[0-9][0-9]:[0-9][0-9]$", ErrorMessage = "Formato incorreto! Formato correto, por exemplo : 14:30")]
        public string HoraDoEvento { get; set; }

        [DisplayName("Valor unitário")]
        public double ValorDoEvento { get; set; }
        public bool Equals(Evento other)
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

   

