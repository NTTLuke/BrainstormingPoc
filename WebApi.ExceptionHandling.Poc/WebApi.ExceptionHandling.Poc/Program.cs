using FluentValidation;
using WebApi.ExceptionHandling.Poc.ExternalServices;
using WebApi.ExceptionHandling.Poc.Models;
using WebApi.ExceptionHandling.Poc.Repositories;
using WebApi.ExceptionHandling.Poc.Services;
using WebApi.ExceptionHandling.Poc.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddScoped<IValidator<Customer>, CustomerValidator>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<CreateCustomerListener, CreateCustomerListener>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
