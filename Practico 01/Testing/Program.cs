using Practico_01.Dominio;
using Practico_01.Servicios;

var gestorFacturas = new GestorFacturas();

foreach(Factura factura in gestorFacturas.ObtenerTodas())
{
    Console.WriteLine(
        $"Nro:{factura.NroFactura} " +
        $"| " +
        $"Fecha: {factura.Fecha} | Id forma de pago: {factura.IdFormaPago} "
    );
}

var facturaUnica = gestorFacturas.ObtenerPorId(3);
Console.WriteLine(
    $"Factura obtenida por id: " +
    $"Nro:{facturaUnica.NroFactura} " +
    $"| " +
    $"Fecha: {facturaUnica.Fecha} | Id forma de pago: {facturaUnica.IdFormaPago} " 
);


var detalle = new DetalleFacturas();
detalle.NroFactura = 14;
detalle.IdArticulo = 2;
detalle.Cantidad = 1;

var detalles = new List<DetalleFacturas>();
detalles.Add(detalle);

var facturaSave = new Factura();
facturaSave.Fecha = DateTime.Now;
facturaSave.IdFormaPago = 2;
facturaSave.Detalles = detalles;

if (gestorFacturas.Guardar(facturaSave))
{
    Console.WriteLine("Factura guardada con exito;");
}
else
{
    Console.WriteLine("Ha habido un error al guardar la factura");
}

if (gestorFacturas.BorrarPorId(3))
{
    Console.WriteLine("Factura y todos sus detalles eliminados con exito.");
}
else
{
    Console.WriteLine("Ha habido un error al eliminar la factura.");
}

