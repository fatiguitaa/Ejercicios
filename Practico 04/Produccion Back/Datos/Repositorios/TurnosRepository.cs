using Produccion_Back.Contratos;
using Produccion_Back.Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Produccion_Back.Datos.Repositorios
{
    public class TurnosRepository : ITurnosRepository
    {
        private TurnosDBContext contexto;

        public TurnosRepository(TurnosDBContext contexto)
        {
            this.contexto = contexto;
        }

        public List<Turno> ObtenerTodos()
        {
            return contexto.Turnos.ToList();
        }

        public Turno? ObtenerPorId(int id)
        {
            return contexto.Turnos.Find(id);
        }

        public void Crear(Turno turno)
        {
            contexto.Turnos.Add(turno); 
            contexto.SaveChanges();
        }

        public void Modificar(Turno turno)
        {
            contexto.Turnos.Update(turno);
            contexto.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var turnoBorrar = ObtenerPorId(id);
            if (turnoBorrar != null)
            {
                contexto.Turnos.Remove(turnoBorrar);
                contexto.SaveChanges();
            }
        }
    }
}
