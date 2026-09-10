public class Poder
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Tipo { get; set; } // Ejemplo: Fuerza, Velocidad, Telekinesis, Vuelo

    public Poder() { }

    public Poder(string nombre, string tipo)
    {
        Nombre = nombre;
        Tipo = tipo;
    }
}
