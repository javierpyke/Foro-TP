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
using Microsoft.Data.SqlClient;

namespace FORO_C.Controllers
{
    [Authorize]
    public class EntradasController : Controller
    {
        private readonly ForoContext _context;
        private readonly UserManager<Usuario> _userManager;

        public EntradasController(ForoContext context,UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult EntradaDisponible(string titulo)
        {
            var existeEntrada = _context.Entradas.Any(entrada => entrada.Titulo == titulo);

            if (existeEntrada)
            {
                return Json($"La categoria {titulo} ya esta creada.");
            }
            else
            {
                return Json(true);
            }

        }

        // GET: Entradas
        public async Task<IActionResult> Index(int idCategoria)
        {
                     
            var foroContext = _context.Entradas.Include(entrada => entrada.Categoria).Where(entrada => entrada.CategoriaId == idCategoria);
            return View(await foroContext.ToListAsync());
        }

        // GET: Entradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas
                .Include(e => e.Categoria)
                .Include(e => e.Miembro)
                .Include(e => e.MiembrosHabilitados)
                .Include(e => e.Preguntas)
                .ThenInclude(p => p.Respuestas)
                .FirstOrDefaultAsync(miembro => miembro.Id == id);

            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        public async Task<IActionResult> Ver(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas
                .Include(e => e.Categoria)
                .Include(e => e.Miembro)
                .Include(e => e.MiembrosHabilitados)
                .ThenInclude(mh => mh.Miembro)
                .Include(e => e.Preguntas)
                .ThenInclude(p => p.Respuestas)
                .FirstOrDefaultAsync(miembro => miembro.Id == id);


            var userId = Int32.Parse(_userManager.GetUserId(User));

            var user = _context.Usuarios.FirstOrDefault(u => u.Id == Int32.Parse(_userManager.GetUserId(User)));

            var habilitado = entrada.MiembroEstaHabilitado(userId);

            if (entrada == null)
            {
                return NotFound();
            }

            if ((entrada.Privada) && (habilitado != "Habilitado"))
            {
                return RedirectToAction("Ver","Categorias",new { id = entrada.CategoriaId});
            }

            

            return View(entrada);
        }

        // GET: Entradas/Create
        [Authorize(Roles= "UsuarioStandard")]
        public IActionResult Create(int CategoriaId)
        {
            ViewData["Categoria"] = _context.Categorias.FirstOrDefault(c => c.Id == CategoriaId);
            return View();
        }

        [Authorize(Roles = "UsuarioStandard")]
        public IActionResult CrearEntrada()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UsuarioStandard")]
        public async Task<IActionResult> CrearEntrada([Bind("Id,Titulo,CategoriaId,Privada")] Entrada entrada)
        {

            entrada.MiembroId = Int32.Parse(_userManager.GetUserId(User));
            var existeEntEnContexto = _context.Entradas.FirstOrDefault(ent => ent.Titulo.ToUpper() == entrada.Titulo.ToUpper());

            if (ModelState.IsValid && existeEntEnContexto == null)
            {
                _context.Add(entrada);
                await _context.SaveChangesAsync();
                RedirectToAction();
                return RedirectToAction("Create", "Preguntas", new { EntradaId = entrada.Id });
            }

            return View(entrada);
        }

        // POST: Entradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UsuarioStandard")]
        public async Task<IActionResult> Create([Bind("Id,Titulo,CategoriaId,Privada")] Entrada entrada)
        {
        
            entrada.MiembroId = Int32.Parse(_userManager.GetUserId(User));
            var existeEntEnContexto = _context.Entradas.FirstOrDefault(ent => ent.Titulo.ToUpper() == entrada.Titulo.ToUpper());

            if (ModelState.IsValid && existeEntEnContexto == null)
            {
                _context.Add(entrada);
                await _context.SaveChangesAsync();
                RedirectToAction();
                return RedirectToAction("Create","Preguntas",new { EntradaId = entrada.Id});
            }
   
            return View(entrada);
        }

        // GET: Entradas/Edit/5
        [Authorize(Roles = "UsuarioStandard")]
        public async Task<IActionResult> Edit(int? id)
        {
            int userId = Int32.Parse(_userManager.GetUserId(User));
            if (id == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas.FindAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }

            if (entrada.MiembroId != userId)
            {
                return RedirectToAction("Ver", "Categorias", new { id = entrada.CategoriaId });
            }

            
            return View(entrada);
        }

        // POST: Entradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "UsuarioStandard")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Privada")] Entrada entrada)
        {
            if (id != entrada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Entrada entradaEnBd = _context.Entradas.Find(entrada.Id);
                    entradaEnBd.Titulo = entrada.Titulo;
                    entradaEnBd.Privada = entrada.Privada;
                    _context.Update(entradaEnBd);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntradaExists(entrada.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Ver", "Entradas", new { id = entrada.Id });
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", entrada.CategoriaId);
            return View(entrada);
        }

        // GET: Entradas/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas
                .Include(entrada => entrada.Categoria)
                .FirstOrDefaultAsync(miembro => miembro.Id == id);
            if (entrada == null)
            {
                return NotFound();
            }

            return View(entrada);
        }

        // POST: Entradas/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          
            Entrada entrada = _context.Entradas.Find(id);
            var categoriaId = entrada.CategoriaId;
            
            if(entrada != null)
            {
                try
                {
                    _context.Entradas.Remove(entrada);
                    _context.SaveChanges();
                }
                catch(Exception e)
                {
                    SqlException ie =  e.InnerException as SqlException;

                    if (ie.Number == 547)
                    {

                        entrada = _context.Entradas.Include(e => e.Preguntas).ThenInclude(p => p.Respuestas).ThenInclude(r => r.Reacciones).First(entrada => entrada.Id == id);

                        foreach (Pregunta p in entrada.Preguntas)
                        {
                            //foreach (Respuesta r in p.Respuestas)
                            //{
                            //    _context.Reacciones.RemoveRange(r.Reacciones);
                            //    _context.SaveChanges();
                            //}
                            //_context.Remove(p);
                            //_context.SaveChanges();
                        }
                        //_context.Preguntas.RemoveRange(entrada.Preguntas);
                        //_context.SaveChanges();
                        _context.Remove(entrada);
                        _context.SaveChanges();
                    }
                    else
                    {
                        return Content(e.Message);
                    }                    
                }
                
                return RedirectToAction("Ver","Categorias", new { id = categoriaId });
            }
            else
            {
                return NotFound();
            }
            
            return View(entrada);
        }

        private bool EntradaExists(int id)
        {
            return _context.Entradas.Any(entrada => entrada.Id == id);
        }


        public async Task<IActionResult> AgregarMiembroParaHabilitar(int CategoriaId,int EntradaId)
        {
            int userId = Int32.Parse(_userManager.GetUserId(User));

            MiembrosEntradas miembroEntrada = new MiembrosEntradas { EntradaId = EntradaId, MiembroId = userId };

            _context.Add(miembroEntrada);
            await _context.SaveChangesAsync();
            return RedirectToAction("Ver","Categorias",new { id = CategoriaId });
        }

        public async Task<IActionResult> HabilitarUsuario(int EntradaId, int MiembroId)
        {

            MiembrosEntradas miembroEntrada = await _context.MiembrosEntradas.FirstOrDefaultAsync(miembroEntrada => miembroEntrada.EntradaId == EntradaId && miembroEntrada.MiembroId == MiembroId);
            miembroEntrada.Habilitado = true;
            _context.Update(miembroEntrada);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Entradas", new { id = EntradaId });
        }
    }
}
