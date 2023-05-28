using Interfaces;
using OrderHandlerMicroservice.Repositories;
using OrderHandlerMicroservice.Repositories.Extensions;
using OrderHandlerMicroservice.Repositories.Interfaces;
using OrderHandlerMicroservice.Services;
using Shed.CoreKit.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<OrderHandlerService>();

builder.Services.AddInfrastructure();
builder.Services.AddRepositories();

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

app.MigrateUp();

app.Run();