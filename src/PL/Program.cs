
using DAL.Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PL.Middlewares;
using DAL.DI;
using BLL.DependencyInjection;
using PL.Validators;
namespace PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            
            builder.Services.AddOpenApi();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerDtoValidator>();

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
