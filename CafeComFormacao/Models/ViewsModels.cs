namespace CafeComFormacao.Models
{
    [NotMapped]
    public class ViewsModels
    {
        public IEnumerable<Evento> Eventos { get; set; }
        public IEnumerable<Participante> Participantes { get; set; }
        public Dictionary<Evento, List<Participante>> ParticipantesPorEvento { get; set; }
        public IEnumerable<UsuarioEvento> UsuarioEventos { get; set;}
    }
}
