using FORO_C.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.Models
{
    public class Reaccion
    {


        [Required (ErrorMessage = ErrMsgs.Requerido)]
        [Range(Restrictions.MinReaccion, Restrictions.MaxReaccion)]
        public int MeGusta { get; set; } = 0;

        [Key]
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Display(Name = Alias.RespuestaId)]
        public int RespuestaId { get; set;}
        public Respuesta Respuesta { get; set; } = null;

        [Key]
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Display(Name = Alias.MiembroId)]
        public int MiembroId { get; set; }
        public Miembro Miembro { get; set; } = null;

        [Required(ErrorMessage =ErrMsgs.Requerido)]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        internal static Reaccion CrearReaccion(int MeGusta, int RespuestaId)
        {
            Reaccion reaccion = new Reaccion();
            reaccion.MeGusta = MeGusta;
            reaccion.RespuestaId = RespuestaId;
            return reaccion;
        }
    }
}
