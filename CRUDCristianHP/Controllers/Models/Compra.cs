namespace CRUDCristianHP.Controllers.Models
{
    public class Compra
    {
        public int id { get; set; }
        public int DniCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
