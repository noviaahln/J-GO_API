var builder = WebApplication.CreateBuilder(args);

// Tambahkan CORS di sini
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin", policy =>
    {
        policy.WithOrigins("*")  // ganti ini dengan alamat yang kamu mau izinkan
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Tambahkan ini supaya CORS aktif
app.UseCors("AllowMyOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
