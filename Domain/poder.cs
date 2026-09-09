public class Poder
{
    public nivelPoder Nivel { get; set; }
    public string Tipo { get; set; }

    public string nombre { get; set; }

}

public enum nivelPoder
{
    Bajo,
    Medio,
    Alto
}
