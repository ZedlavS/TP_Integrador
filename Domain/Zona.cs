using System.Collections.Generic;

namespace TuProyecto.Dominio{

    public class Zona
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public Zona() { }

    public Zona(string nombre)
    {
        Nombre = nombre;
    }
}

}