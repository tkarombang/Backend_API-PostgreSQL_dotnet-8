using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Builder;
using MyApp.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan konfigurasi untuk layanan database
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS memblokir request karena masalah CORS
// KONFIGURASI CORS 
builder.Services.AddCors(option => {
    option.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Tambahkan layanan controller dan Swagger
builder.Services.AddControllers(); // Penting untuk MapControllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Gunakan Swagger hanya dalam lingkungan pengembangan
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Tambahkan middleware routing untuk mendukung controller
app.UseRouting();

// Tambahkan middleware untuk memetakan endpoint controller


app.UseCors("AllowAll");


app.UseAuthorization();


app.MapControllers();



app.Run();
