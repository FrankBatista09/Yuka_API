using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using YukaBLL.Contracts;
using YukaBLL.Services;
using YukaDAL.Context;
using YukaDAL.Entities;
using YukaDAL.Interfaces;
using YukaDAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();
builder.Services.AddScoped<ISizeRepository, SizeRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
builder.Services.AddScoped<ISizeCategoryRepository, SizeCategoryRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductVariantService, ProductVariantService>();
builder.Services.AddScoped<ISizeCategoryService, SizeCategoryService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<YukaContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Yuka_API") ?? Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTION"), sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(

                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }
        ));

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<YukaContext>()
    .AddDefaultTokenProviders()
    .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapIdentityApi<ApplicationUser>();  
app.MapControllers();

app.Run();
