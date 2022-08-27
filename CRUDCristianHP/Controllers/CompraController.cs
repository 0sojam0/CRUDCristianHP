using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Data.SqlClient;
using CRUDCristianHP.Common;

namespace CRUDCristianHP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : Controller
    {
        string connectionString = "Data Source=127.0.0.1; Initial Catalog=inclub; User ID=sa; Password=Root2030; Encrypt=False;";

        [HttpGet]
        public IActionResult ObtenerCompras()
        {
            try
            {
                IEnumerable<Models.Compra> lst = null;
                var querySelectCompra = "SELECT * FROM Compras";
                using (var connection = new SqlConnection(connectionString))
                {
                    lst = connection.Query<Models.Compra>(querySelectCompra).ToList();
                }
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")] //Consulta lista de compras por IDUsuario
        public IActionResult ObtenerCompra(int id)
        {
            try
            {
                IEnumerable<Models.Compra> result = null;
                var querySelectCompra = "SELECT * FROM Compras WHERE IdUsuario = @id";
                using (var connection = new SqlConnection(connectionString))
                {
                    result = connection.Query<Models.Compra>(querySelectCompra, new { id = id });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CrearCompra(Models.Compra[] model)
        {
            try
            {
                int result = 0;
                using (var connection = new SqlConnection(connectionString))
                {
                    var queryInsertCompra = "INSERT INTO Compras ( DniCliente, IdUsuario, IdProducto, Fecha ) " +
                                             " VALUES ( @DniCliente, @IdUsuario, @IdProducto, @Fecha  )";
                    for(int i=0; i<model.Length; i++)
                    {
                        var compra = model[i];
                        result = connection.Execute(queryInsertCompra, compra);
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult EditarCompra(Models.Compra[] model)
        {
            try
            {
                int result = 0;
                using (var connection = new SqlConnection(connectionString))
                {
                    var queryUpdateCompra = "UPDATE Compras set " +
                                             "DniCliente = @DniCliente, " +
                                             "IdUsuario = @IdUsuario, " +
                                             "IdProducto = @IdProducto, " +
                                             "Fecha = @Fecha " +
                                             "where id = @id";  
                    
                    for (int i = 0; i < model.Length; i++)
                    {
                        var compra = model[i];
                        result = connection.Execute(queryUpdateCompra, compra);
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult EliminarCompra(Models.Compra[] model)
        {
            try
            {
                int result = 0;
                using (var connection = new SqlConnection(connectionString))
                {
                    var queryDeleteCompra = "DELETE FROM Compras where id = @id";                    
                    for (int i = 0; i < model.Length; i++)
                    {
                        var compra = model[i];
                        result = connection.Execute(queryDeleteCompra, compra);
                    }
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
