using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WEB3.Data;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Giri� yap�lmad���nda y�nlendirilecek yol
        options.AccessDeniedPath = "/Account/AccessDenied"; // Yetkisiz eri�im i�in y�nlendirme
    });


// Di�er servisleri ekleyin
builder.Services.AddControllers();

builder.Services.AddHttpClient();


// Add services to the container.
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddControllers(); // API deste�i ekleniyor

// Session y�netimini etkinle�tirme
builder.Services.AddDistributedMemoryCache(); // Session i�in gerekli
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi
    options.Cookie.HttpOnly = true; // G�venlik i�in
    options.Cookie.IsEssential = true; // Taray�c� ayarlar�ndan ba��ms�z
});

// Veritaban� ba�lant�s�n� yap�land�r
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();


var app = builder.Build();
// Middleware'leri burada kullan�n
app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // API route'lar�n� etkinle�tirme
app.UseHttpsRedirection();
app.UseStaticFiles();


// Oturum ve kimlik do�rulama middleware'leri
app.UseSession();



app.MapDefaultControllerRoute(); // Varsay�lan route (MVC kullan�m� i�in)

// Session'� kullanabilmek i�in bu middleware eklenmeli


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());


// Middleware'leri ekleyin
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




