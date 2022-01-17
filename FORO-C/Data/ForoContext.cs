using FORO_C.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FORO_C.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FORO_C.Data
{
    public class ForoContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public ForoContext(DbContextOptions options) : base(options)
        {

        }

        public static readonly ILoggerFactory MiLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseLoggerFactory(MiLoggerFactory);


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Se define las relaciones muchos a muchos (ver -> fluent API)
            modelBuilder.Entity<MiembrosEntradas>().HasKey(miembrosEntrada => new { miembrosEntrada.MiembroId, miembrosEntrada.EntradaId });

            modelBuilder.Entity<MiembrosEntradas>()
                .HasOne(miembrosEntrada => miembrosEntrada.Miembro)
                .WithMany(miembro => miembro.EntradasPermitidas)
                .HasForeignKey(miembrosEntrada => miembrosEntrada.MiembroId);

            modelBuilder.Entity<MiembrosEntradas>()
                .HasOne(miembrosEntrada => miembrosEntrada.Entrada)
                .WithMany(entrada => entrada.MiembrosHabilitados)
                .HasForeignKey(miembrosEntrada => miembrosEntrada.EntradaId);

            modelBuilder.Entity<Reaccion>().HasKey(reaccion => new { reaccion.MiembroId, reaccion.RespuestaId });

            //Definicion de nombre de las tablas
            modelBuilder.Entity<IdentityUser<int>>().ToTable("Usuarios");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UsuariosRoles");

            //Comportamientos para el manejo de entidades


        }

            public DbSet<Rol> Roles { get; set; }

            public DbSet<Usuario> Usuarios { get; set; }

            public DbSet<Miembro> Miembros { get; set; }

            public DbSet<Categoria> Categorias { get; set; }

            public DbSet<Entrada> Entradas { get; set; }

            public DbSet<Respuesta> Respuestas { get; set; }

            public DbSet<Reaccion> Reacciones { get; set; }

            public DbSet<Pregunta> Preguntas { get; set; }

            public DbSet<MiembrosEntradas> MiembrosEntradas { get; set; }

    }
}
