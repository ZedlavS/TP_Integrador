public class Poder
{
    public nivelPoder Nivel { get; set; }
    public string Tipo { get; set; }
    public string Origen { get; set; }
    public string Limitaciones { get; set; }
    public alcancePoder Alcance { get; set; }
    public accesorioPoder Accesorio { get; set; }

}

public enum nivelPoder
{
    Bajo,
    Medio,
    Alto
}

public enum alcancePoder
{
    Personal,
    Cercano,
    Lejano
}

public enum accesorioPoder
{
    Ninguno,
    Amuleto,
    Arma,
    Vestimenta
}