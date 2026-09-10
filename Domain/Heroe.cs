using System;
using System.Collections.Generic;

namespace TuProyecto.Dominio
{
    public class Heroe
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string IdentidadSecreta { get; set; } = string.Empty;
        public int CantidadIncidentes { get; set; }
        public int CantidadExitos { get; set; }
        public int NivelPoder { get; set; }
        public List<Poder> Poderes { get; set; } = new List<Poder>();
        public EstadoHeroe Estado { get; set; } = EstadoHeroe.Disponible;

        // Propiedad calculada para las Estrellas
        public int Estrellas => CalcularEstrellas();

        public Heroe() { }

        public Heroe(string nombre, string identidadSecreta, int nivelPoder)
        {
            Nombre = nombre;
            IdentidadSecreta = identidadSecreta;
            NivelPoder = nivelPoder;
        }

        public int CalcularEstrellas()
        {
            double puntosPorExitos = 0.2;
            double puntosPorIncidentes = 0.3;

            double resultado = NivelPoder + (CantidadExitos * puntosPorExitos) - (CantidadIncidentes * puntosPorIncidentes);
            return (int)Math.Clamp(Math.Round(resultado), 1, 10);
        }

        public int Efectividad()
        {
            if (CantidadIncidentes == 0) return 100;
            
            double efectividad = ((double)CantidadExitos / CantidadIncidentes) * 100;
            return (int)Math.Round(efectividad);
        }

        public void RecibirDano()
        {
            Estado = EstadoHeroe.Herido;
        }

        public void Cansado()
        {
            if (Estado != EstadoHeroe.Herido)
            {
                Estado = EstadoHeroe.Inactivo;
            }
        }

        public void Recuperar()
        {
            if (Estado == EstadoHeroe.Inactivo)
            {
                Estado = EstadoHeroe.Disponible;
            }
        }
    }
}

