using TuProyecto.Dominio;

namespace TuProyecto.Servicios
{
    public class HeroeService
    {
       
        private readonly List<Heroe> _heroes = new();

        public IEnumerable<Heroe> ObtenerTodos() => _heroes;

        public Heroe? ObtenerPorId(int id) => _heroes.FirstOrDefault(h => h.Id == id);

        public Heroe CrearHeroe(Heroe nuevoHeroe)
        {
            nuevoHeroe.Id = _heroes.Count + 1;
            _heroes.Add(nuevoHeroe);
            return nuevoHeroe;
        }

     
        public bool EstaDisponible(int heroeId)
        {
            var heroe = ObtenerPorId(heroeId) 
            ?? throw new KeyNotFoundException($"El héroe con ID {heroeId} no existe.");

            return heroe.Estado == EstadoHeroe.Disponible;
        }

     
        public void ValidarDisponibilidadParaOperacion(int heroeId)
        {
            var heroe = ObtenerPorId(heroeId) 
                ?? throw new KeyNotFoundException($"El héroe con ID {heroeId} no existe.");

            if (heroe.Estado == EstadoHeroe.Herido)
            {
                throw new InvalidOperationException($"El héroe '{heroe.Nombre}' está Herido y no puede participar de ninguna operación.");
            }

            if (heroe.Estado == EstadoHeroe.Inactivo)
            {
                throw new InvalidOperationException($"El héroe '{heroe.Nombre}' está Inactivo/Cansado y debe recuperarse antes de participar.");
            }
        }

       
        public void RegistrarDano(int heroeId)
        {
            var heroe = ObtenerPorId(heroeId) 
                ?? throw new KeyNotFoundException($"El héroe con ID {heroeId} no existe.");

            heroe.RecibirDano();
        }

     
        public void RecuperarHeroe(int heroeId)
        {
            var heroe = ObtenerPorId(heroeId) 
                ?? throw new KeyNotFoundException($"El héroe con ID {heroeId} no existe.");

          
            if (heroe.Estado == EstadoHeroe.Inactivo)
            {
                heroe.Recuperar();
            }

        }

        // REGLA 5: Actualizar resultados tras participar en un incidente
        public void RegistrarResultadoIncidente(int heroeId, bool exito)
        {
            var heroe = ObtenerPorId(heroeId) 
                ?? throw new KeyNotFoundException($"El héroe con ID {heroeId} no existe.");

            if (exito)
            {
                heroe.CantidadExitos++;
                heroe.Cansado(); 
            }
            else
            {
                heroe.CantidadIncidentes++;
                heroe.RecibirDano(); 
            }
        }

        public void ValidarDisponibilidadParaOperacion(int heroeId)
        {
            var heroe = ObtenerPorId(heroeId);

            if (heroe.Estado == EstadoHeroe.EnMision)
            {
                throw new InvalidOperationException($"El héroe '{heroe.Nombre}' ya se encuentra En Misión con otro equipo.");
            }

            if (heroe.Estado == EstadoHeroe.Herido)
            {
                throw new InvalidOperationException($"El héroe '{heroe.Nombre}' está Herido y no puede participar de ninguna operación.");
            }

            if (heroe.Estado == EstadoHeroe.Inactivo)
            {
                throw new InvalidOperationException($"El héroe '{heroe.Nombre}' está Inactivo/Cansado y debe recuperarse antes de participar.");
            }
        }

     
        public void AsignarAMision(int heroeId)
        {
            ValidarDisponibilidadParaOperacion(heroeId);
            var heroe = ObtenerPorId(heroeId);
            heroe.Estado = EstadoHeroe.EnMision;
        }


        public void FinalizarMision(int heroeId)
        {
            var heroe = ObtenerPorId(heroeId);
            if (heroe.Estado == EstadoHeroe.EnMision)
            {
                heroe.Estado = EstadoHeroe.Inactivo; 
            }
        }
    }
}