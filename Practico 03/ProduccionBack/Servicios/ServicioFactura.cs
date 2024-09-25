using ProduccionBack.Contratos;
using ProduccionBack.Datos;
using ProduccionBack.Modelos;

namespace ProduccionBack.Servicios
{
    public class ServicioFactura : IFacturaRepository
    {
        public List<Factura>? ObtenerTodas() { return FacturaRepository.ObtenerInstancia().ObtenerTodas(); }

        public Factura? ObtenerPorId(int idFactura) => FacturaRepository.ObtenerInstancia().ObtenerPorId(idFactura);
        
        public bool? Crear(Factura factura) => FacturaRepository.ObtenerInstancia().Crear(factura);

        public bool? Modificar(Factura factura) => FacturaRepository.ObtenerInstancia().Modificar(factura);

        public bool? Eliminar(int idFactura) => FacturaRepository.ObtenerInstancia().Eliminar(idFactura);
    }
}
