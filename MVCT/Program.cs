using MVCT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCT.Data;
using MVCT.Services;
using DNTCaptcha.Core;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddDistributedMemoryCache();  // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
builder.Services.AddSession(cfg => {           // Đăng ký dịch vụ Session
    cfg.Cookie.Name = "Hoangtuan";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
    cfg.IdleTimeout = new TimeSpan(0, 30, 0);   // Thời gian tồn tại của Session
});

builder.Services.AddDNTCaptcha(options =>
{
    options.UseCookieStorageProvider()
    .ShowThousandsSeparators(false);
    options.WithEncryptionKey("sadsadasdsadsadsa");
});


builder.Services.AddSingleton<IEmailSender, SendMailService>();

builder.Services.Configure<IdentityOptions>(options => {
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = false;

    // Cấu hình về User.
    options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // Email là duy nhất

    // Cấu hình đăng nhập.
    options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại

});


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login/";
    options.LogoutPath = "/logout/";
    options.AccessDeniedPath = "/khong-duoc-truy-cap.html";
});
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();


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
app.UseCookiePolicy();
app.UseHttpsRedirection();
app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");


//app.MapAreaControllerRoute(
//    name: "identidy",
//    pattern: "{controller}/{action=Index}/{id?}",
//    areaName: "Identity"
//);
app.MapRazorPages();
app.Run();
