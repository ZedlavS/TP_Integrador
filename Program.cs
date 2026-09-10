using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios para Controllers y convertir Enums a texto en JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// 2. Configurar la documentación interactiva de Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Sistema de Emergencias",
        Version = "v1",
        Description = "Punto de entrada RESTful para Héroes, Equipos, Incidentes y Central"
    });
});

var app = builder.Build();

// 3. Habilitar la pantalla gráfica de Swagger al ejecutar
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Emergencias v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// 4. Mapear las rutas definidas en los controladores
app.MapControllers();

app.Run();