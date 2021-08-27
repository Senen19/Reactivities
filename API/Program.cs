using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        /*
        Al añadir esta función en el main, siempre que inicialicemos la aplicación, se creará la BBDD si no estuviere creada previamente
        */
        public static async Task Main(string[] args)
        {
            //almacenarmos esa función en la variable host. Con dicha función CONSTRUIMOS/MONTAMOS nuestra aplicación
            var host = CreateHostBuilder(args).Build();

            //usamos el using para almacenar en scope cualquier service que creemos en este Services en particular
            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try 
            {
                /*
                El context será del tipo DataContext y lo obtendremos como service porque en nuestro StartUp ya lo añadimos como Service
                lo que hacemos aqui                
                */
                //El GetRequiredService es una función para localizar los service y poder almacenar todos en el context
                var context = services.GetRequiredService<DataContext>();
                //Si no tenemos una BBDD y arrancamos nuestra aplicación, llegaremos a la siguiente línea de código e inicializaremos nuestra migración
                //y crearemos una BBDD si aun no lo habiamos hecho
                await context.Database.MigrateAsync();

                //cargamos nuestra información de nuestro seed en la BBDD usando la instancia de EF
                await Seed.SeedData(context); 
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error ocurred during migration");
            }

            //Esta función INICIA nuestra aplicación
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
