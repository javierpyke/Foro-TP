using FORO_C.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FORO_C.Models
{
    public class Entrada 
    {
        
        [Display(Name = Alias.EntradaId)]
        public int Id {get; set;}

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [StringLength(Restrictions.MaxTitleLengh, MinimumLength = Restrictions.MinTitleLengh, ErrorMessage = ErrMsgs.StrMaxAndMin )]
        [Display(Name = Alias.Titulo)]
        [Remote(action: "EntradaDisponible", controller: "Entradas")]
        public string Titulo { get; set; } = String.Empty;
        
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Display(Name = Alias.CategoriaId)]
        public int CategoriaId {get; set;}

        public Categoria Categoria { get; set; }

       
        public int MiembroId {get; set;}

        public Miembro Miembro { get; set; }
        public bool Privada {get; set;}
        public List<MiembrosEntradas> MiembrosHabilitados { get; set; }
        public List<Pregunta> Preguntas { get; set; }

        public string MiembroEstaHabilitado(int userId)
        {
            string resultado = "No habilitado";
            foreach (MiembrosEntradas miembroEntrada in MiembrosHabilitados)
            {
                if (miembroEntrada.EntradaId == this.Id && miembroEntrada.MiembroId == userId)
                {
                    if (miembroEntrada.Habilitado)
                    {
                        resultado = "Habilitado";
                    }
                    else
                    {
                        resultado = "Pendiente";
                    }
                }
            }
            return resultado;
        }

        public int CantidadDePreguntasYRespuestas
        {
            get
            {
                int cantidad = 0;
                if(Preguntas != null)
                {
                    foreach(Pregunta pregunta in Preguntas)
                    {
                        cantidad += pregunta.CantidadDeRespuestas;
                    }
                    cantidad += Preguntas.Count;
                }
                return cantidad;
            }
        }

        public int Antiguedad
        {
            get
            {
                return (int)(DateTime.Now - Fecha).TotalDays;
            }
        }

        

}
}
