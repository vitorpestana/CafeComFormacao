namespace CafeComFormacao.Models
{
    public class ViewsModels
    {
        public IEnumerable<Evento> Eventos { get; set; }
        public IEnumerable<Participante> Participantes { get; set; }
        public List<List<Participante>> ParticipantesPorEvento { get; set; }
        public IEnumerable<UsuarioEvento> UsuarioEventos { get; set;}
    }
}
