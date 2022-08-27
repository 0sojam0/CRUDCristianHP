using CRUDCristianHP.Common;
using CRUDCristianHP.Controllers.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CRUDCristianHP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        string connectionString = "Data Source=127.0.0.1; Initial Catalog=inclub; User ID=sa; Password=Root2030; Encrypt=False;";

        [HttpGet]
        public IActionResult ObtenerUsuarios()
        {
            try
            {
                IEnumerable<Models.Usuario> lst = null;
                var querySelectUsuarios = "select * from Usuarios";
                using (var connection = new SqlConnection(connectionString))
                {
                    lst = connection.Query<Models.Usuario>(querySelectUsuarios).ToList();
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
                IEnumerable<Models.Usuario> result = null;
                var querySelectUsuario = "SELECT * FROM Usuarios WHERE id = @id";
                using (var connection = new SqlConnection(connectionString))
                {
                    result = connection.Query<Models.Usuario>(querySelectUsuario, new { id = id });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CrearUsuario(Models.Usuario model)
        {
            try
            {
                int result = 0;
                // Utilizamos el metodo de Ecriptacion en creado en Common
                string passwordEncrypt = CommonMethods.ConvertToEncrypt(model.Pass); 
                if(passwordEncrypt == "")
                {
                    return Ok("Ingrese Contraseña valida");
                }
                using (var connection = new SqlConnection(connectionString))
                {
                    var queryInsertUsuario = "INSERT INTO dbo.Usuarios (Nombres, Apellidos,NombreUsuario, Pass) " +
                                             " VALUES (@Nombres, @Apellidos, @NombreUsuario, @passwordEncrypt)";
                    result = connection.Execute(queryInsertUsuario, new { 
                        Nombres = model.Nombres, 
                        Apellidos = model.Apellidos, 
                        NombreUsuario = model.NombreUsuario, 
                        passwordEncrypt = passwordEncrypt 
                    });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult EditarUsuario(Models.Usuario model)
        {
            try
            {
                int result = 0;
                using (var connection = new SqlConnection(connectionString))
                {
                    var queryInsertUsuario = "UPDATE Usuarios set " +
                                             "Nombres = @Nombres, " +
                                             "Apellidos = @Apellidos, " +
                                             "NombreUsuario = @NombreUsuario " +
                                             "where id = @id";
                    result = connection.Execute(queryInsertUsuario, model);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult EliminarUsuario(Models.Usuario model)
        {
            try
            {
                int result = 0;
                using (var connection = new SqlConnection(connectionString))
                {
                    var queryInsertUsuario = "DELETE FROM Usuarios where id = @id";
                    result = connection.Execute(queryInsertUsuario, model);
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
