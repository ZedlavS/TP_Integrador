public class Poder
{
    public nivelPoder Nivel { get; set; }
    public string Tipo { get; set; }

    public string Nombre { get; set; }

}

public enum nivelPoder
{
    Bajo,
    Medio,
    Alto
}
