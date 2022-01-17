using FORO_C.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.Models
{
    public class MiembrosEntradas
    {
        [Key]
        [Display(Name = Alias.MiembroId)]
        public int MiembroId { get; set; }

        [Key]
        [Display(Name = Alias.EntradaId)]
        public int EntradaId { get; set; }

        public bool Habilitado { get; set; }


        public Miembro Miembro { get; set; }
        public Entrada Entrada { get; set; }

    }
}
