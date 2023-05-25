using API.Extensions;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddAplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);






var app = builder.Build();







// p' excepcion carpeta Errors + carpeta Middleware
// Configure the HTTP request pipeline.
// DEBE IR EN LA PARTE DE MAS ARRIBA DEL pipeline
// este es para ocupar mi middleware de excepciones y no tener que poner try-catch por todos lados
app.UseMiddleware<ExceptionMiddleware>();








// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();





app.UseCors(builder => builder.AllowAnyHeader()
            .AllowAnyMethod().WithOrigins("http://localhost:4200"));



app.UseAuthentication();
app.UseAuthorization();




app.MapControllers();

app.Run();
