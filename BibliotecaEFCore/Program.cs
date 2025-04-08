using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BibliotecaEFCore.Data;
using BibliotecaEFCore.Services;
using Spectre.Console;

namespace BibliotecaEFCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddDbContext<BibliotecaContext>(options => options.UseSqlite("Data Source=biblioteca.db"));

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

            while (true)
            {
                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]Seleccione una opción del menú:[/]")
                        .AddChoices(new[]
                        {
                                "1. Crear Autor",
                                "2. Crear Libro",
                                "3. Crear Usuario",
                                "4. Registrar Préstamo",
                                "5. Salir"
                        }));

                switch (option)
                {
                    case "1. Crear Autor":
                        CrearAutor(autorService);
                        break;

                    case "2. Crear Libro":
                        CrearLibro(libroService, autorService);
                        break;

                    case "3. Crear Usuario":
                        CrearUsuario(usuarioService);
                        break;

                    case "4. Registrar Préstamo":
                        RegistrarPrestamo(prestamoService, libroService, usuarioService);
                        break;

                    case "5. Salir":
                        AnsiConsole.MarkupLine("[yellow]Saliendo del programa...[/]");
                        return;

                    default:
                        AnsiConsole.MarkupLine("[red]Opción no válida.[/]");
                        break;
                }
            }
            await host.RunAsync();
        }

        static void CrearAutor(IAutorService autorService)
        {
            var nombre = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] del autor:");
            var apellido = AnsiConsole.Ask<string>("Ingrese el [green]apellido[/] del autor:");
            var autor = autorService.CrearAutor(nombre, apellido);
            AnsiConsole.MarkupLine($"[blue]Autor creado con éxito:[/] {autor.Nombres} {autor.Apellidos} (ID: {autor.Id})");
        }

        static void CrearLibro(ILibroService libroService, IAutorService autorService)
        {
            var titulo = AnsiConsole.Ask<string>("Ingrese el [green]título[/] del libro:");
            var anio = AnsiConsole.Ask<int>("Ingrese el [green]año de publicación[/] del libro:");

            var autor = autorService.ObtenerAutor(SeleccionarAutor(autorService).Value);

            if (autor == null)
            {
                AnsiConsole.MarkupLine("[red]Autor no encontrado.[/]");
                return;
            }

            var libro = libroService.CrearLibro(titulo, anio, autor.Id);
            AnsiConsole.MarkupLine($"[blue]Libro creado con éxito:[/] {libro.Titulo} (ID: {libro.Id})");
        }

        static void CrearUsuario(IUsuarioService usuarioService)
        {
            var nombre = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] del usuario:");
            var apellido = AnsiConsole.Ask<string>("Ingrese el [green]apellido[/] del usuario:");
            var usuario = usuarioService.CrearUsuario(nombre, apellido);
            AnsiConsole.MarkupLine($"[blue]Usuario creado con éxito:[/] {usuario.Nombres} {usuario.Apellidos} (ID: {usuario.Id})");
        }

        static void RegistrarPrestamo(IPrestamoService prestamoService, ILibroService libroService, IUsuarioService usuarioService)
        {
            var libro = libroService.ObtenerLibro(SeleccionarLibro(libroService).Value);

            if (libro == null)
            {
                AnsiConsole.MarkupLine("[red]Libro no encontrado.[/]");
                return;
            }

            var usuario = usuarioService.ObtenerUsuario(SeleccionarUsuario(usuarioService).Value);

            if (usuario == null)
            {
                AnsiConsole.MarkupLine("[red]Usuario no encontrado.[/]");
                return;
            }

            var prestamo = prestamoService.RegistrarPrestamo(libro.Id, usuario.Id);
            AnsiConsole.MarkupLine($"[blue]Préstamo registrado con éxito:[/] Libro '{libro.Titulo}' prestado a {usuario.Nombres} {usuario.Apellidos}.");
        }

        // Menus de Seleccion

        static int? SeleccionarAutor(IAutorService autorService)
        {
            var autores = autorService.ObtenerAutores();

            if (autores == null || !autores.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay autores disponibles.[/]");
                return null;
            }

            var autorSeleccionado = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Seleccione un [green]autor[/]:")
                    .PageSize(10)
                    .AddChoices(autores.Select(a => $"{a.Id}: {a.Nombres}, {a.Apellidos}")));

            // Extraer el ID del string seleccionado (formato "ID: Nombre")
            int id = int.Parse(autorSeleccionado.Split(':')[0]);
            return id;
        }

        static int? SeleccionarLibro(ILibroService libroService)
        {
            var libros = libroService.ObtenerLibros();

            if (libros == null || !libros.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay libros disponibles.[/]");
                return null;
            }

            var libroSeleccionado = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Seleccione un [green]libro[/]:")
                    .PageSize(10)
                    .AddChoices(libros.Select(l => $"{l.Id}: {l.Titulo}")));

            // Extraer el ID del string seleccionado (formato "ID: Título")
            int id = int.Parse(libroSeleccionado.Split(':')[0]);

            return id;
        }

        static int? SeleccionarUsuario(IUsuarioService usuarioService)
        {
            var usuarios = usuarioService.ObtenerUsuarios();
            if (usuarios == null || !usuarios.Any())
            {
                AnsiConsole.MarkupLine("[red]No hay usuarios disponibles.[/]");
                return null;
            }
            var usuarioSeleccionado = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Seleccione un [green]usuario[/]:")
                    .PageSize(10)
                    .AddChoices(usuarios.Select(u => $"{u.Id}: {u.Nombres}, {u.Apellidos}")));
            // Extraer el ID del string seleccionado (formato "ID: Nombre")
            int id = int.Parse(usuarioSeleccionado.Split(':')[0]);
            return id;
        }
    }
}
