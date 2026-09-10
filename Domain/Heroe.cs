public class Heroe
{

    public int Id { get; set; }
    public string Nombre { get; set; }
    public string IdentidadSecreta { get; set; }
    public int CantidadInicidentes { get; set; }
    public int CantidadExitos { get; set; }
    public Poder Poder { get; set; }
    public int NivelPoder { get; set; }
    public EstadoHeroe Estado { get; set; } = EstadoHeroe.Activo;
    public enum EstadoHeroe
    {
        Activo,
        Inactivo,
        Herido,
    }

    public int CalcularEstrellas()
    {
        double PuntosPorExitos = 0.2;
        double PuntosPorIncidentes = 0.3;

        double resultado = NivelPoder + (CantidadExitos * PuntosPorExitos) - (CantidadInicidentes * PuntosPorIncidentes); //Bro hace un calculo de estrellas basado en el nivel de poder, la cantidad de exitos y la cantidad de incidentes 
        return (int)Math.Clamp(Math.Round(resultado), 1, 10); // Limita el resultado entre 0 y 10
    }

    public int efectividad()
    {
        if (CantidadInicidentes == 0)
        {
            return 100; // Si no hay incidentes la efectividad es del 100%
        }
        else
        {
            double efectividad = ((double)CantidadExitos / CantidadInicidentes) * 100;
            return (int)Math.Round(efectividad); // Redondea el resultado al entero más cercano
        }
    }  
    
    public void RecibirDaño()
    {
        Estado = EstadoHeroea.Herido;
    }

    public void Cansado()
    {
        if (Estado != EstadoHeroea.Herido)
        {
            Estado = EstadoHeroea.Inactivo;
        }
    }

    public void Recuperar()
    {
        if (Estado == EstadoHeroea.Inactivo)
        {
            Estado = EstadoHeroea.Activo;
        }
    }

}

