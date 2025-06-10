var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
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
=======
// Add services to the container.

>>>>>>> 562387df4dd7ea37d539639cc76279e7003b1b6b
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

<<<<<<< HEAD
// Tambahkan ini supaya CORS aktif
app.UseCors("AllowMyOrigin");

=======
>>>>>>> 562387df4dd7ea37d539639cc76279e7003b1b6b
app.UseAuthorization();

app.MapControllers();

app.Run();
