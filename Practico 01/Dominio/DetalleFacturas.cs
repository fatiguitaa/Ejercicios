using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practico_01.Dominio
{
    public class DetalleFacturas
    {
        public int IdDetalleFacturas { get; set; }
        public int NroFactura { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
    }
}
