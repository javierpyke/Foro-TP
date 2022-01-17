using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FORO_C.Data;
using FORO_C.Models;
using Microsoft.AspNetCore.Authorization;

namespace FORO_C.Controllers
{
    [Authorize]
    public class MiembrosEntradasController : Controller
    {
        private readonly ForoContext _context;

        public MiembrosEntradasController(ForoContext context)
        {
            _context = context;
        }

        // GET: MiembrosEntradas
        public async Task<IActionResult> Index()
        {
            var foroContext = _context.MiembrosEntradas
                .Include(miembro => miembro.Entrada)
                .Include(miembro => miembro.Miembro);
            return View(await foroContext.ToListAsync());
        }

        // GET: MiembrosEntradas/Details/5
        public async Task<IActionResult> Details(int? mId, int? eId)
        {
            if (mId == null || eId == null)
            {
                return NotFound();
            }

            var miembrosEntradas = await _context.MiembrosEntradas
                .Include(miembro => miembro.Entrada)
                .Include(miembro => miembro.Miembro)
                .FirstOrDefaultAsync(miembro => miembro.MiembroId == mId);
            if (miembrosEntradas == null)
            {
                return NotFound();
            }

            return View(miembrosEntradas);
        }

        // GET: MiembrosEntradas/Create
        public IActionResult Create()
        {
            ViewData["EntradaId"] = new SelectList(_context.Entradas, "Id", "Titulo");
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "Id", "Apellido");
            return View();
        }

        // POST: MiembrosEntradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MiembroId,EntradaId,Habilitado")] MiembrosEntradas miembrosEntradas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miembrosEntradas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntradaId"] = new SelectList(_context.Entradas, "Id", "Titulo", miembrosEntradas.EntradaId);
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "Id", "Apellido", miembrosEntradas.MiembroId);
            return View(miembrosEntradas);
        }

        // GET: MiembrosEntradas/Edit/5
        public async Task<IActionResult> Edit(int? mId, int?eId)
        {
          
            var miembrosEntradas = await _context.MiembrosEntradas.FindAsync(mId, eId);
            if (miembrosEntradas == null)
            {
                return NotFound();
            }
            
            ViewData["EntradaId"] = new SelectList(_context.Entradas, "Id", "Titulo", miembrosEntradas.EntradaId);
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "Id", "Apellido", miembrosEntradas.MiembroId);
            return View(miembrosEntradas);
        }

        // POST: MiembrosEntradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? mId, int? eId, [Bind("MiembroId,EntradaId,Habilitado")] MiembrosEntradas miembrosEntradas)
        {
            if (mId != miembrosEntradas.MiembroId || eId != miembrosEntradas.EntradaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miembrosEntradas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiembrosEntradasExists(miembrosEntradas.MiembroId))
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
            ViewData["EntradaId"] = new SelectList(_context.Entradas, "Id", "Titulo", miembrosEntradas.EntradaId);
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "Id", "Apellido", miembrosEntradas.MiembroId);
            return View(miembrosEntradas);
        }

        // GET: MiembrosEntradas/Delete/5
        public async Task<IActionResult> Delete(int? mId, int? eId)
        {
            if (mId == null || eId == null)
            {
                return NotFound();
            }

            var miembrosEntradas = await _context.MiembrosEntradas
                .Include(miembro => miembro.Entrada)
                .Include(miembro => miembro.Miembro)
                .FirstOrDefaultAsync(miembro => miembro.MiembroId == mId);
            if (miembrosEntradas == null)
            {
                return NotFound();
            }

            return View(miembrosEntradas);
        }

        // POST: MiembrosEntradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var miembrosEntradas = await _context.MiembrosEntradas.FindAsync(id);
            _context.MiembrosEntradas.Remove(miembrosEntradas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiembrosEntradasExists(int id)
        {
            return _context.MiembrosEntradas.Any(miembro => miembro.MiembroId == id);
        }
    }
}
