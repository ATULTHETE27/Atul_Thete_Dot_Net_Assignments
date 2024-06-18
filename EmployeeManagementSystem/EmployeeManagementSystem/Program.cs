using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.CosmoDB;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;
using EmployeeManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeAdditionalDetailsService, EmployeeAdditionalDetailsService>();
builder.Services.AddScoped<IEmployeeBasicDetailsService, EmployeeBasicDetailsService>();
builder.Services.AddScoped<ICosmoDBService, CosmoDBService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
