using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(opt => {
  opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

/****Hasta aqui se consideran los servicios */
var app = builder.Build();
/**** desde aqui se considera Middleware */

app.MapControllers();

app.Run();