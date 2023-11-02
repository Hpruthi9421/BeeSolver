using BeeSolver1;
using Microsoft.OpenApi.Models;
using System.Reflection;
 using Swashbuckle.AspNetCore.Annotations;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IMagicApiService, Magicapirequest>();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    ; options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Magic API",
        Description = "API Magic data",
    
    });


    // using System.Reflection;
    //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(options =>
    {
     
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
        options.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "API v1");
    });


}

app.UseAuthorization();

app.MapControllers();

app.Run();
