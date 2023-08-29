using CrudNet6.Datos;
using CrudNet6.Migrations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//aqui colocamos la politica de autenticacion
var politicaUsuariosAutenticados = new AuthorizationPolicyBuilder()
 .RequireAuthenticatedUser()
 .Build();

// Configuramos la cadena de conexion Cnn
builder.Services.AddDbContext<ApplicationDBContext>(opciones =>
opciones.UseSqlServer(builder.Configuration.GetConnectionString("Cnn")));

builder.Services.AddScoped<IRepositorioClientes, ClientesRepositorio>();
builder.Services.AddScoped<IRepositorioGrados, GradosRepositorio>();

// Add services to the container.
builder.Services.AddControllersWithViews(opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter(politicaUsuariosAutenticados));
});

// activamos los servicios para que el usuario se pueda logear
builder.Services.AddAuthentication();
// agregar los servicios de Indentity. las clases que van a representar nuestro
// sistema de usuarios.
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    //esto es para no requerir una cuenta confirmada
    // ejemplo si usamos true el usuario debera confirmar su cuenta
    // por ejemplo mandando un email.
    opciones.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();
// ya con esto tenemos activado Identity en nuestra aplicación

// para usar mis propias vistas y no usar las que me da el framework
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ExternalScheme,
 opciones =>
 {
     //usar mi propia vista
     opciones.LoginPath = "/usuarios/login";
     //para cuando al usuario se le ha negado el acceso
     opciones.AccessDeniedPath = "/usuarios/login";

 });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// colocamos el middleware. esto nos va a permitir obtener la data del usuario autenticado
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Login}/{id?}");

app.Run();
