using Microsoft.EntityFrameworkCore;
using Proprette.Infrastructure;
using Proprette.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSqliteInfrastructure(builder.Configuration.GetConnectionString("SqliteString"));
builder.Services.AddMariaDbInfrastructure(builder.Configuration.GetConnectionString("MariaDbString"));
builder.Services.AddDomain();

//builder.Services.AddDbContext<PropretteDbContext>(
//    options => options.UseSqlite(builder.Configuration.GetConnectionString("SqliteString"), b => b.MigrationsAssembly("Service"))
//    );

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
