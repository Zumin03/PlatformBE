using InstrumentPlatform.Data;
using InstrumentPlatform.Core.Service;
using Microsoft.EntityFrameworkCore;
using InstrumentPlatform.Service;
using InstrumentPlatform.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<IInstrumentService, InstrumentService>();
builder.Services.AddScoped<IRepositoryService, RepositoryService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

var scope = app.Services.CreateScope();

// Check database and aplly migration if needed.
app.ApplyMigration();

// Reset instrument states in database to default values.
var detectionService = scope.ServiceProvider.GetRequiredService<IInstrumentService>();
await detectionService.DetectInstrumentsAsync();


app.Run();
