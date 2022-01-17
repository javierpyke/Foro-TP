using FORO_C.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.Models
{
    public class Pregunta
    {
        public int Id { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [StringLength(Restrictions.MaxPregunta, MinimumLength = Restrictions.MinPregunta, ErrorMessage = ErrMsgs.StrMaxAndMin)]
        public string Descripcion { get; set; }

        [Display(Name = Alias.EntradaId)]
        public int EntradaId { get; set; }
        public Entrada Entrada { get; set; }

        public List<Respuesta> Respuestas { get; set; }

        [Display(Name = Alias.MiembroId)]
        public int MiembroId { get; set; }
        public Miembro Miembro { get; set; }

        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;
        public bool Activa { get; set; } = false;

        public int CantidadDeRespuestas
        {
            get
            {
                if(Respuestas != null)
                {
                    return Respuestas.Count;
                } else
                {
                    return 0;
                }

                
            }
        }


    }
}
