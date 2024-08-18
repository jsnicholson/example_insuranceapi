using Api;
using Api.Exceptions;
using Api.Handler;
using Api.Mapping;
using Data;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString(Constants.Config.INSURANCECONNECTION)));
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
builder.Services.AddTransient<GlobalExceptionHandler>();

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddControllers(config => {
    // disabled for now
    //config.Filters.Add(new RawRequestHandler());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.CustomSchemaIds(type => type.FullName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

using(var scope = app.Services.CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated(); // using EnsureCreated() for simplicity

    if(!dbContext.Companies.Any()) {
        dbContext.Companies.AddRange([
            new() { Id = 1, Name = "Google", Address1 = "6 Pancras Square", Address2 = "London", Postcode = "N1C 4AG", Country = "UK", Active = true, InsuranceEndDate = DateTime.Parse("2024-12-30") },
            new() { Id = 2, Name = "Amazon", Address1 = "2111 7th Avenue", Address2 = "Seattle", Postcode = "WA 98121", Country = "USA", Active = false, InsuranceEndDate = DateTime.Parse("2024-01-01") },
            new() { Id = 3, Name = "Apple", Address1 = "1 Apple Park Way", Address2 = "Cupertino",  Postcode = "CA 95014", Country = "USA", Active = true, InsuranceEndDate = DateTime.Parse("2024-11-01") }
        ]);

        dbContext.SaveChanges();
    }
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
