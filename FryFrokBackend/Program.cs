using FryFrokBackend.DataBase;
using Microsoft.EntityFrameworkCore;
using FryFrokBackend.Model; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();

    if (!context.Register.Any(u => u.Email == "admin@gmail.com"))
    {
        context.Register.Add(new RegisterUser
        {
            Name = "Super Admin",
            Email = "admin@gmail.com",
            Password = "Admin@123", 
            Role = "Admin"
        });

        await context.SaveChangesAsync();
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
