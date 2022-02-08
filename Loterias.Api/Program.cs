using Loterias.Core.Interfaces;
using Loterias.Core.Services;
using Loterias.Infra.Data.Rest.Caixa.Interfaces;
using Loterias.Infra.Data.Rest.Caixa.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDomainNotification, DomainNotification>();
builder.Services.AddScoped<ILotoFacilConsultaService, LotoFacilConsultaService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
