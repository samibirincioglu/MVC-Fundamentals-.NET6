using Microsoft.EntityFrameworkCore;
using MVCFundamentals.Data;
using Microsoft.Extensions.DependencyInjection;
using MVCFundamentals.Models;


var builder = WebApplication.CreateBuilder(args);

//MVC mimarisini aktif ediyorum.
builder.Services.AddControllersWithViews();

//Dependency Injection konteynirina calisacagim sunucu programini ve connection stringimi kaydediyorum.
builder.Services.AddDbContext<MVCContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MVCFundamentals")));

var app = builder.Build();

//Data seeding kullanmak için ilgili servisi scope'a alýyorum
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
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//varsayilan URL rotasi 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
