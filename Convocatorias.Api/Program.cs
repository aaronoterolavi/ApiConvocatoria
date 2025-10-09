using System.Text;
using Convocatorias.Application.Interfaces;
using Convocatorias.Application.Services;
using Convocatorias.Infrastructure.Repositories;
using Convocatorias.Infrastructure.Services;
using Convocatorias.Infrastructure.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders; // 👈 Importante para FileProvider

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Opcional, si usas cookies o autenticación con credenciales
    });
});

// -----------------------------------------------------------------------------
// Configuración de JWT
// -----------------------------------------------------------------------------
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// -----------------------------------------------------------------------------
// Inyección de dependencias
// -----------------------------------------------------------------------------
builder.Services.AddScoped<IUsuarioRepository>(sp =>
    new UsuarioRepository(
        builder.Configuration.GetConnectionString("DefaultConnection")!,
        builder.Configuration
    )
);

var storageProvider = builder.Configuration["Storage:Provider"];

if (storageProvider == "Ftp")
{
    builder.Services.AddScoped<IFileStorageService, FtpFileStorageService>();
}
else
{
    builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
}

builder.Services.AddScoped<IEmailService, EmailService>();

//Convocatoria
builder.Services.AddScoped<IConvocatoriaRepository, ConvocatoriaRepository>();
builder.Services.AddScoped<IConvocatoriaService, ConvocatoriaService>();

//  ConvocatoriaFase
builder.Services.AddScoped<IConvocatoriaFaseRepository, ConvocatoriaFaseRepository>();
builder.Services.AddScoped<ConvocatoriaFaseService>();

//  ArchivoConvocatoria
builder.Services.AddScoped<IArchivoConvocatoriaRepository, ArchivoConvocatoriaRepository>();
builder.Services.AddScoped<ArchivoConvocatoriaService>();

//
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<EstadoService>();

//
builder.Services.AddScoped<IFormatoRepository, FormatoRepository>();
builder.Services.AddScoped<FormatoService>();

//
builder.Services.AddScoped<ITipoConvocatoriaRepository, TipoConvocatoriaRepository>();
builder.Services.AddScoped<TipoConvocatoriaService>();

//
builder.Services.AddScoped<IUnidadZonalRepository, UnidadZonalRepository>();
builder.Services.AddScoped<UnidadZonalService>();

//
builder.Services.AddScoped<ITipoDocumentoRepository, TipoDocumentoRepository>();
builder.Services.AddScoped<TipoDocumentoService>();

builder.Services.AddScoped<IPostulanteRepository, PostulanteRepository>();
builder.Services.AddScoped<PostulanteService>();

builder.Services.AddScoped<IFormacionAcademicaRepository, FormacionAcademicaRepository>();
builder.Services.AddScoped<FormacionAcademicaService>();

builder.Services.AddScoped<IColegiaturaRepository, ColegiaturaRepository>();
builder.Services.AddScoped<ColegiaturaService>();

builder.Services.AddScoped<IExperienciaLaboralRepository, ExperienciaLaboralRepository>();
builder.Services.AddScoped<ExperienciaLaboralService>();

builder.Services.AddScoped<ICursoDiplomadoRepository, CursoDiplomadoRepository>();
builder.Services.AddScoped<CursoDiplomadoService>();

builder.Services.AddScoped<IIdiomaRepository, IdiomaRepository>();
builder.Services.AddScoped<IdiomaService>();

builder.Services.AddScoped<IOfimaticaNivelIntermedioRepository, OfimaticaNivelIntermedioRepository>();
builder.Services.AddScoped<OfimaticaNivelIntermedioService>();

// -----------------------------------------------------------------------------
// Controllers y Swagger
// -----------------------------------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Convocatorias API",
        Version = "v1"
    });

    // Definición de seguridad para JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT con el formato: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// -----------------------------------------------------------------------------
// Middleware
// -----------------------------------------------------------------------------
var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors("AllowAngularDevClient");

app.UseAuthentication();
app.UseAuthorization();

// -----------------------------------------------------------------------------
// Servir archivos estáticos desde D:\convocatorias
// -----------------------------------------------------------------------------
var localPath = builder.Configuration["LocalStorage:BasePath"];
var requestPath = builder.Configuration["LocalStorage:RequestPath"];

if (!string.IsNullOrEmpty(localPath) && !string.IsNullOrEmpty(requestPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(localPath),
        RequestPath = requestPath
    });
}

app.MapControllers();

app.Run();
