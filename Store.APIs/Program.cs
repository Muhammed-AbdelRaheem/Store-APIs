
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.APIs.Errors;
using Store.APIs.Middlewares;
using Store.Core.Mapping.Products;
using Store.Core.Repositories.Contract;
using Store.Core.Servecies.Contract;
using Store.Repository;
using Store.Repository.Data;
using Store.Repository.Data.Contexts;
using Store.Services.Servecies;
using Store.APIs.Helper;
namespace Store.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDependency(builder.Configuration) ;


            var app = builder.Build();


          await  app.ConfigureMiddleWaresAsync();

            app.Run();
        }
    }
}