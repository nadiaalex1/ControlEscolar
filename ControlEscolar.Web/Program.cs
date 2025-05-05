using ControlEscolar.Services;
using ControlEscolar.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<AlumnoRepository>();
builder.Services.AddScoped<AlumnoService>();
builder.Services.AddScoped<MateriaService>();
builder.Services.AddScoped<MateriaRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAngular",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("PermitirAngular"); 

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


