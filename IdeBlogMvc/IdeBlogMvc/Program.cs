using IdentityFrameworkWepApp.Data;
using Microsoft.EntityFrameworkCore;
using IdentityFrameworkWepApp.Extenisons;
using Microsoft.AspNetCore.Identity;
using IdentityFrameworkWepApp.Services;
using NToastNotify;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddNToastNotifyToastr(new ToastrOptions()
    {
        PositionClass = ToastPositions.TopRight,
        ProgressBar = true,
        TimeOut = 5000
    })
    ;

builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
});

// Lokasyon De�i�ikli�i Yap�ld�.
builder.Services.Configure<RazorViewEngineOptions>(opts =>
{
    opts.AreaViewLocationFormats.Add("/Areas/Shared/{0}.cshtml");
    opts.AreaViewLocationFormats.Add("/Areas/Admin/Views/{1}/{0}.cshtml");
});


builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan=TimeSpan.FromHours(1);
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(15); // kullan�c�n�n kimlik do�rulama i�lemi s�ras�nda kullan�lan g�venlik damgas�n�n ge�erlilik s�resini belirler.
});

builder.Services.AddIdentityWithExt();
builder.Services.ConfigureApplicationCookie(opts =>
{
    var cookieBuilder= new CookieBuilder();
    cookieBuilder.Name = "LoginCookie"; // Cookinin ad�n� olu�turduk 
    opts.LoginPath = new PathString("/Login/SignIn"); // Taray�c�n�n kimlik do�rulama yapmas� gerekti�i sayfan�n yolu, "Login/SignIn" olarak belirlenir. Kullan�c� do�rulanmad��� zaman, sistem otomatik olarak y�nlendirece�i sayfan�n adresidir.
    opts.AccessDeniedPath = new PathString("/Member/AccessDenied"); // Yetkisiz sayfaya giri� yapt���nda bu sayfaya y�nlendir.
    opts.Cookie=cookieBuilder;
    opts.ExpireTimeSpan=TimeSpan.FromDays(60); // Cookinin 60 g�n boyunca ge�erli olmas�n� sa�lar.
    opts.SlidingExpiration = true; // kullan�c�n�n belirli bir s�re boyunca i�lem yapmamas� durumunda �erezin s�resinin yeniden ba�lat�lmas�n� sa�lar.
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
