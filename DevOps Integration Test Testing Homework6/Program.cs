using DevOps_Integration_Test_Testing_Homework6.Data;
using DevOps_Integration_Test_Testing_Homework6.Repositories.Abstracts;
using DevOps_Integration_Test_Testing_Homework6.Repositories.Concretes;
using DevOps_Integration_Test_Testing_Homework6.Services.Abstracts;
using DevOps_Integration_Test_Testing_Homework6.Services.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DevOps_Integration_Test_Testing_Homework6
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();

            var connectionStrings = builder.Configuration.GetConnectionString("Connection");

            builder.Services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(connectionStrings);
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
        }
    }
}

