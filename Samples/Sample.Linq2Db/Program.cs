using DALQueryChain.Linq2Db;
using LinqToDB.AspNet;
using LinqToDB.Configuration;
using ManualTest.Linq2Db.Context;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQueryChain(Assembly.GetExecutingAssembly());

builder.Services.AddLinqToDBContext<TestContext>((provider, options) =>
{
    options
    .UsePostgreSQL(builder.Configuration.GetConnectionString("Default")!);
});

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
