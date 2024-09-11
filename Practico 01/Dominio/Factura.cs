namespace Practico_01.Dominio
{
    public class Factura
    {
        public int NroFactura { get; set;}
        public DateTime Fecha { get; set;}
        public List<DetalleFacturas> Detalles { get; set;}
        public int IdFormaPago { get; set;}
    }
}
