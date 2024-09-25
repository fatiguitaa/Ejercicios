namespace ProduccionBack.Modelos
{
    public class Factura
    {
        public int IdFactura { get; set; }

        public DateTime Fecha { get; set; }

        public int IdFormaPago { get; set; }

        public List<DetalleFactura> DetallesFactura { get; set; }
    }
}
