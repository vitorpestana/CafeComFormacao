namespace CafeComFormacao.Models
{
    public class EventoPalestrante
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Palestrante")]
        public int PalestranteId { get; set; }
        public Palestrante Palestrante { get; set; }

        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        public EventoPalestrante(int palestranteId, int eventoId)
        {
            EventoId = eventoId;
            PalestranteId = palestranteId;
        }
    }
}
