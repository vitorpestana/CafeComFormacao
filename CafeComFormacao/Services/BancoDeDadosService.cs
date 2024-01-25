using CafeComFormacao.Controllers;
using CafeComFormacao.Data;
using CafeComFormacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeComFormacao.Services
{
    public class BancoDeDadosService
    {
        private readonly CafeComFormacaoContext _context;

        public BancoDeDadosService(CafeComFormacaoContext context)
        {
            _context = context;
        }

        public async Task<int> Inserir(Participante participante)
        {
            _context.Add(participante);
            _context.SaveChanges();

            return await _context.Participante.Where(y => y.Nome.Trim() == participante.Nome.Trim() && y.Senha.Trim() == participante.Senha.Trim()).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<List<Participante>> ListarParticipantes()
        {
            return await _context.Participante.ToListAsync();
        }

        public async Task<List<Evento>> ListarEventos()
        {
            return await _context.Evento.ToListAsync();
        }

        public void AdicionarEventoAoUsuario(List<int> eventosSelecionados, int participanteId)
        {
            foreach (int idEvento in eventosSelecionados)
            {
                _context.UsuarioEvento.Add(new UsuarioEvento(participanteId, idEvento));
                _context.SaveChanges();
            }
        }

        public void CriarNovoEvento(Evento evento)
        {
            _context.Evento.Add(evento);
            _context.SaveChanges();
        }

        public async Task<List<UsuarioEvento>> ListarTodosOsUsuariosPorEvento()
        {
            return await _context.UsuarioEvento.ToListAsync();
        }


        public async Task<Dictionary<Evento, List<Participante>>> TodosOsUsuariosPorEvento(List<int> eventosIds)
        {
            Dictionary<Evento, List<Participante>> usuariosPorEvento = new();

            foreach (int idEvento in eventosIds)
            {
                usuariosPorEvento.Add(await EventoDaListaDeParticipantes(idEvento), await ListarUsuariosPorEvento(idEvento));
            }

            List<Evento> listaARemover = usuariosPorEvento.Where(listaParticipante => listaParticipante.Value.Count == 0)
                                                           .Select(parChaveValor => parChaveValor.Key)
                                                           .ToList();
            foreach (Evento chave in listaARemover)
            {
                usuariosPorEvento.Remove(chave);
            };

            return usuariosPorEvento;
        }

        public async Task<List<Participante>> ListarUsuariosPorEvento(int eventoId)
        {

            return await (from Participante participante in _context.Participante
                    join UsuarioEvento usuarioEvento in _context.UsuarioEvento on participante.Id equals usuarioEvento.ParticipanteId
                    join Evento evento in _context.Evento on usuarioEvento.EventoId equals evento.Id
                    where usuarioEvento.EventoId == eventoId
                    select participante).ToListAsync();

        }

        public async Task<ViewsModels> PrepararTudoViewsModels()
        {
            return new ViewsModels()
            {
                ParticipantesPorEvento = await TodosOsUsuariosPorEvento(await ObterIdsDosEventos()),
                Participantes = await ListarParticipantes(),
                Eventos = await ListarEventos(),
                UsuarioEventos = await ListarTodosOsUsuariosPorEvento()
            };
        }

        public async Task<ViewsModels> PrepararParticipantesPorEventoViewsModels()
        {
            return new ViewsModels()
            {
                ParticipantesPorEvento = await TodosOsUsuariosPorEvento(await ObterIdsDosEventos()),
            };
        }

        public async Task<List<Evento>>  ListarEventosDoUsuario(int usuarioId)
        {

            return await (from Evento evento in _context.Evento
                          join UsuarioEvento usuarioEvento in _context.UsuarioEvento on evento.Id equals usuarioEvento.EventoId
                          where usuarioEvento.ParticipanteId == usuarioId
                          select evento).ToListAsync();
        }

        public async Task<Evento> EventoDaListaDeParticipantes(int idEvento)
        {
            return  await (from Evento evento in _context.Evento
                         where evento.Id == idEvento
                          select evento).FirstOrDefaultAsync();
        }

        public async Task<List<int>> ObterIdsDosEventos()
        {
            return await (from Evento evento in _context.Evento
                    select evento.Id).ToListAsync();
        }
    }
}
