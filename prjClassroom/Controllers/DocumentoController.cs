using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using prjClassroom.Models;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace prjClassroom.Controllers
{
    [ApiController]
    [Route("/documento")]
    public class DocumentoController : ControllerBase
    {
        private readonly string? _rutaServidor;
        private readonly string? _cadenaSQL;

        public DocumentoController(IConfiguration _config)
        {
            _rutaServidor = _config.GetSection("Configuracion").GetSection("RutaServidor").Value;
            _cadenaSQL = _config.GetConnectionString("CadenaSQL");
        }
        [HttpPost]
        [Route("subir")]
        public IActionResult Subir([FromForm] Documento request)
        {
            string rutaDocumento = Path.Combine(_rutaServidor, request.Archivo.FileName);
            try
            {
                using(FileStream newFile = System.IO.File.Create(rutaDocumento))
                {
                    request.Archivo.CopyTo(newFile);
                    newFile.Flush();
                }
                using(var conexion = new SqlConnection(_cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_guardar_documento", conexion);
                    cmd.Parameters.AddWithValue("descripcion", request.Descripcion);
                    cmd.Parameters.AddWithValue("ruta", rutaDocumento);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }
                return Ok(new
                {
                    mensaje = "Documento Guardado"
                });
            }
            catch(Exception e)
            {
                return BadRequest(new
                {
                    mensaje = e.Message
                });
            }
            
        }
    }
}

