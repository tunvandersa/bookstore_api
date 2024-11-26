var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
// Thêm dịch vụ Session vào DI container
builder.Services.AddDistributedMemoryCache();  // Đảm bảo bạn có bộ nhớ lưu trữ session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Thời gian hết hạn của session
    options.Cookie.HttpOnly = true;  // Đảm bảo cookie không thể truy cập từ Javascript
    options.Cookie.IsEssential = true;  // Đảm bảo cookie luôn được gửi
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.WithOrigins("https://localhost:7120")  // Địa chỉ của client (frontend)
               .AllowAnyHeader()
               .AllowCredentials()// Cho phép tất cả header
               .AllowAnyMethod());                  // Cho phép tất cả phương thức HTTP
});
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();
app.UseStaticFiles(); 

app.UseCors("AllowSpecificOrigin");
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
