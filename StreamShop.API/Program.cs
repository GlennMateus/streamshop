using StreamShop.API;
using StreamShop.API.Context;
using StreamShop.API.Interfaces;
using StreamShop.API.Repositories;
using StreamShop.API.Services.ProductImages;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IProductImagesServices), typeof(ProductImagesService));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IProductRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<SQLiteDbContext>();
builder.Services.AddCors(
    options => options.AddPolicy(
        "CorsPolicy",
        builder => builder
            .WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin()
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();