using System;
namespace prjClassroom.Models
{
	public class Documento
	{
		public int ID { get; set; }
		public string? Descripcion { get; set; }
		public string? Ruta { get; set; }

		public IFormFile? Archivo { get; set; }
    }
}

