namespace TuProyecto.Dominio
{
    public class Poder
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;

        public Poder() { }

        public Poder(string nombre, string tipo)
        {
            Nombre = nombre;
            Tipo = tipo;
        }
    }
}

