using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.Data.SqlClient;
using CRUDCristianHP.Common;

namespace CRUDCristianHP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        string connectionString = "Data Source=127.0.0.1; Initial Catalog=inclub; User ID=sa; Password=Root2030; Encrypt=False;";

        [HttpGet]
        public IActionResult ObtenerProductos()
        {
            try
            {
                IEnumerable<Models.Producto> lst = null;
                var querySelectProductos = "SELECT * FROM Productos";
                using (var connection = new SqlConnection(connectionString))
                {
                    lst = connection.Query<Models.Producto>(querySelectProductos).ToList();
                }
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult ObtenerUsuario(int id)
        {
            try
            {
                IEnumerable<Models.Producto> result = null;
                var querySelectProducto = "SELECT * FROM Productos WHERE id = @id";
                using (var connection = new SqlConnection(connectionString))
                {
                    result = connection.Query<Models.Producto>(querySelectProducto, new { id = id });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CrearProducto(Models.Producto model)
        {
            try
            {
                int result = 0;              
                using (var connection = new SqlConnection(connectionString))
                {
                    var queryInsertProducto = "INSERT INTO Productos ( Nombre, Precio, Descripcion ) " +
                                             " VALUES ( @Nombre, @Precio, @Descripcion )";
                    result = connection.Execute(queryInsertProducto, model);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult EditarProducto(Models.Producto model)
        {
            try
            {
                int result = 0;
                using (var connection = new SqlConnection(connectionString))
                {
                    var queryInsertProducto = "UPDATE Productos set " +
                                             "Nombre = @Nombre, " +
                                             "Precio = @Precio, " +
                                             "Descripcion = @Descripcion " +
                                             "where id = @id";
                    
                    result = connection.Execute(queryInsertProducto, model);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult EliminarProducto(Models.Producto model)
        {
            try
            {
                int result = 0;
                using (var connection = new SqlConnection(connectionString))
                {
                    var queryInsertProducto = "DELETE FROM Productos where id = @id";
                    result = connection.Execute(queryInsertProducto, model);
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
