using FORO_C.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.Models
{
    public class Miembro : Usuario
    {

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.PhoneNumber)]
        [StringLength(Restrictions.MaxTelefono, MinimumLength = Restrictions.MinTelefono, ErrorMessage = ErrMsgs.StrMaxAndMin)]
        [Display(Name = Alias.Telefono)]
        public string Telefono { get; set; } = string.Empty;


        public List<Entrada> EntradasPropias { get; set; }


        //public int PreguntaId { get; set; }
        public List<Pregunta> Preguntas { get; set; }


        //public int RespuestaId { get; set; }
        public List<Respuesta> Respuestas { get; set; }


        //public int ReaccionId { get; set; }
        public List<Reaccion> PreguntasYRespuestasQueMeGustan { get; set; }


        public List<MiembrosEntradas> EntradasPermitidas { get; set; }

        public int CantEntradasUltimoMes
        {
            get
            {
                int cantidad = 0;
                if (EntradasPropias != null)
                {
                    DateTime fechaActual = DateTime.Now;
                    foreach (Entrada entrada in EntradasPropias)
                    {
                        if ((int)(fechaActual - entrada.Fecha).TotalDays < 30)
                        {
                            cantidad += 1;
                        }
                    }
                }

                return cantidad;
            }
        }

    }
}
