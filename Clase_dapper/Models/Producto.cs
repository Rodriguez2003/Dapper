using System.ComponentModel.DataAnnotations;

namespace Clase_dapper.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public bool State { get; set; } = true;
    }
}
