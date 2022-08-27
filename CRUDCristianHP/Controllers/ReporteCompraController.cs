using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CRUDCristianHP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteCompraController : Controller
    {
        string connectionString = "Data Source=127.0.0.1; Initial Catalog=inclub; User ID=sa; Password=Root2030; Encrypt=False;";
        // Peticion devuelve el reporte de la compra por medio del IDUsuario 
        // Utilizando el procedimientos almacenados
        [HttpGet("{id:int}")]  
        public IActionResult ObtenerReporteCompra(int id)
        {
            try
            {            
                using (var connection = new SqlConnection(connectionString))
                {
                    //lst = connection.Query<Models.ReporteCompra>(querySelectCompra, new { id = id });
                    var r = connection.Query("ListarCompraUsuario", new {idUsuario= id} , commandType: CommandType.StoredProcedure).ToList();
                    return Ok(r);
                }                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
