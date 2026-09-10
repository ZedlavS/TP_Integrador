using System;
using System.Collections.Generic;

namespace TuProyecto.Dominio
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<Heroe> Heroes { get; set; } = new List<Heroe>();
        public List<Zona> Zonas { get; set; } = new List<Zona>();
        public EstadoEquipo EstadoActual { get; set; } = EstadoEquipo.Disponible;

        private const int MIN_INTEGRANTES = 2;
        private const int MAX_INTEGRANTES = 5;

        public Equipo() { }

        public Equipo(string nombre, List<Heroe> heroes, List<Zona> zonas)
        {
            if (heroes == null || heroes.Count < MIN_INTEGRANTES || heroes.Count > MAX_INTEGRANTES)
            {
                throw new ArgumentException($"Un equipo debe tener entre {MIN_INTEGRANTES} y {MAX_INTEGRANTES} integrantes.");
            }

            Nombre = nombre;
            Heroes = heroes;
            Zonas = zonas ?? new List<Zona>();
            EstadoActual = EstadoEquipo.Disponible;
        }

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

            return totalEstrellas * cantidadIntegrantes;
        }

        public List<Heroe> IntegrantesDisp()
        {
            List<Heroe> disponibles = new List<Heroe>();

            foreach (Heroe heroe in Heroes)
            {
                if (heroe != null && heroe.Estado == EstadoHeroe.Disponible)
                {
                    disponibles.Add(heroe);
                }
            }

            return disponibles;
        }

        public bool PuedeParticiparEnMision()
        {
            if (EstadoActual != EstadoEquipo.Disponible) return false;

            return IntegrantesDisp().Count >= MIN_INTEGRANTES;
        }
    }
}