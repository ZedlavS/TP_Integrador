using System.Collections.Generic;

public class Zona
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Zona() { }

    public Zona(string nombre)
    {
        Nombre = nombre;
    }
}