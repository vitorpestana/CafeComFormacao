namespace CafeComFormacao.Models
{
    [NotMapped]
    public class ViewsModels
    {
        public List<Evento> ListaDeEventos { get; set; }
        public List<Participante> ListaDeParticipantes { get; set; }
        public List<UsuarioEvento> ListaDeUsuarioEventos { get; set;}
        public Evento Evento { get; set; }
        public string StatusPagamento { get; set; }
    }
}
