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
    public class RespuestasController : Controller
    {
        private readonly ForoContext _context;
        private readonly UserManager<Usuario> _userManager;

        public RespuestasController(ForoContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Respuestas
        public async Task<IActionResult> Index()
        {
            var foroContext = _context.Respuestas
                .Include(respuesta => respuesta.Miembro)
                .Include(respuesta => respuesta.Pregunta);
            return View(await foroContext.ToListAsync());
        }

        // GET: Respuestas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuesta = await _context.Respuestas
                .Include(respuesta => respuesta.Miembro)
                .Include(respuesta => respuesta.Pregunta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (respuesta == null)
            {
                return NotFound();
            }

            return View(respuesta);
        }

        [Authorize(Roles = "UsuarioStandard")]
        // GET: Respuestas/Create
        public IActionResult Create(int PreguntaId)
        {
            /*ViewData["Pregunta"] = _context.Preguntas.FirstOrDefault(p => p.Id == PreguntaId);
            return View();*/

            Pregunta pregunta = _context.Preguntas.FirstOrDefault(p => p.Id == PreguntaId);
            int userId = Int32.Parse(_userManager.GetUserId(User));
            if (pregunta.Activa && pregunta.MiembroId != userId)
            {
                ViewData["Pregunta"] = pregunta;
                return View();
            }
            return RedirectToAction("Details", "Preguntas", new { id = PreguntaId });

        }

        // POST: Respuestas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UsuarioStandard")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,PreguntaId")] Respuesta respuesta)
        {
            respuesta.MiembroId = Int32.Parse(_userManager.GetUserId(User));
            var pregunta = await _context.Preguntas.FirstOrDefaultAsync(p => p.Id == respuesta.PreguntaId);
            if (ModelState.IsValid && respuesta.MiembroId != pregunta.MiembroId)
            {
                _context.Add(respuesta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details","Preguntas",new { id = respuesta.PreguntaId});
            }
            return View(respuesta);
        }

        // GET: Respuestas/Edit/5
        [Authorize(Roles = "UsuarioStandard")]
        public async Task<IActionResult> Edit(int? id)
        {
            int userId = Int32.Parse(_userManager.GetUserId(User));
            if (id == null)
            {
                return NotFound();
            }

            var respuesta = await _context.Respuestas.FindAsync(id);
            if (respuesta == null)
            {
                return NotFound();
            }
            if (respuesta.MiembroId != userId)
            {
                return RedirectToAction("Details", "Preguntas", new { id = respuesta.PreguntaId });
            }
            return View(respuesta);
        }

        // POST: Respuestas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "UsuarioStandard")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] Respuesta respuesta)
        {

            if (id != respuesta.Id)
            {
                return NotFound();
            }

            Respuesta respuestaEnBd = _context.Respuestas.Find(respuesta.Id);

            if (ModelState.IsValid)
            {

                try
                {
                    
                    respuestaEnBd.Descripcion = respuesta.Descripcion;
                    _context.Update(respuestaEnBd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RespuestaExists(respuesta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Preguntas", new { id = respuestaEnBd.PreguntaId });
            }
            ViewData["UsuarioId"] = new SelectList(_context.Miembros, "Id", "Apellido", respuesta.MiembroId);
            ViewData["PreguntaId"] = new SelectList(_context.Set<Pregunta>(), "Id", "Descripcion", respuesta.PreguntaId);
            return View(respuesta);
        }

        // GET: Respuestas/Delete/5
        [Authorize(Roles = "UsuarioStandard")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var respuesta = await _context.Respuestas
                .Include(respuesta => respuesta.Miembro)
                .Include(respuesta => respuesta.Pregunta)
                .FirstOrDefaultAsync(respuesta => respuesta.Id == id);
            if (respuesta == null)
            {
                return NotFound();
            }

            return View(respuesta);
        }

        // POST: Respuestas/Delete/5
        [Authorize(Roles = "UsuarioStandard")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var respuesta = await _context.Respuestas.FindAsync(id);
            _context.Respuestas.Remove(respuesta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RespuestaExists(int id)
        {
            return _context.Respuestas.Any(e => e.Id == id);
        }


    }
}
