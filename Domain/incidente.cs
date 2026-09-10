namespace.TuProyecto.Dominio{
    public class Incidente
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string ContactoNombre { get; set; }
        public int ContactoNumero { get; set; }
        public double Ubicacion { get; set; }
        public int NivelPrioridad { get; set; }
        public string Tipo { get; set; }
        public int NivelDificultad { get; set; }
        public string TipoPoder { get; set; }
        
        public EstadoIncidente EstadoActual { get; set; } = EstadoIncidente.NoHecho;

        // Regla de negocio: Umbral para emergencia general (ejemplo: dificultad > 8)
        private const int UMBRAL_EMERGENCIA_GENERAL = 8;

        /// <summary>
        /// Evalúa si el incidente requiere la asignación de dos equipos en lugar de uno.
        /// </summary>
        public bool EsEmergenciaGeneral()
        {
            return NivelDificultad >= UMBRAL_EMERGENCIA_GENERAL;
        }

        public void IniciarIncidente()
        {
            if (EstadoActual == EstadoIncidente.NoHecho)
            {
                EstadoActual = EstadoIncidente.EnCurso;
            }
        }

        public void FinalizarIncidente()
        {
            if (EstadoActual == EstadoIncidente.EnCurso)
            {
                EstadoActual = EstadoIncidente.Hecho;
            }
        }
    }

}

