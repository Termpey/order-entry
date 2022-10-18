using Microsoft.EntityFrameworkCore;

using ClientService.Data;

var builder = WebApplication.CreateBuilder(args);
// Add secrets file not committed to repository
builder.Configuration.AddJsonFile("service-secrets.json");
// Add services to the container.
builder.Services.AddDbContext<ClientContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ClientServiceConnection")));
builder.Services.AddControllers();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
