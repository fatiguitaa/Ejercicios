namespace ProduccionBack.Modelos
{
    public class DetalleFactura
    {
        public int IdDetalle { get; set; }

        public int IdFactura { get; set; }

        public int IdArticulo { get; set; }

        public int Cantidad { get; set; }
    }
}
