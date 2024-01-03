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
    }
}
