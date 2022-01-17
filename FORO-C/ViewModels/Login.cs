using FORO_C.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.ViewModels
{
    public class Login
    {

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.EmailAddress, ErrorMessage = ErrMsgs.DTypeEmail)]
        [StringLength(Restrictions.MaxEmail, MinimumLength = Restrictions.MinEmail, ErrorMessage = ErrMsgs.StrMaxAndMin)]
        [Display(Name = Alias.Email)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.Password, ErrorMessage = ErrMsgs.PassIncorrect)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }

        public bool Recordarme { get; set; } = false;


    }
}
