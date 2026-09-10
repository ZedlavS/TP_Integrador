using System;
using System.Collections.Generic;
using System.Linq;

public class Equipo
{
    // Variables / Atributos
    public Heroe[] Heroes { get; set; }
    public Zona[] Zonas { get; set; }
    public string Nombre { get; set; }
// Estados del Equipo
public enum EstadoEquipo
{
    Disponible,
    EnMision,
    EnDescanso
}

// Estados individuales del Héroe
public enum EstadoHeroe
{
    Disponible,
    Ocupado,
    Herido
}
    // Estado propio del equipo
    public EstadoEquipo EstadoActual { get; set; }

    // Constantes de regla de negocio
    private const int MIN_INTEGRANTES = 2;
    private const int MAX_INTEGRANTES = 5;

    // Constructor
    public Equipo(string nombre, Heroe[] heroes, Zona[] zonas)
    {
        if (heroes == null || heroes.Length < MIN_INTEGRANTES || heroes.Length > MAX_INTEGRANTES)
        {
            throw new ArgumentException($"Un equipo debe tener entre {MIN_INTEGRANTES} y {MAX_INTEGRANTES} integrantes.");
        }

        Nombre = nombre;
        Heroes = heroes;
        Zonas = zonas ?? new Zona[0];
        EstadoActual = EstadoEquipo.Disponible;
    }

    /// <summary>
    /// Calcula la capacidad del equipo considerando la cantidad de integrantes
    /// y la suma total de sus estrellas.
    /// </summary>
    public int Capacidad()
    {
        int totalEstrellas = 0;
        int cantidadIntegrantes = 0;

        foreach (Heroe heroe in Heroes)
        {
            if (heroe != null)
            {
                totalEstrellas += heroe.Estrellas;
                cantidadIntegrantes++;
            }
        }

        // Ejemplo de cálculo: Total de estrellas multiplicado por la cantidad de integrantes
        return totalEstrellas * cantidadIntegrantes;
    }

    /// <summary>
    /// Retorna el estado actual del equipo en formato String.
    /// Si el equipo figura como "Disponible" pero sus integrantes no lo están,
    /// lo notifica.
    /// </summary>
    public string Estado()
    {
        if (EstadoActual == EstadoEquipo.Disponible)
        {
            return PuedeParticiparEnMision() 
                ? "Disponible" 
                : "No disponible (Integrantes no cumplen disponibilidad mínima)";
        }

        return EstadoActual.ToString();
    }

    /// <summary>
    /// Retorna la lista de héroes del equipo que están individualmente Disponibles.
    /// </summary>
    public Heroe[] IntegrantesDisp()
    {
        List<Heroe> disponibles = new List<Heroe>();

        foreach (Heroe heroe in Heroes)
        {
            if (heroe != null && heroe.Estado == EstadoHeroe.Disponible)
            {
                disponibles.Add(heroe);
            }
        }

        return disponibles.ToArray();
    }

    /// <summary>
    /// Evalúa si el equipo está en condiciones de asumir una nueva misión.
    /// Requiere que el equipo esté marcado como Disponible y que conserve al menos
    /// el mínimo de integrantes (2) en estado Disponible.
    /// </summary>
    public bool PuedeParticiparEnMision()
    {
        if (EstadoActual != EstadoEquipo.Disponible)
        {
            return false;
        }

        int integrantesDisponibles = IntegrantesDisp().Length;
        return integrantesDisponibles >= MIN_INTEGRANTES;
    }
}