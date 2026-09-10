using System.Collections.Generic;

namespace TuProyecto.Dominio
{
    public class BaseOperaciones
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public Zona ZonaUbicacion { get; set; } = new Zona ();
        public List<Heroe> HeroesResidentes { get; set; } = new List<Heroe>();

        public BaseOperaciones() { }

        public BaseOperaciones(string nombre, Zona zona)
        {
            Nombre = nombre;
            ZonaUbicacion = zona;
        }

        public void AgregarHeroe(Heroe heroe)
        {
            if (heroe != null && !HeroesResidentes.Contains(heroe))
            {
                HeroesResidentes.Add(heroe);
            }
        }
    }
}

