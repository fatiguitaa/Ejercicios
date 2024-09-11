using Practico_01.Datos;
using Practico_01.Dominio;

namespace Practico_01.Servicios
{
    public class GestorFacturas : IFacturaRepository
    {
        private FacturaRepository facturaRepository = new FacturaRepository();

        public List<Factura> ObtenerTodas()
        {
            return facturaRepository.ObtenerTodas();
        }

        public Factura ObtenerPorId(int id)
        {
            return facturaRepository.ObtenerPorId(id);
        }

        public bool Guardar(Factura factura)
        {
            return facturaRepository.Guardar(factura);
        }

        public bool BorrarPorId(int id)
        {
            return facturaRepository.BorrarPorId(id);
        }
    }
}
