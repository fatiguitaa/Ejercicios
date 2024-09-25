namespace ProduccionBack.Datos.Utilidades
{
    public class Parametro
    {
        public Parametro(string nombre, object valor) 
        {
            this.Valor = valor;
            this.Nombre = nombre;
        }

        public string Nombre { get; set; }
        public object Valor { get; set; }
    }
}
