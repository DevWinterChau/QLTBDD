﻿using ITShop.Logic;
using ITShop.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Configuration;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ITShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ITShopConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(options =>
 {
     options.Cookie.Name = "ITShop.Cookie";
     options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
     options.SlidingExpiration = true;
     options.LoginPath = "/Home/Login";
     options.LogoutPath = "/Home/Logout";
     options.AccessDeniedPath = "/Home/Forbidden";
 });
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.Cookie.Name = "ITShop.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(15);
});
builder.Services.AddTransient<IMailLogic, MailLogic>();
// Lấy thông tin cấu hình trong tập tin appsettings.json và gán vào đối tượng MailSettings
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
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
var options = new RewriteOptions()
    .AddRewrite(@"^SanPham/?trang=(\d+)", "tat-ca-san-pham/$1", skipRemainingRules: true)
    .AddRewrite(@"^SanPham/PhanLoai?tenLoai=(.*)/", "san-pham/$1", skipRemainingRules: true)
    .AddRewrite(@"^SanPham/PhanLoai?tenLoai=(.*)&trang=(\d+)", "sanpham/$1/$2", skipRemainingRules: true)
    .AddRewrite(@"^SanPham/ChiTiet?tenLoai=(.*)&tenSanPham=(.*)", "san-pham/$1/$2.html", skipRemainingRules: true)

    .AddRewrite(@"^BaiViet/?trang=(\d+)", "tat-ca-bai-viet/$1", skipRemainingRules: true)
    .AddRewrite(@"^BaiViet/ChuDe?tenChuDe=(.*)/", "bai-viet/$1", skipRemainingRules: true)
    .AddRewrite(@"^BaiViet/ChuDe?tenChuDe=(.*)&trang=(\d+)/", "bai-viet/$1/$2", skipRemainingRules: true)
    .AddRewrite(@"^BaiViet/ChiTiet?tenChuDe=(.*)&tieuDe=(.*)", "bai-viet/$1/$2.html", skipRemainingRules: true);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(name: "adminareas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();