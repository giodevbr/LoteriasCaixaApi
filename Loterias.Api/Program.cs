using Loterias.Core.Interfaces;
using Loterias.Core.Services;
using Loterias.Domain.Interfaces;
using Loterias.Domain.Services;
using Loterias.Infra.Data.Model;
using Loterias.Infra.Data.Repository.Interfaces;
using Loterias.Infra.Data.Repository.Repositories;
using Loterias.Infra.Data.Rest.Caixa.Interfaces;
using Loterias.Infra.Data.Rest.Caixa.Services;
using Loterias.Infra.Data.Rest.Ibge.Interfaces;
using Loterias.Infra.Data.Rest.Ibge.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("LoteriasConnection");

builder.Services.AddDbContext<AppDbContext>(
                 options => options.UseSqlServer(connectionString,
                 sqlServerOptions => sqlServerOptions.MigrationsAssembly("Loterias.Infra.Data")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateActor = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

builder.Services.AddScoped<IDomainNotification, DomainNotification>();

builder.Services.AddScoped<IBaseRepository<AppDbContext>, BaseRepository<AppDbContext>>();
builder.Services.AddScoped<IMunicipioRepository, MunicipioRepository>();
builder.Services.AddScoped<IUfRepository, UfRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ILotoFacilConsultaService, LotoFacilConsultaService>();
builder.Services.AddScoped<IIbgeConsultaService, IbgeConsultaService>();
builder.Services.AddScoped<IIbgeService, IbgeService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ILoginService, LoginService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dataContext.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();
