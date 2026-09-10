namespace TuProyecto.Dominio{
    public class Heroe
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IdentidadSecreta { get; set; }
        public int CantidadIncidentes { get; set; }
        public int CantidadExitos { get; set; }
        public Poder Poder { get; set; }
        public int NivelPoder { get; set; }
        public EstadoHeroe Estado { get; set; } = EstadoHeroe.Disponible;

        // Propiedad calculada requerida por la clase Equipo
        public int Estrellas => CalcularEstrellas();

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

        public void RecibirDaño()
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

