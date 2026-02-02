using Microsoft.EntityFrameworkCore;
using PlatformTest.Data;
using PlatformTest.Service;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<IInstrumentService, InstrumentService>();
builder.Services.AddScoped<IRepositoryService, RepositoryService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//builder.Services.AddSingleton<IInstrumentService, InstrumentService>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//var detectionService = app.Services.GetRequiredService<IInstrumentDetectionService>();
//detectionService.DetectInstruments();

var scope = app.Services.CreateScope();

var detectionService = scope.ServiceProvider.GetRequiredService<IInstrumentService>();
await detectionService.DetectInstrumentsAsync();


app.Run();
