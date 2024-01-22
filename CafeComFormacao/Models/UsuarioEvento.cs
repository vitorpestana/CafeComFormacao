namespace CafeComFormacao.Models
{
    public class UsuarioEvento
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Participante")]
        public int ParticipanteId { get; set; }
        public Participante Participante { get; set; }

        [ForeignKey("Evento")]
        public int EventoId { get; set; }
        public Evento Evento { get; set; }

        public UsuarioEvento(int participanteId, int eventoId) 
        {
            EventoId = eventoId;
            ParticipanteId = participanteId;
        }
    }
}
