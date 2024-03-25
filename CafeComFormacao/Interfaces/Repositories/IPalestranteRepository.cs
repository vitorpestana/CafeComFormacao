using CafeComFormacao.Models;
using Microsoft.AspNetCore.Mvc;

namespace CafeComFormacao.Interfaces.Repositories
{
    public interface IPalestranteRepository
    {
        Task InserirPalestrante(Palestrante palestrante);
        Task<List<Palestrante>> ListarPalestrantes();
        Task InserirPalestranteNoEvento(EventoPalestrante eventoPalestrante);
    }
}
