using FORO_C.Data;
using FORO_C.Models;
using FORO_C.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.Controllers
{
    public class RegistracionesController : Controller
    {

        private readonly ForoContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<Rol> _rolManager;

        public RegistracionesController(ForoContext context, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, RoleManager<Rol> rolManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._rolManager = rolManager;
        }

        [HttpGet]
        public IActionResult EmailDisponible(string email)
        {
            var existeEmail = _context.Usuarios.Any(user => user.Email == email);

            if (existeEmail)
            {
                return Json($"La direccion {email} ya existe en la base.");
            }
            else
            {
                return Json(true);
            }
        }
       
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registrar(Registracion viewModel)
        {
            if (ModelState.IsValid)
            {
                Miembro usuarioACrear = new Miembro();
                usuarioACrear.Email = viewModel.Email;
                usuarioACrear.UserName = viewModel.Email;
                usuarioACrear.PasswordHash = viewModel.Password;

                var resultadoCreacion = await _userManager.CreateAsync(usuarioACrear, viewModel.Password);

                if (resultadoCreacion.Succeeded)
                {
                    if (!await _rolManager.RoleExistsAsync("UsuarioStandard"))
                    {
                        await _rolManager.CreateAsync(new Rol("UsuarioStandard"));
                    }
                    var resultadoAsignacionRol = await _userManager.AddToRoleAsync(usuarioACrear, "UsuarioStandard");
                    if (resultadoAsignacionRol.Succeeded)
                    {
                        await _signInManager.SignInAsync(usuarioACrear, isPersistent: false);
                        return RedirectToAction("Edit", "Miembros", new { id= usuarioACrear.Id});
                    }
                    
                }

                foreach (var error in resultadoCreacion.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            return View(viewModel);
        }

        //[Authorize(Roles = "Administrador")]
        public IActionResult CrearAdmin()
        {
            return View();
        }

        //[Authorize(Roles ="Administrador")]
        [HttpPost]
        public async Task<ActionResult> CrearAdmin(Registracion viewModel)
        {
           
                Usuario adminACrear = new Usuario();
                adminACrear.Email = viewModel.Email;
                adminACrear.UserName = viewModel.Email;

                var resultadoCreacion = await _userManager.CreateAsync(adminACrear, "Password1!");

                if (resultadoCreacion.Succeeded)
                {
                    if (!await _rolManager.RoleExistsAsync("Administrador"))
                    {
                        await _rolManager.CreateAsync(new Rol("Administrador"));
                    }
                    var resultadoAsignacionRol = await _userManager.AddToRoleAsync(adminACrear, "Administrador");
                    if (resultadoAsignacionRol.Succeeded)
                    {
                      
                        return RedirectToAction("Index", "Home");
                    }

                }

                foreach (var error in resultadoCreacion.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            return RedirectToAction("Index", "Home");
        }

        private async Task CrearRolesBase()
        {
            if(!_context.Roles.Any())
            {
                if (!await _rolManager.RoleExistsAsync("Administrador"))
                {
                    await _rolManager.CreateAsync(new Rol("Administrador"));
                }

                if (!await _rolManager.RoleExistsAsync("UsuarioStandard"))
                {
                    await _rolManager.CreateAsync(new Rol("UsuarioStandard"));
                }
            }         
        }

        public IActionResult IniciarSesion(string returnurl)
        {
            TempData["returnUrl"] = returnurl;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> IniciarSesion(Login viewModel)
        {
            if (ModelState.IsValid)
            {
                var resultadoSignIn = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.Recordarme, false);

                if (resultadoSignIn.Succeeded)
                {
                    var returnUrl = TempData["returnUrl"] as string;
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index","Home");
                }

                ModelState.AddModelError(string.Empty, "Inicio de sesion invalido");
            }

                return View(viewModel);
        }

        public async Task<ActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "home");
        }


    }
}


