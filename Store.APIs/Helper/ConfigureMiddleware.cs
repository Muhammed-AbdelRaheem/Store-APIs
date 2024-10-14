using Store.APIs.Middlewares;
using Store.Repository.Data.Contexts;
using Store.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Store.Repository.Identity.Contexts;
using Store.Repository.Identity.DataSeed;
using Microsoft.AspNetCore.Identity;
using Store.Core.Entities.Identity;

namespace Store.APIs.Helper
{
    public static class ConfigureMiddleware
    {

        public static async Task<WebApplication> ConfigureMiddleWaresAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var service = scope.ServiceProvider;
            var context = service.GetRequiredService<StoreDbContext>();
            var Identitycontext = service.GetRequiredService<StoreIdentityDbContext>();
            var userManger = service.GetRequiredService<UserManager<AppUser>>();
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();


            try
            {
                await context.Database.MigrateAsync();
                await Identitycontext.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManger);

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "There Are Problems During Appling Migrations");
            }





            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStaticFiles();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                 app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();



            return app;
        }

    }
}
