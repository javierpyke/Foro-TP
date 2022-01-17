using FORO_C.Data;
using FORO_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ForoContext _context;
        private readonly RoleManager<Rol> _roleManager;

        public HomeController(ILogger<HomeController> logger,ForoContext context,RoleManager<Rol> roleManager)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {

            ViewBag.existeAdmin = _roleManager.RoleExistsAsync("Administrador").Result;

            ViewBag.UltEntradasCargadas = await _context.Entradas.OrderByDescending(e => e.Fecha).Take(5).ToListAsync();

            var entradas = await _context.Entradas.Include(e => e.Preguntas).ThenInclude(p => p.Respuestas).ToListAsync();
            ViewBag.EntradasOrdenadas = entradas.OrderByDescending(e => e.CantidadDePreguntasYRespuestas).Take(5);

            var top3Miembros = _context.Miembros
                .Include(m => m.EntradasPropias)                
                .ToList();

            ViewBag.top3Miembros = top3Miembros.OrderByDescending(m => m.CantEntradasUltimoMes).Take(3).ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PrecargaExitosa()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
