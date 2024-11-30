using Microsoft.EntityFrameworkCore;
using MyApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan konfigurasi untuk layanan database
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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


 app.MapControllers();

app.Run();
