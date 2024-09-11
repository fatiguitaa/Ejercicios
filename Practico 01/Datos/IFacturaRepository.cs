using Practico_01.Dominio;

namespace Practico_01.Datos
{
    public interface IFacturaRepository
    {
        public List<Factura> ObtenerTodas();
        public Factura ObtenerPorId(int id);
        public bool Guardar(Factura factura);
        public bool BorrarPorId(int id);
    }
}
