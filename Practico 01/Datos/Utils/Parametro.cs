namespace Practico_01.Datos.Utils
{
    public class Parametro
    {
        public string Nombre { get; set; }
        public object Valor { get; set; }
        public Parametro(string nombre, object valor)
        {
            Nombre = nombre;
            Valor = valor;
        }
    }
}
