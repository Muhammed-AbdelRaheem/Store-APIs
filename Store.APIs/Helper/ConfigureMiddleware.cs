using Store.APIs.Middlewares;
using Store.Repository.Data.Contexts;
using Store.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Store.APIs.Helper
{
    public static class ConfigureMiddleware
    {

        public static async Task<WebApplication> ConfigureMiddleWaresAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var service = scope.ServiceProvider;
            var context = service.GetRequiredService<StoreDbContext>();
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();


            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);

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

            app.UseAuthorization();


            app.MapControllers();



            return app;
        }

    }
}
