using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Store.APIs.Errors;
using Store.Core.Entities.Identity;
using Store.Core.Mapping.Auth;
using Store.Core.Mapping.Basket;
using Store.Core.Mapping.Products;
using Store.Core.Repositories.Contract;
using Store.Core.Servecies.Contract;
using Store.Repository;
using Store.Repository.Data.Contexts;
using Store.Repository.Identity.Contexts;
using Store.Repository.Repositories;
using Store.Services.Servecies;
using System.Text;

namespace Store.APIs.Helper
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddBuiltInService();
            services.AddSwaggerService();
            services.AddDbContextService(configuration);
            services.AddUserDefinedService();
            services.AddAutoMapperService(configuration);
            services.ConfigureInvalidModelStateResponseService();
            services.AddRedisService(configuration);
            services.AddIdentityService();
            services.AddAuthService(configuration);

            return services;


        }

        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {

            services.AddControllers();
            return services;

        }

        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;

        }
        private static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<StoreDbContext>(Options => Options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<StoreIdentityDbContext>(Options => Options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            return services;

        }

        private static IServiceCollection AddUserDefinedService(this IServiceCollection services)
        {


            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();


            return services;

        }

        private static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new AddressProfile()));
            services.AddAutoMapper(M => M.AddProfile(typeof(BasketProfile)));

            return services;

        }


        private static IServiceCollection ConfigureInvalidModelStateResponseService(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {

                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                               .SelectMany(P => P.Value.Errors)
                                               .Select(E => E.ErrorMessage)
                                               .ToArray();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors

                    };

                    return new BadRequestObjectResult(response);
                };


            }
            );



            return services;

        }

        private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connection);
            });

            return services;

        }

        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {

            services.AddIdentity<AppUser, IdentityRole>()
                     .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;

        }

        private static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {

                    ValidateIssuer= true,
                    ValidIssuer = configuration["JWT:Issure"],
                    ValidateAudience=true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateLifetime=true,
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))



                };
            });

            return services;

        }


    }
}
