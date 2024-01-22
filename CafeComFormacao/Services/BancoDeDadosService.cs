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


        public List<List<Participante>> TodosOsUsuariosPorEvento(List<int> eventosIds)
        {
            List<List<Participante>> usuariosPorEvento = new();

            foreach (int idEvento in eventosIds)
            {
                usuariosPorEvento.Add(ListarUsuariosPorEvento(idEvento));
            }

            return usuariosPorEvento;
        }
        public List<Participante> ListarUsuariosPorEvento(int eventoId)
        {

            return (from Participante participante in _context.Participante
                          join UsuarioEvento usuarioEvento in _context.UsuarioEvento on participante.Id equals usuarioEvento.ParticipanteId
                          join Evento evento in _context.Evento on usuarioEvento.EventoId equals evento.Id
                          where usuarioEvento.EventoId == eventoId
                          select participante).ToList();

        }
    }
}
