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

namespace FORO_C.Controllers
{
    public class ReaccionesController : Controller
    {
        private readonly ForoContext _context;
        private readonly UserManager<Usuario> _userManager;

        public ReaccionesController(ForoContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reacciones
        public async Task<IActionResult> Index()
        {
            var foroContext = _context.Reacciones
                .Include(reaccion => reaccion.Miembro)
                .Include(reaccion => reaccion.Respuesta);
            return View(await foroContext.ToListAsync());
        }

        // GET: Reacciones/Details/5
       /* public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaccion = await _context.Reacciones
                .Include(reaccion => reaccion.Miembro)
                .Include(reaccion => reaccion.Respuesta)
                .FirstOrDefaultAsync(miembro => miembro.Id == id);

            if (reaccion == null)
            {
                return NotFound();
            }

            return View(reaccion);
        }*/

        // GET: Reacciones/Create
        

        public async Task<IActionResult> AgregarReaccion(int meGusta, int respuestaId, int preguntaId)
        {
            var miembroId = Int32.Parse(_userManager.GetUserId(User));
            /* PREGUNTAR COMO HACER AND EN UN WHERE*/
            Reaccion reaccion = _context.Reacciones.FirstOrDefault(r => r.MiembroId == miembroId && r.RespuestaId == respuestaId);

            if (reaccion == null)
            {
                Reaccion reaccionNueva = new Reaccion()
                {
                    MeGusta = meGusta,
                    RespuestaId = respuestaId,
                    MiembroId = Int32.Parse(_userManager.GetUserId(User))
                };

                _context.Add(reaccionNueva);
                await _context.SaveChangesAsync();
            }         
            
            return RedirectToAction("Details", "Preguntas", new { id = preguntaId });
        }

        public async Task<IActionResult> QuitarReaccion(int respuestaId, int preguntaId)
        {
            var miembroId = Int32.Parse(_userManager.GetUserId(User));
            Reaccion reaccion = _context.Reacciones.FirstOrDefault(r => r.MiembroId == miembroId && r.RespuestaId == respuestaId);

            if (reaccion != null)
            {
                _context.Reacciones.Remove(reaccion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Preguntas", new { id = preguntaId });
        }

        // GET: Reacciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaccion = await _context.Reacciones.FindAsync(id);
            if (reaccion == null)
            {
                return NotFound();
            }
            ViewData["MiembroId"] = reaccion.MiembroId;
            ViewData["RespuestaId"] = reaccion.RespuestaId;
            return View(reaccion);
        }

        // POST: Reacciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MeGusta,RespuestaId,MiembroId,Fecha")] Reaccion reaccion)
        {
            if (id != reaccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reaccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReaccionExists(reaccion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "Id", "Apellido", reaccion.MiembroId);
            ViewData["RespuestaId"] = new SelectList(_context.Respuestas, "Id", "Descripcion", reaccion.RespuestaId);
            return View(reaccion);
        }*/

        // GET: Reacciones/Delete/5
       /* public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reaccion = await _context.Reacciones
                .Include(reaccion => reaccion.Miembro)
                .Include(reaccion => reaccion.Respuesta)
                .FirstOrDefaultAsync(miembro => miembro.Id == id);
            if (reaccion == null)
            {
                return NotFound();
            }

            return View(reaccion);
        }*/

        // POST: Reacciones/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reaccion = await _context.Reacciones.FindAsync(id);
            _context.Reacciones.Remove(reaccion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        /*private bool ReaccionExists(int id)
        {
            return _context.Reacciones.Any(reaccion => reaccion.Id == id);
        }*/
    }
}
