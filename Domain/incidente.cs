public class incidente{
    public String Descripcion{get; set;}
    public String ContactoNombre{get; set;}
    public Int  ContactoNumero{get;set;}
    public Float Ubicacion{get;set;}
    public Int NivelPrioridad{get;set;}
    public String Tipo{get;set;}
    public Int NivelDificultad{get;set;}
    public String TipoPoder{get;set;}
    public enum Estado{
         Hecho,
         NoHecho,
         EnCurso,
    }
     public void IniciarIncidente()
    {
        if (EstadoActual == Estado.NoHecho)
        {
            EstadoActual = Estado.EnCurso;
        }
    }


    // Función 2: pasar de EnCurso a Hecho
    public void FinalizarIncidente()
    {
        if (EstadoActual == Estado.EnCurso)
        {
            EstadoActual = Estado.Hecho;
        }
    }
}