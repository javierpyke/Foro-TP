using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FORO_C.Helpers
{
	public static class Alias
	{
		public const string Descripcion = "Descripción";
		public const string Nombre = "Nombre";
		public const string PreguntaId = "Pregunta";
		public const string MiembroId = "Miembro";
		public const string EntradaId = "Entrada";
		public const string CategoriaId = "Categoria";
		public const string RespuestaId = "Respuesta";
		public const string Apellido = "Apellido";
		public const string Email = "Correo Electrónico";
		public const string Password = "Contraseña";
		public const string Telefono = "Teléfono";
		public const string Titulo = "Título";
		public const string RolName = "Nombre del Rol";

		public static class AliasGUI
		{
			public static string Create { get { return "Crear"; } }
			public static string Delete { get { return "Eliminar"; } }
			public static string Edit { get { return "Editar"; } }
			public static string Details { get { return "Detalles"; } }
			public static string Back { get { return "Volver atras"; } }
			public static string BackToList { get { return "Volver al listado"; } }
			public static string SureToDelete { get { return "¿Está seguro que desea proceder con la eliminación?"; } }
			public static string ListOf { get { return "Listado de "; } }
		}
	}
}
