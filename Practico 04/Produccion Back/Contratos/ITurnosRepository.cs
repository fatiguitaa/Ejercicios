using Produccion_Back.Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produccion_Back.Contratos
{
    public interface ITurnosRepository
    {
        List<Turno> ObtenerTodos();
        Turno? ObtenerPorId(int id);
        void Crear(Turno turno);
        void Modificar(Turno turno);
        void Eliminar(int id);
    }
}
