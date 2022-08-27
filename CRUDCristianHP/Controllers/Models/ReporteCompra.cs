namespace CRUDCristianHP.Controllers.Models
{
    public class ReporteCompra
    {
        public int DniCliente { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public DateTime Fecha { get; set; }
        public string NombresCliente { get; set; }
        public string ApellidosCliente { get; set; }
        public string NombreProducto { get; set; }
        public float PrecioProducto { get; set; }
    }
}
