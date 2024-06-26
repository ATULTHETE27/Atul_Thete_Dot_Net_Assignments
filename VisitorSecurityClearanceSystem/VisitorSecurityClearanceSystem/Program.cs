using Microsoft.AspNetCore.Hosting;
using VisitorSecurityClearanceSystem.Common;
using VisitorSecurityClearanceSystem.CosmoDB;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Services;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<IOfficeService, OfficeService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
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
