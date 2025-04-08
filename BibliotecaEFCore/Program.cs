using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BibliotecaEFCore.Data;
using BibliotecaEFCore.Services;

namespace BibliotecaEFCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddDbContext<BibliotecaContext>();
                    services.AddTransient<IAutorService, AutorService>();
                    services.AddTransient<ILibroService, LibroService>();
                    services.AddTransient<IUsuarioService, UsuarioService>();
                    services.AddTransient<IPrestamoService, PrestamoService>();

                    services.AddHostedService<InitData>();
                })
                .Build();

            using var scope = host.Services.CreateScope();

            var autorService = scope.ServiceProvider.GetRequiredService<IAutorService>();
            var libroService = scope.ServiceProvider.GetRequiredService<ILibroService>();
            var usuarioService = scope.ServiceProvider.GetRequiredService<IUsuarioService>();
            var prestamoService = scope.ServiceProvider.GetRequiredService<IPrestamoService>();

            var autor = autorService.CrearAutor("Gabriel", "García Márquez");
            var libro = libroService.CrearLibro("Cien Años de Soledad", 1967, autor.Id);
            var usuario = usuarioService.CrearUsuario("Juan", "Pérez");
            var prestamo = prestamoService.RegistrarPrestamo(libro.Id, usuario.Id);

            await host.RunAsync();
        }
    }
}
