using Clase_dapper.Data;
using Clase_dapper.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Clase_dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DbDataContext _dbContext;

        public ProductoController(DbDataContext dbContext)
        {
            _dbContext=dbContext;
        }

        [HttpGet("All")]
        public IActionResult GetAll()
        {
            var sql = "SELECT * FROM producto";
            using (var connection = _dbContext.crearconexion())
            {
                connection.Open();
                var result = connection.Query<Producto>(sql);
                var response = new
                {
                    status = 200,
                    message = "Listado de Productos",
                    data = result
                };
                return Ok (response);
            }
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            string sql = "SELECT * FROM producto WHERE ProductoId = @id"; //La sentencia SQL se puede declarar como String para hacer honor al tipado fuerte
            using (var connection = _dbContext.crearconexion())
            {
                connection.Open();
                var result = connection.Query<Producto>(sql, new {id = id}).FirstOrDefault();
                var response = new
                {
                    status = 200,
                    message = "Producto",
                    data = result
                };
                return Ok(response);
            }
        }
        [HttpPost("New/")]
        public IActionResult Insert([FromForm]Producto producto)
        {
            string sql = "INSERT INTO Producto  (Name, Price) VALUES(@Name, @Price)";
            using (var con = _dbContext.crearconexion())
            {
                con.Open();
                var result = con.Query<Producto>(sql, producto);
                var response = new
                {
                    status = 200,
                    message = "Nuevo Producto Registrado en la Base de Datos",
                    data = result
                };
                return Ok(response);
            }
        }
        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromForm] Producto producto)
        {
            var sql = "UPDATE producto SET Name=@Name, Price=@Price WHERE ProductoId=@id";
            using (var connection = _dbContext.crearconexion())
            {
                connection.Open();

                var result = connection.Execute(sql, new { Name = producto.Name, Price = producto.Price, Id = id });
                var response = new
                {
                    Status = 200,
                    Message = "Producto Actualizado",
                    Data = result
                };
                return Ok(response);
            }
        }
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var sql = "DELETE FROM producto WHERE ProductoId=@id";
            using (var connection = _dbContext.crearconexion())
            {
                connection.Open();
                var result = connection.Execute(sql, new { Id = id });
                var response = new
                {
                    Status = 200,
                    Message = "Producto Eliminado",
                    Data = result
                };
                return Ok(response);
            }
        }

    }
}
