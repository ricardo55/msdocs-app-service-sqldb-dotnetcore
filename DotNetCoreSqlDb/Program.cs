using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DotNetCoreSqlDb.Data;
using DotNetCoreSqlDb.Areas.Identity.Data;
using DotNetCoreSqlDb.Models;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add database context and cache con sql server
// builder.Services.AddDbContext<MyDatabaseContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnection")));
// builder.Services.AddDistributedMemoryCache();

// Add database context and cache con postgresql
// builder.Services.AddDbContext<LoginDBContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionStringPSQL")));
// builder.Services.AddDistributedMemoryCache();

// Add database context and cache con sqlite
// builder.Services.AddDbContext<MyDatabaseContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("LoginDBContextConnection")));
// builder.Services.AddDistributedMemoryCache();

// Add database context and cache con azure postgresql
builder.Services.AddDbContext<LoginDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AZURE_POSTGRESQL_CONNECTIONSTRING")));
builder.Services.AddStackExchangeRedisCache(options =>{
    options.Configuration = builder.Configuration["AZURE_REDIS_CONNECTIONSTRING"];
    options.InstanceName = "SampleInstance";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Es para autenticar con el login de Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<LoginDBContext>();

// Add App Service logging
builder.Logging.AddAzureWebAppDiagnostics();
builder.Services.AddRazorPages();

var app = builder.Build();

// Inicializar la base de datos con registros
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

app.UseDeveloperExceptionPage();
app.UseDatabaseErrorPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todos}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
