
using BusinessObjects.Models;
using DataAccess.DAOs;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using Repositories.Business;
using Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISachBusiness, SachBusiness>();
builder.Services.AddScoped<ISachDao, SachDao>();
builder.Services.AddScoped<ITaikhoanBusiness, TaikhoanBusiness>();
builder.Services.AddScoped<ITaiKhoanDao, TaiKhoanDao>();
builder.Services.AddScoped<ICartBusiness, CartBusiness>();
builder.Services.AddScoped<ICartDao, CartDao>();
builder.Services.AddDbContext<qlbsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("qlbs_db")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache(); // Cấu hình bộ nhớ cho session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session (30 phút)
    options.Cookie.HttpOnly = true; // Giới hạn truy cập cookie qua JavaScript
    options.Cookie.IsEssential = true; // Đảm bảo cookie tồn tại ngay cả khi không có sự đồng ý của người dùng
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
        builder.WithOrigins("https://localhost:7226")  // Địa chỉ của client (frontend)
               .AllowAnyHeader()
               .AllowCredentials()// Cho phép tất cả header
               .AllowAnyMethod());                  // Cho phép tất cả phương thức HTTP
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSession();
app.Run();
