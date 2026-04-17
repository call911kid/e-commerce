
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using PL.Middlewares;
using DAL.DI;
using BLL.DependencyInjection;
namespace PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            
            builder.Services.AddOpenApi();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("unable to get connection string");

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString)
            );

            builder.Services.AddDAL(connectionString);
            builder.Services.AddBLLServices();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }


            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
