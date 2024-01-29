using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CafeComFormacao.Models;

namespace CafeComFormacao.Data
{
    public class CafeComFormacaoContext : DbContext
    {
        public CafeComFormacaoContext (DbContextOptions<CafeComFormacaoContext> options)
            : base(options)
        {
        }

        public DbSet<CafeComFormacao.Models.Participante> Participante { get; set; } = default!;
        public DbSet<CafeComFormacao.Models.Evento> Evento { get; set; } = default!;
        public DbSet<CafeComFormacao.Models.UsuarioEvento> UsuarioEvento { get; set; } = default!;
        public DbSet<CafeComFormacao.Models.CredenciaisParticipante> CredenciaisParticipante { get; set; } = default!;
        public DbSet<CafeComFormacao.Models.CredenciaisAdm> CredenciaisAdm { get; set; } = default!;
    }
}
