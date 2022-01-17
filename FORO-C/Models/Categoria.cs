using FORO_C.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.Models
{
    public class Categoria
    {

        [Display(Name = Alias.CategoriaId)]
        public int Id { get; set; }

        [Required (ErrorMessage= ErrMsgs.Requerido)]
        [StringLength(Restrictions.MaxNombre, MinimumLength = Restrictions.MinNombre, ErrorMessage = ErrMsgs.StrMaxAndMin)]
        [Display(Name= Alias.Nombre)]
        [Remote(action: "CategoriaDisponible", controller: "Categorias")]
        public string Nombre {get; set;}
        public List<Entrada> Entradas { get; set; }

       

    }

    
}
