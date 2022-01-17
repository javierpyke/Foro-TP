using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FORO_C.Data;
using FORO_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FORO_C.Controllers
{
    [Authorize]
    public class PreguntasController : Controller
    {
        private readonly ForoContext _context;
        private readonly UserManager<Usuario> _userManager;

        public PreguntasController(ForoContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Preguntas
        public async Task<IActionResult> Index(int idEntrada)
        {
            var foroContext = _context.Preguntas.Include(pregunta => pregunta.Entrada).Include(pregunta => pregunta.Miembro)
                .Where(pregunta => pregunta.EntradaId == idEntrada);
            return View(await _context.Preguntas.ToListAsync());
        }

        // GET: Preguntas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregunta = await _context.Preguntas
                .Include(pregunta => pregunta.Entrada)
                .Include(pregunta => pregunta.Miembro)
                .Include(pregunta => pregunta.Respuestas)
                .ThenInclude(respuesta => respuesta.Reacciones)
                .Include(pregunta => pregunta.Respuestas)
                .ThenInclude(respuesta => respuesta.Miembro)
                .FirstOrDefaultAsync(miembro => miembro.Id == id);



            var preguntaConMasMeGusta = pregunta.Respuestas.OrderByDescending(r => r.cantidadDeReacciones()[0]).Where(r => r.cantidadDeReacciones()[0] > 0).Take(1).ToList();
            int maximaCantiadaDeMeGusta = -1;

            if (preguntaConMasMeGusta.Count > 0)
            {
                maximaCantiadaDeMeGusta = pregunta.Respuestas.OrderByDescending(r => r.cantidadDeReacciones()[0]).Take(1).ToArray()[0].cantidadDeReacciones()[0];
            }

            var preguntaConMasNoMeGusta = pregunta.Respuestas.OrderByDescending(r => r.cantidadDeReacciones()[1]).Where(r => r.cantidadDeReacciones()[1] > 0).Take(1).ToList();
            int maximaCantiadaDeNoMeGusta = -1;

            if (preguntaConMasNoMeGusta.Count > 0)
            {
                maximaCantiadaDeNoMeGusta = pregunta.Respuestas.OrderByDescending(r => r.cantidadDeReacciones()[1]).Take(1).ToArray()[0].cantidadDeReacciones()[1];
            }


            ViewBag.RespuestaMasMeGusta = pregunta.Respuestas.Where(r => r.cantidadDeReacciones()[0] == maximaCantiadaDeMeGusta).ToList();
            ViewBag.RespuestaMasNoMeGusta = pregunta.Respuestas.Where(r => r.cantidadDeReacciones()[1] == maximaCantiadaDeNoMeGusta).ToList();

            if (pregunta == null)
            {
                return NotFound();
            }

            return View(pregunta);
        }

        // GET: Preguntas/Create
        public IActionResult Create(int EntradaId)
        {
            ViewData["Entrada"] = _context.Entradas.FirstOrDefault(e => e.Id == EntradaId);
            return View();
        }

        // POST: Preguntas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,EntradaId,Activa")] Pregunta pregunta)
        {
            pregunta.MiembroId = Int32.Parse(_userManager.GetUserId(User));

            if (ModelState.IsValid)
            {
                _context.Add(pregunta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Ver", "Entradas", new { id = pregunta.EntradaId });
            }

            return View(pregunta);
        }

        [Authorize(Roles = "UsuarioStandard")]
        // GET: Preguntas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            int userId = Int32.Parse(_userManager.GetUserId(User));
            if (id == null)
            {
                return NotFound();
            }

            var pregunta = await _context.Preguntas.FindAsync(id);
            if (pregunta == null)
            {
                return NotFound();
            }
            if (pregunta.MiembroId != userId)
            {
                return RedirectToAction("Ver", "Entradas", new { id = pregunta.EntradaId });
            }
            return View(pregunta);
        }

        // POST: Preguntas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Activa")] Pregunta pregunta)
        {
            if (id != pregunta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Pregunta preguntaEnBd = _context.Preguntas.Find(pregunta.Id);
                    preguntaEnBd.Descripcion = pregunta.Descripcion;
                    preguntaEnBd.Activa = pregunta.Activa;
                    _context.Update(preguntaEnBd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreguntaExists(pregunta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Preguntas", new { id = pregunta.Id });
            }
            ViewData["EntradaId"] = new SelectList(_context.Entradas, "Id", "Titulo", pregunta.EntradaId);
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "Id", "Apellido", pregunta.MiembroId);
            return View(pregunta);
        }

        // GET: Preguntas/Delete/5
        [Authorize(Roles = "Administrador")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pregunta = await _context.Preguntas
                .Include(pregunta => pregunta.Entrada)
                .Include(pregunta => pregunta.Miembro)
                .FirstOrDefaultAsync(miembro => miembro.Id == id);
            if (pregunta == null)
            {
                return NotFound();
            }

            return View(pregunta);
        }

        // POST: Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pregunta = await _context.Preguntas.FindAsync(id);
            _context.Preguntas.Remove(pregunta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PreguntaExists(int id)
        {
            return _context.Preguntas.Any(pregunta => pregunta.Id == id);
        }
    }
}
