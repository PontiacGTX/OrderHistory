using Common.ServicesModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderHistory.Domain.Context;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ServicesBaseURL>(prov=>
{
    var config = prov.GetRequiredService<IConfiguration>();
    var routes = config.GetSection("ServicesUrl");
    return routes.Get<ServicesBaseURL>()!;
});
// Add services to the container
builder.Services.AddDbContext<DbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbCon"));
});
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
using(var ctx = app.Services.GetService<DBContext>())
{
    if (!ctx.Database.EnsureCreated())
    {

    }
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
