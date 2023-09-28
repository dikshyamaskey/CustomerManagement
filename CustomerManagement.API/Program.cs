using CustomerManagement.API.Controllers;
using CustomerManagement.Application.Interface;
using CustomerManagement.Core.Entities;
using CustomerManagement.Infrastructure;
using CustomerManagement.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUnitofWorkFactory, UnitofWorkFactory>();
builder.Services.AddInfrastructureLayer(builder.Configuration);


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
