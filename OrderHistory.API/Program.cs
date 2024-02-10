using Common.ServicesModels;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderHistory.Domain.Context;
using OrderHistory.Domain.QueryHandler;
using OrderHistory.Domain.Repository;
using OrderHistory.Domain.Repository.Contracts;
using OrderHistory.Infraestructure.Services;
using OrderHistory.Infraestructure.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ServicesBaseURL>(prov =>
{
    var config = prov.GetRequiredService<IConfiguration>();
    var routes = config.GetSection("ServicesUrl");
    var urls = routes.Get<ServicesBaseURL>();
    return urls!;
});
builder.Services.AddHttpClient("CommentsClient",client =>
{
    using (var svcProv = builder.Services.BuildServiceProvider())
    {
        var urls = svcProv.GetService<ServicesBaseURL>();
        client.BaseAddress = new Uri(urls.CommentsUrl);
    }
        
});

builder.Services.AddScoped<ICommentsServices,CommentsServices>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<DbFeed>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(GetLastMembersOrderQueryHandler)));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IOrderRepository).Assembly));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbCon"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
using (var ctx = scope.ServiceProvider.GetService<DBContext>())
{
    ctx.Database.EnsureCreated();
    if (ctx.Order.Count()==0)
    {
        var dbFeed =scope.ServiceProvider.GetService<DbFeed>();
        await dbFeed.FeedDB();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
