using FluentValidation.AspNetCore;
using OrderHandlerMicroservice.MiddleWares;
using OrderHandlerMicroservice.Repositories.Extensions;
using OrderHandlerMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<OrderHandlerService>();

builder.Services.AddInfrastructure();
builder.Services.AddRepositories();

builder.Services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
    conf.AutomaticValidationEnabled = true;
});

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

app.UseMiddleware<ErrorMiddleware>();

app.Run();