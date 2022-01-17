using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.Helpers
{
    public class ErrMsgs
    {
        public const string Requerido = "El campo {0} es requerido";

        public const string StrMax = "El campo {0} admite una longitud máxima de {1} caracteres";

        public const string StrMaxAndMin = "El campo {0} admite una longitud mínima de {2} caracteres y máxima de {1} caracteres";

        public const string StrEmail = "El campo {0} debe ser una dirección válida";

        public const string DTypeEmail = "Ingrese un correo electronico válido";

        public const string PassMissmatch = "La contraseña difiere de la ingresada";

        public const string PassIncorrect = "Ingrese una contraseña válida";

    }
}
