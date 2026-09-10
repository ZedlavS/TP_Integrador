using System.Collections.Generic;

public class Zona
{
    //Variables (Propiedades en C#)
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<Equipo> EquiposHabituales { get; set; }

    //Constructor
    public Zona()
    {
        EquiposHabituales = new List<Equipo>();
    }

    //Funciones (Métodos)
    public string GetNombre()
    {
        return Nombre;
    }

    public List<Equipo> ObtenerEquipos()
    {
        return EquiposHabituales;
    }

    public void AgregarEquipo(Equipo e)
    {
        if (e != null && !EquiposHabituales.Contains(e))
        {
            EquiposHabituales.Add(e);
        }
    }

    public void RemoverEquipo(Equipo e)
    {
        if (e != null && EquiposHabituales.Contains(e))
        {
            EquiposHabituales.Remove(e);
        }
    }
}