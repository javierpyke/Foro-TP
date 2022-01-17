using FORO_C.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.Models
{
    public class Usuario : IdentityUser<int>
    {
        //public int Id { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.Text)]
        [StringLength(Restrictions.MaxNombre, MinimumLength = Restrictions.MinNombre, ErrorMessage = ErrMsgs.StrMaxAndMin)]
        [Display(Name = Alias.Nombre)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.Text)]
        [StringLength(Restrictions.MaxNombre, MinimumLength = Restrictions.MinNombre, ErrorMessage = ErrMsgs.StrMaxAndMin)]
        [Display(Name = Alias.Apellido)]
        public string Apellido { get; set; } = string.Empty;

        //[Required(ErrorMessage = ErrMsgs.Requerido)]
        //[DataType(DataType.EmailAddress, ErrorMessage = ErrMsgs.DTypeEmail)]
        //[StringLength(Restrictions.MaxEmail, MinimumLength = Restrictions.MinEmail, ErrorMessage = ErrMsgs.StrMaxAndMin)]
        //[Display(Name = Alias.Email)]
        //[Remote(action: "EmailDisponible",controller:"usuarios")]
        //public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.Password)]
        [StringLength(Restrictions.MaxPassword, MinimumLength = Restrictions.MinPassword, ErrorMessage = ErrMsgs.StrMaxAndMin)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; } = Guid.NewGuid().ToString();


    }
}
