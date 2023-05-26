using OrderHandlerMicroservice.Repositories;
using OrderHandlerMicroservice.Repositories.Extensions;
using OrderHandlerMicroservice.Repositories.Interfaces;
using OrderHandlerMicroservice.Services;
using Shed.CoreKit.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<OrderHandlerService>();

builder.Services.AddInfrastructure();
builder.Services.AddRepositories();

// builder.Services.AddWebApiEndpoints(new WebApiEndpoint<IProductCatalog>(new System.Uri("http://localhost:5001")));

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