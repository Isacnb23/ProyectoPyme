//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//}
//app.UseRouting();

//app.UseAuthorization();

//app.MapStaticAssets();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();


//app.Run();


using Pyme.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// 1) Lee la cadena
var conn = builder.Configuration.GetConnectionString("Contexto")
          ?? throw new InvalidOperationException("Falta ConnectionStrings:Contexto en appsettings.json");

// 2) Configura el valor por defecto (para new Contexto() en AD)
Contexto.DefaultConnectionString = conn;

// 3) Registra servicios (antes de Build)
builder.Services.AddControllersWithViews();
// (opcional si luego querés inyectarlo)
// builder.Services.AddScoped<Contexto>(_ => new Contexto(conn));

var app = builder.Build();

// pipeline...
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
