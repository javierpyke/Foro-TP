using FORO_C.Data;
using FORO_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.Controllers
{
    public class PrecargaController : Controller
    {
        private readonly ForoContext context;
        private readonly UserManager<Usuario> userManager;
        private readonly RoleManager<Rol> rolManager;

        public PrecargaController(ForoContext context, UserManager<Usuario> userManager, RoleManager<Rol> rolManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.rolManager = rolManager;
        }

        public async Task<IActionResult> Seed()
        {
            //CARGA DE MIEMBROS USUARIOS STANDARD//

            if (!rolManager.RoleExistsAsync("Administrador").Result)
            {
                string user = "miembro";
                string dominio = "@miembro.com";
                string password = "Password1!";

                Miembro usuario1 = new Miembro() { Nombre = "nombre1", Apellido = "apellido1", UserName = user + "1" + dominio, Email = user + "1" + dominio };
                Miembro usuario2 = new Miembro() { Nombre = "nombre2", Apellido = "apellido2", UserName = user + "2" + dominio, Email = user + "2" + dominio };
                Miembro usuario3 = new Miembro() { Nombre = "nombre3", Apellido = "apellido3", UserName = user + "3" + dominio, Email = user + "3" + dominio };
                Miembro usuario4 = new Miembro() { Nombre = "nombre4", Apellido = "apellido4", UserName = user + "4" + dominio, Email = user + "4" + dominio };

                Rol usuarioRol = new Rol("UsuarioStandard");

                await rolManager.CreateAsync(usuarioRol);

                var resultado1 = await userManager.CreateAsync(usuario1, password);
                var resultado2 = await userManager.CreateAsync(usuario2, password);
                var resultado3 = await userManager.CreateAsync(usuario3, password);
                var resultado4 = await userManager.CreateAsync(usuario4, password);

                await userManager.AddToRoleAsync(usuario1, "UsuarioStandard");
                await userManager.AddToRoleAsync(usuario2, "UsuarioStandard");
                await userManager.AddToRoleAsync(usuario3, "UsuarioStandard");
                await userManager.AddToRoleAsync(usuario4, "UsuarioStandard");


                Usuario admin = new Usuario() { Nombre = "admin1", Apellido = "admin1", UserName = "admin@admin.com", Email = "admin@admin.com" };
                Rol adminRol = new Rol("Administrador");
                await rolManager.CreateAsync(adminRol);

                var resultado5 = await userManager.CreateAsync(admin, password);

                await userManager.AddToRoleAsync(admin, "Administrador");

                Categoria categoria1 = new Categoria() { Nombre = "Carreras ORT" };
                Categoria categoria2 = new Categoria() { Nombre = "Asignaturas ORT" };
                Categoria categoria3 = new Categoria() { Nombre = "Finales ORT" };
                Categoria categoria4 = new Categoria() { Nombre = "Trabajos Practico ORT" };
                Categoria categoria5 = new Categoria() { Nombre = "Parciales ORT" };

                context.Add(categoria1); context.Add(categoria2);
                context.Add(categoria3); context.Add(categoria4);
                context.Add(categoria5);
                context.SaveChanges();

                Entrada entrada11 = new Entrada() { CategoriaId = categoria1.Id, MiembroId = usuario1.Id, Titulo = "Informacion sobre Analista de Sistemas" };
                Entrada entrada12 = new Entrada() { CategoriaId = categoria1.Id, MiembroId = usuario2.Id, Titulo = "Diseño industrial" };
                Entrada entrada13 = new Entrada() { CategoriaId = categoria1.Id, MiembroId = usuario3.Id, Titulo = "Bio Tecnologia" };
                Entrada entrada21 = new Entrada() { CategoriaId = categoria2.Id, MiembroId = usuario4.Id, Titulo = "Prog de Nuevas Tecnologias I" };
                Entrada entrada22 = new Entrada() { CategoriaId = categoria2.Id, MiembroId = usuario1.Id, Titulo = "Programacion" };
                Entrada entrada23 = new Entrada() { CategoriaId = categoria2.Id, MiembroId = usuario2.Id, Titulo = "Taller de Programacion" };
                Entrada entrada31 = new Entrada() { CategoriaId = categoria3.Id, MiembroId = usuario3.Id, Titulo = "Pedido de Finales de PR1" };
                Entrada entrada32 = new Entrada() { CategoriaId = categoria3.Id, MiembroId = usuario4.Id, Titulo = "Pedido de Finales de Taller" };
                Entrada entrada33 = new Entrada() { CategoriaId = categoria3.Id, MiembroId = usuario1.Id, Titulo = "Pedido de Finales de Matematicas" };
                Entrada entrada41 = new Entrada() { CategoriaId = categoria4.Id, MiembroId = usuario2.Id, Titulo = "TP de PNT1" };
                Entrada entrada42 = new Entrada() { CategoriaId = categoria4.Id, MiembroId = usuario3.Id, Titulo = "TP de Programacion" };
                Entrada entrada51 = new Entrada() { CategoriaId = categoria5.Id, MiembroId = usuario4.Id, Titulo = "Parcial de Taller" };
                context.Add(entrada11); context.Add(entrada12); context.Add(entrada13);
                context.Add(entrada21); context.Add(entrada22); context.Add(entrada23);
                context.Add(entrada31); context.Add(entrada32); context.Add(entrada33);
                context.Add(entrada41); context.Add(entrada42);
                context.Add(entrada51);
                context.SaveChanges();

                MiembrosEntradas miembrosEntradas1 = new MiembrosEntradas() { EntradaId = entrada11.Id, MiembroId = usuario1.Id, Habilitado = true };
                MiembrosEntradas miembrosEntradas2 = new MiembrosEntradas() { EntradaId = entrada12.Id, MiembroId = usuario2.Id, Habilitado = true };
                MiembrosEntradas miembrosEntradas3 = new MiembrosEntradas() { EntradaId = entrada13.Id, MiembroId = usuario3.Id, Habilitado = true };
                MiembrosEntradas miembrosEntradas4 = new MiembrosEntradas() { EntradaId = entrada21.Id, MiembroId = usuario4.Id, Habilitado = true };
                MiembrosEntradas miembrosEntradas5 = new MiembrosEntradas() { EntradaId = entrada22.Id, MiembroId = usuario1.Id, Habilitado = true };
                MiembrosEntradas miembrosEntradas6 = new MiembrosEntradas() { EntradaId = entrada23.Id, MiembroId = usuario2.Id, Habilitado = true };
                MiembrosEntradas miembrosEntradas7 = new MiembrosEntradas() { EntradaId = entrada31.Id, MiembroId = usuario3.Id, Habilitado = true };
                MiembrosEntradas miembrosEntradas8 = new MiembrosEntradas() { EntradaId = entrada32.Id, MiembroId = usuario4.Id, Habilitado = true };
                MiembrosEntradas miembrosEntradas9 = new MiembrosEntradas() { EntradaId = entrada33.Id, MiembroId = usuario1.Id, Habilitado = true };
                MiembrosEntradas miembrosEntradas10 = new MiembrosEntradas() { EntradaId = entrada41.Id, MiembroId = usuario2.Id, Habilitado = true };
                MiembrosEntradas miembrosEntradas11 = new MiembrosEntradas() { EntradaId = entrada42.Id, MiembroId = usuario3.Id, Habilitado = true };
                MiembrosEntradas miembrosEntradas12 = new MiembrosEntradas() { EntradaId = entrada51.Id, MiembroId = usuario4.Id, Habilitado = true };

                context.Add(miembrosEntradas1); context.Add(miembrosEntradas2); context.Add(miembrosEntradas3); context.Add(miembrosEntradas4);
                context.Add(miembrosEntradas5); context.Add(miembrosEntradas6); context.Add(miembrosEntradas7); context.Add(miembrosEntradas8);
                context.Add(miembrosEntradas9); context.Add(miembrosEntradas10); context.Add(miembrosEntradas11); context.Add(miembrosEntradas12);
                context.SaveChanges();

                Pregunta pregunta1 = new Pregunta() { EntradaId = entrada11.Id, MiembroId = usuario1.Id, Descripcion = "Pueden dar Feedback de la carrera?", Activa = true };
                Pregunta pregunta2 = new Pregunta() { EntradaId = entrada11.Id, MiembroId = usuario4.Id, Descripcion = "Que materias son promocionables?", Activa = true };
                Pregunta pregunta3 = new Pregunta() { EntradaId = entrada12.Id, MiembroId = usuario2.Id, Descripcion = "Pueden dar Feedback de la carrera?", Activa = true };
                Pregunta pregunta4 = new Pregunta() { EntradaId = entrada12.Id, MiembroId = usuario4.Id, Descripcion = "Que materias son promocionables?", Activa = true };
                Pregunta pregunta5 = new Pregunta() { EntradaId = entrada13.Id, MiembroId = usuario3.Id, Descripcion = "Pueden dar Feedback de la carrera?", Activa = true };
                Pregunta pregunta6 = new Pregunta() { EntradaId = entrada13.Id, MiembroId = usuario4.Id, Descripcion = "Que materias son promocionables?", Activa = true };
                Pregunta pregunta7 = new Pregunta() { EntradaId = entrada21.Id, MiembroId = usuario4.Id, Descripcion = "Pueden dar Feedback de la materia?", Activa = true };
                Pregunta pregunta8 = new Pregunta() { EntradaId = entrada21.Id, MiembroId = usuario1.Id, Descripcion = "Que profeseores recomiendan?", Activa = true };
                Pregunta pregunta9 = new Pregunta() { EntradaId = entrada22.Id, MiembroId = usuario1.Id, Descripcion = "Pueden dar Feedback de la materia?", Activa = true };
                Pregunta pregunta10 = new Pregunta() { EntradaId = entrada22.Id, MiembroId = usuario2.Id, Descripcion = "Que profeseores recomiendan?", Activa = true };
                Pregunta pregunta11 = new Pregunta() { EntradaId = entrada23.Id, MiembroId = usuario2.Id, Descripcion = "Pueden dar Feedback de la materia?", Activa = true };
                Pregunta pregunta12 = new Pregunta() { EntradaId = entrada23.Id, MiembroId = usuario3.Id, Descripcion = "Que profeseores recomiendan?", Activa = true };
                Pregunta pregunta13 = new Pregunta() { EntradaId = entrada31.Id, MiembroId = usuario3.Id, Descripcion = "Pueden dar Feedback de los finales?", Activa = true };
                Pregunta pregunta14 = new Pregunta() { EntradaId = entrada31.Id, MiembroId = usuario4.Id, Descripcion = "Quien tiene finales resueltos?", Activa = true };
                Pregunta pregunta15 = new Pregunta() { EntradaId = entrada32.Id, MiembroId = usuario4.Id, Descripcion = "Pueden dar Feedback de los finales?", Activa = true };
                Pregunta pregunta16 = new Pregunta() { EntradaId = entrada32.Id, MiembroId = usuario1.Id, Descripcion = "Quien tiene finales resueltos?", Activa = true };
                Pregunta pregunta17 = new Pregunta() { EntradaId = entrada33.Id, MiembroId = usuario1.Id, Descripcion = "Pueden dar Feedback de los finales?", Activa = true };
                Pregunta pregunta18 = new Pregunta() { EntradaId = entrada33.Id, MiembroId = usuario2.Id, Descripcion = "Quien tiene finales resueltos?", Activa = true };
                Pregunta pregunta19 = new Pregunta() { EntradaId = entrada41.Id, MiembroId = usuario2.Id, Descripcion = "Alguien puede compartir el TP?", Activa = true };
                Pregunta pregunta20 = new Pregunta() { EntradaId = entrada41.Id, MiembroId = usuario3.Id, Descripcion = "Quien tiene el enunciado?", Activa = true };
                Pregunta pregunta21 = new Pregunta() { EntradaId = entrada42.Id, MiembroId = usuario3.Id, Descripcion = "Alguien puede compartir el TP?", Activa = true };
                Pregunta pregunta22 = new Pregunta() { EntradaId = entrada42.Id, MiembroId = usuario4.Id, Descripcion = "Quien tiene el enunciado?", Activa = true };
                Pregunta pregunta23 = new Pregunta() { EntradaId = entrada51.Id, MiembroId = usuario4.Id, Descripcion = "Quien tiene parciales resueltos?", Activa = true };
                Pregunta pregunta24 = new Pregunta() { EntradaId = entrada51.Id, MiembroId = usuario1.Id, Descripcion = "En que fecha se recupera?", Activa = true };

                context.Add(pregunta1); context.Add(pregunta2); context.Add(pregunta3); context.Add(pregunta4); context.Add(pregunta5);
                context.Add(pregunta6); context.Add(pregunta7); context.Add(pregunta8); context.Add(pregunta9); context.Add(pregunta10);
                context.Add(pregunta11); context.Add(pregunta12); context.Add(pregunta13); context.Add(pregunta14); context.Add(pregunta15);
                context.Add(pregunta16); context.Add(pregunta17); context.Add(pregunta18); context.Add(pregunta19); context.Add(pregunta20);
                context.Add(pregunta21); context.Add(pregunta22); context.Add(pregunta23); context.Add(pregunta24);
                context.SaveChanges();

                Respuesta respuesta1 = new Respuesta() { PreguntaId = pregunta1.Id, MiembroId = usuario2.Id, Descripcion = "Respuesta1" };
                Respuesta respuesta2 = new Respuesta() { PreguntaId = pregunta2.Id, MiembroId = usuario2.Id, Descripcion = "Respuesta2" };
                Respuesta respuesta3 = new Respuesta() { PreguntaId = pregunta3.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta3" };
                Respuesta respuesta4 = new Respuesta() { PreguntaId = pregunta4.Id, MiembroId = usuario1.Id, Descripcion = "Respuesta4" };
                Respuesta respuesta5 = new Respuesta() { PreguntaId = pregunta5.Id, MiembroId = usuario1.Id, Descripcion = "Respuesta5" };
                Respuesta respuesta6 = new Respuesta() { PreguntaId = pregunta6.Id, MiembroId = usuario2.Id, Descripcion = "Respuesta6" };
                Respuesta respuesta7 = new Respuesta() { PreguntaId = pregunta7.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta7" };
                Respuesta respuesta8 = new Respuesta() { PreguntaId = pregunta8.Id, MiembroId = usuario4.Id, Descripcion = "Respuesta8" };
                Respuesta respuesta9 = new Respuesta() { PreguntaId = pregunta9.Id, MiembroId = usuario2.Id, Descripcion = "Respuesta9" };
                Respuesta respuesta10 = new Respuesta() { PreguntaId = pregunta10.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta10" };
                Respuesta respuesta11 = new Respuesta() { PreguntaId = pregunta11.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta11" };
                Respuesta respuesta12 = new Respuesta() { PreguntaId = pregunta12.Id, MiembroId = usuario4.Id, Descripcion = "Respuesta12" };
                Respuesta respuesta13 = new Respuesta() { PreguntaId = pregunta13.Id, MiembroId = usuario1.Id, Descripcion = "Respuesta13" };
                Respuesta respuesta14 = new Respuesta() { PreguntaId = pregunta14.Id, MiembroId = usuario2.Id, Descripcion = "Respuesta14" };
                Respuesta respuesta15 = new Respuesta() { PreguntaId = pregunta15.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta15" };
                Respuesta respuesta16 = new Respuesta() { PreguntaId = pregunta16.Id, MiembroId = usuario4.Id, Descripcion = "Respuesta16" };
                Respuesta respuesta17 = new Respuesta() { PreguntaId = pregunta17.Id, MiembroId = usuario2.Id, Descripcion = "Respuesta17" };
                Respuesta respuesta18 = new Respuesta() { PreguntaId = pregunta18.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta18" };
                Respuesta respuesta19 = new Respuesta() { PreguntaId = pregunta19.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta19" };
                Respuesta respuesta20 = new Respuesta() { PreguntaId = pregunta20.Id, MiembroId = usuario4.Id, Descripcion = "Respuesta20" };
                Respuesta respuesta21 = new Respuesta() { PreguntaId = pregunta21.Id, MiembroId = usuario1.Id, Descripcion = "Respuesta21" };
                Respuesta respuesta22 = new Respuesta() { PreguntaId = pregunta22.Id, MiembroId = usuario2.Id, Descripcion = "Respuesta22" };
                Respuesta respuesta23 = new Respuesta() { PreguntaId = pregunta23.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta23" };
                Respuesta respuesta24 = new Respuesta() { PreguntaId = pregunta24.Id, MiembroId = usuario4.Id, Descripcion = "Respuesta24" };
                Respuesta respuesta25 = new Respuesta() { PreguntaId = pregunta1.Id, MiembroId = usuario2.Id, Descripcion = "Respuesta25" };
                Respuesta respuesta26 = new Respuesta() { PreguntaId = pregunta3.Id, MiembroId = usuario1.Id, Descripcion = "Respuesta26" };
                Respuesta respuesta27 = new Respuesta() { PreguntaId = pregunta5.Id, MiembroId = usuario4.Id, Descripcion = "Respuesta27" };
                Respuesta respuesta28 = new Respuesta() { PreguntaId = pregunta7.Id, MiembroId = usuario2.Id, Descripcion = "Respuesta28" };
                Respuesta respuesta29 = new Respuesta() { PreguntaId = pregunta9.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta29" };
                Respuesta respuesta30 = new Respuesta() { PreguntaId = pregunta11.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta30" };
                Respuesta respuesta31 = new Respuesta() { PreguntaId = pregunta13.Id, MiembroId = usuario4.Id, Descripcion = "Respuesta31" };
                Respuesta respuesta32 = new Respuesta() { PreguntaId = pregunta15.Id, MiembroId = usuario1.Id, Descripcion = "Respuesta32" };
                Respuesta respuesta33 = new Respuesta() { PreguntaId = pregunta17.Id, MiembroId = usuario2.Id, Descripcion = "Respuesta33" };
                Respuesta respuesta34 = new Respuesta() { PreguntaId = pregunta19.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta34" };
                Respuesta respuesta35 = new Respuesta() { PreguntaId = pregunta21.Id, MiembroId = usuario4.Id, Descripcion = "Respuesta35" };
                Respuesta respuesta36 = new Respuesta() { PreguntaId = pregunta23.Id, MiembroId = usuario1.Id, Descripcion = "Respuesta36" };
                Respuesta respuesta37 = new Respuesta() { PreguntaId = pregunta1.Id, MiembroId = usuario2.Id, Descripcion = "Respuesta37" };
                Respuesta respuesta38 = new Respuesta() { PreguntaId = pregunta3.Id, MiembroId = usuario3.Id, Descripcion = "Respuesta38" };
                Respuesta respuesta39 = new Respuesta() { PreguntaId = pregunta5.Id, MiembroId = usuario4.Id, Descripcion = "Respuesta39" };
                Respuesta respuesta40 = new Respuesta() { PreguntaId = pregunta7.Id, MiembroId = usuario1.Id, Descripcion = "Respuesta40" };

                context.Add(respuesta1); context.Add(respuesta2); context.Add(respuesta3); context.Add(respuesta4); context.Add(respuesta5);
                context.Add(respuesta6); context.Add(respuesta7); context.Add(respuesta8); context.Add(respuesta9); context.Add(respuesta10);
                context.Add(respuesta11); context.Add(respuesta12); context.Add(respuesta13); context.Add(respuesta14); context.Add(respuesta15);
                context.Add(respuesta16); context.Add(respuesta17); context.Add(respuesta18); context.Add(respuesta19); context.Add(respuesta20);
                context.Add(respuesta21); context.Add(respuesta22); context.Add(respuesta23); context.Add(respuesta24); context.Add(respuesta25);
                context.Add(respuesta26); context.Add(respuesta27); context.Add(respuesta28); context.Add(respuesta29); context.Add(respuesta30);
                context.Add(respuesta31); context.Add(respuesta32); context.Add(respuesta33); context.Add(respuesta34); context.Add(respuesta35);
                context.Add(respuesta36); context.Add(respuesta37); context.Add(respuesta38); context.Add(respuesta39); context.Add(respuesta40);
                context.SaveChanges();

                Reaccion reaccion1 = new Reaccion() { RespuestaId = respuesta1.Id, MiembroId = usuario2.Id, MeGusta = 1 };
                Reaccion reaccion2 = new Reaccion() { RespuestaId = respuesta1.Id, MiembroId = usuario3.Id, MeGusta = 1 };
                Reaccion reaccion3 = new Reaccion() { RespuestaId = respuesta2.Id, MiembroId = usuario3.Id, MeGusta = 1 };
                Reaccion reaccion4 = new Reaccion() { RespuestaId = respuesta2.Id, MiembroId = usuario1.Id, MeGusta = -1 };
                Reaccion reaccion5 = new Reaccion() { RespuestaId = respuesta3.Id, MiembroId = usuario3.Id, MeGusta = -1 };
                Reaccion reaccion6 = new Reaccion() { RespuestaId = respuesta3.Id, MiembroId = usuario1.Id, MeGusta = 1 };
                Reaccion reaccion7 = new Reaccion() { RespuestaId = respuesta4.Id, MiembroId = usuario3.Id, MeGusta = 1 };
                Reaccion reaccion8 = new Reaccion() { RespuestaId = respuesta4.Id, MiembroId = usuario1.Id, MeGusta = 1 };
                Reaccion reaccion9 = new Reaccion() { RespuestaId = respuesta5.Id, MiembroId = usuario2.Id, MeGusta = 1 };
                Reaccion reaccion10 = new Reaccion() { RespuestaId = respuesta5.Id, MiembroId = usuario1.Id, MeGusta = -1 };
                Reaccion reaccion11 = new Reaccion() { RespuestaId = respuesta6.Id, MiembroId = usuario3.Id, MeGusta = -1 };
                Reaccion reaccion12 = new Reaccion() { RespuestaId = respuesta6.Id, MiembroId = usuario1.Id, MeGusta = -1 };
                Reaccion reaccion13 = new Reaccion() { RespuestaId = respuesta7.Id, MiembroId = usuario2.Id, MeGusta = 1 };
                Reaccion reaccion14 = new Reaccion() { RespuestaId = respuesta7.Id, MiembroId = usuario3.Id, MeGusta = 1 };
                Reaccion reaccion15 = new Reaccion() { RespuestaId = respuesta8.Id, MiembroId = usuario4.Id, MeGusta = 1 };
                Reaccion reaccion16 = new Reaccion() { RespuestaId = respuesta8.Id, MiembroId = usuario2.Id, MeGusta = 1 };
                Reaccion reaccion17 = new Reaccion() { RespuestaId = respuesta9.Id, MiembroId = usuario2.Id, MeGusta = -1 };
                Reaccion reaccion18 = new Reaccion() { RespuestaId = respuesta9.Id, MiembroId = usuario3.Id, MeGusta = 1 };
                Reaccion reaccion19 = new Reaccion() { RespuestaId = respuesta10.Id, MiembroId = usuario4.Id, MeGusta = 1 };
                Reaccion reaccion20 = new Reaccion() { RespuestaId = respuesta10.Id, MiembroId = usuario1.Id, MeGusta = -1 };
                Reaccion reaccion21 = new Reaccion() { RespuestaId = respuesta11.Id, MiembroId = usuario1.Id, MeGusta = 1 };
                Reaccion reaccion22 = new Reaccion() { RespuestaId = respuesta11.Id, MiembroId = usuario3.Id, MeGusta = 1 };
                Reaccion reaccion23 = new Reaccion() { RespuestaId = respuesta12.Id, MiembroId = usuario4.Id, MeGusta = 1 };
                Reaccion reaccion24 = new Reaccion() { RespuestaId = respuesta13.Id, MiembroId = usuario1.Id, MeGusta = -1 };
                Reaccion reaccion25 = new Reaccion() { RespuestaId = respuesta14.Id, MiembroId = usuario2.Id, MeGusta = 1 };
                Reaccion reaccion26 = new Reaccion() { RespuestaId = respuesta14.Id, MiembroId = usuario3.Id, MeGusta = 1 };
                Reaccion reaccion27 = new Reaccion() { RespuestaId = respuesta15.Id, MiembroId = usuario1.Id, MeGusta = 1 };
                Reaccion reaccion28 = new Reaccion() { RespuestaId = respuesta15.Id, MiembroId = usuario2.Id, MeGusta = -1 };
                Reaccion reaccion29 = new Reaccion() { RespuestaId = respuesta16.Id, MiembroId = usuario2.Id, MeGusta = 1 };
                Reaccion reaccion30 = new Reaccion() { RespuestaId = respuesta16.Id, MiembroId = usuario3.Id, MeGusta = 1 };
                Reaccion reaccion31 = new Reaccion() { RespuestaId = respuesta17.Id, MiembroId = usuario4.Id, MeGusta = 1 };
                Reaccion reaccion32 = new Reaccion() { RespuestaId = respuesta18.Id, MiembroId = usuario1.Id, MeGusta = 1 };
                Reaccion reaccion33 = new Reaccion() { RespuestaId = respuesta19.Id, MiembroId = usuario3.Id, MeGusta = -1 };
                Reaccion reaccion34 = new Reaccion() { RespuestaId = respuesta20.Id, MiembroId = usuario4.Id, MeGusta = 1 };
                Reaccion reaccion35 = new Reaccion() { RespuestaId = respuesta21.Id, MiembroId = usuario4.Id, MeGusta = 1 };
                Reaccion reaccion36 = new Reaccion() { RespuestaId = respuesta22.Id, MiembroId = usuario1.Id, MeGusta = -1 };
                Reaccion reaccion37 = new Reaccion() { RespuestaId = respuesta23.Id, MiembroId = usuario2.Id, MeGusta = -1 };
                Reaccion reaccion38 = new Reaccion() { RespuestaId = respuesta24.Id, MiembroId = usuario3.Id, MeGusta = -1 };
                Reaccion reaccion39 = new Reaccion() { RespuestaId = respuesta24.Id, MiembroId = usuario4.Id, MeGusta = -1 };
                Reaccion reaccion40 = new Reaccion() { RespuestaId = respuesta24.Id, MiembroId = usuario2.Id, MeGusta = -1 };

                context.Add(reaccion1); context.Add(reaccion2); context.Add(reaccion3); context.Add(reaccion4); context.Add(reaccion5);
                context.Add(reaccion6); context.Add(reaccion7); context.Add(reaccion8); context.Add(reaccion9); context.Add(reaccion10);
                context.Add(reaccion11); context.Add(reaccion12); context.Add(reaccion13); context.Add(reaccion14); context.Add(reaccion15);
                context.Add(reaccion16); context.Add(reaccion17); context.Add(reaccion18); context.Add(reaccion19); context.Add(reaccion20);
                context.Add(reaccion21); context.Add(reaccion22); context.Add(reaccion23); context.Add(reaccion24); context.Add(reaccion25);
                context.Add(reaccion26); context.Add(reaccion27); context.Add(reaccion28); context.Add(reaccion29); context.Add(reaccion30);
                context.Add(reaccion31); context.Add(reaccion32); context.Add(reaccion33); context.Add(reaccion34); context.Add(reaccion35);
                context.Add(reaccion36); context.Add(reaccion37); context.Add(reaccion38); context.Add(reaccion39); context.Add(reaccion40);
                context.SaveChanges();

                return RedirectToAction("PrecargaExitosa", "Home");

            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CrearAdmin()
        {

            string user = "admin";
            string dominio = "@admin.com";
            string password = "Password1!";

            Usuario admin1 = new Usuario() { Nombre = "nombre1", Apellido = "apellido1", UserName = user + dominio, Email = user + dominio };
            var resultado3 = await userManager.CreateAsync(admin1, password);

            Rol rolAdmin = new Rol("Administrador");

            await rolManager.CreateAsync(rolAdmin);
            await userManager.AddToRoleAsync(admin1, "Administrador");

            return RedirectToAction("Index", "Home");
        }
    }
}
