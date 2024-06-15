
using Genial.Gestao.Online.Domain.Authorization;
using Genial.Gestao.Online.WebApp.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDataBaseConfiguration(builder.Configuration);
builder.Services.AddRepositorys();
builder.Services.AddServices();

//Configurando session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddIdentityCore<Usuario>(options => { });
builder.Services.AddScoped<IUserStore<Usuario>, UsuarioStore>();

builder.Services.AddAuthentication("cookies")
    .AddCookie("cookies", options =>
    options.LoginPath = "/Home/Login");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}


///Configure Globalization Culture
var cultures = new[] {
    new CultureInfo("pt-BR"),
    new CultureInfo("en-US")
};


app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR"),
    SupportedCultures = cultures
});

//app.UseHttpsRedirection();
app.UseForwardedHeaders();
app.UseDefaultFiles();
app.UseStaticFiles();

//Adiciona uso de session
app.UseSession();

app.UseRouting();
app.UseAuthorization();

//app.UseMiddleware<ExceptionHandlingMiddleware>();
//app.MapHub<NotificationHub>("/notificationHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
