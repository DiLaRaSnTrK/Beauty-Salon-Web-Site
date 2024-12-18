using Microsoft.EntityFrameworkCore;
using WEB3.Data;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddControllers(); // API desteði ekleniyor

// Session yönetimini etkinleþtirme
builder.Services.AddDistributedMemoryCache(); // Session için gerekli
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
    options.Cookie.HttpOnly = true; // Güvenlik için
    options.Cookie.IsEssential = true; // Tarayýcý ayarlarýndan baðýmsýz
});

// Veritabaný baðlantýsýný yapýlandýr
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Oturum ve kimlik doðrulama middleware'leri
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // API route'larýný etkinleþtirme
app.MapDefaultControllerRoute(); // Varsayýlan route (MVC kullanýmý için)

// Session'ý kullanabilmek için bu middleware eklenmeli


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


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




