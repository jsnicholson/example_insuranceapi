using Api;
using Api.Exceptions;
using Api.Handler;
using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString(Constants.Config.INSURANCECONNECTION)));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
// disabled for now
/*builder.Services.AddControllers(config => {
    config.Filters.Add(new RawRequestHandler());
});*/

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

using(var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated(); // using EnsureCreated() for simplicity
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
