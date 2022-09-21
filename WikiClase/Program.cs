using Microsoft.EntityFrameworkCore;
using WikiClase.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// CREAR LA CONEXION A LA BASE DE DATOS
builder.Services.AddDbContext<AplicationDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Por el error del DateTime que no tiene Postgre
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
