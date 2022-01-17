using FORO_C.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FORO_C.Models
{
    public class Respuesta
    {
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [StringLength(Restrictions.MaxRespuesta, MinimumLength = Restrictions.MinRespuesta, ErrorMessage = ErrMsgs.StrMaxAndMin)]
        [Display(Name = Alias.Descripcion)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Display(Name = Alias.PreguntaId)]
        public int PreguntaId { get; set; }
        public Pregunta Pregunta { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Display(Name = Alias.MiembroId)]
        public int MiembroId { get; set; }
        public Miembro Miembro { get; set; }

        public DateTime? Fecha { get; set; } = DateTime.Now;

        public List<Reaccion> Reacciones { get; set; }

        public int getReaccionUsuario(int userId)
        {
                int resultado = 0;
                if (this.Reacciones != null)
                {
                    foreach (Reaccion reaccion in this.Reacciones)
                    {
                        if(reaccion.MiembroId == userId)
                        {
                            resultado = reaccion.MeGusta;
                        }
                    }

                }
                return resultado;
        }

        public int[] cantidadDeReacciones()
        {
            int[] valores = new int[2];
            int contarMeGusta = 0;
            int contarNoMeGusta = 0;
            foreach (Reaccion reaccion in Reacciones)
            {
                if (reaccion.MeGusta == 1)
                {
                    contarMeGusta++;
                }
                else if (reaccion.MeGusta == -1)
                {
                    contarNoMeGusta++;
                }
            }
            valores[0] = contarMeGusta;
            valores[1] = contarNoMeGusta;
            return valores;
        }
    }
}

