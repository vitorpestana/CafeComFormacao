using CafeComFormacao.Models;
using CafeComFormacao.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeComFormacao.Controllers
{
    public class InscricaosController : Controller
    {
        private readonly BancoDeDadosService _bancoService;

        public InscricaosController(BancoDeDadosService bancoService)
        {
            _bancoService = bancoService;
        }


        // GET: InscricaoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: InscricaoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InscricaoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InscricaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(Participante participante)
        {
           _bancoService.Inserir(participante);

            return RedirectToAction(nameof(Index));
        }

        // GET: InscricaoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InscricaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InscricaoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InscricaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
