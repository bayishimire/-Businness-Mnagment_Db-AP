using Microsoft.EntityFrameworkCore;
using Businness_Mnagment_Db_API.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configuration map controller
builder.Services.AddControllers();

//configuration connection to my sql
var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");

builder.Services.AddDbContext<AppDbContext>(options =>options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(difine=>difine.SerializeAsV2 = true);
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();



app.Run();