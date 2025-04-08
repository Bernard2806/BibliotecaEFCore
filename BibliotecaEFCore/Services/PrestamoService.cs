using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaEFCore.Data.Models;
using BibliotecaEFCore.Data;

namespace BibliotecaEFCore.Services
{
    public interface IPrestamoService
    {
        Prestamo RegistrarPrestamo(int libroId, int usuarioId);
    }

    public class PrestamoService : IPrestamoService
    {
        private readonly BibliotecaContext _context;
        public PrestamoService(BibliotecaContext context) => _context = context;

        public Prestamo RegistrarPrestamo(int libroId, int usuarioId)
        {
            var prestamo = new Prestamo
            {
                LibroId = libroId,
                UsuarioId = usuarioId,
                FechaPrestamo = DateTime.Now
            };

            _context.Prestamos.Add(prestamo);
            _context.SaveChanges();

            var libro = _context.Libros.Find(libroId);
            var usuario = _context.Usuarios.Find(usuarioId);
            Console.WriteLine($"Préstamo registrado: {usuario.Nombres} pidió \"{libro.Titulo}\" el {prestamo.FechaPrestamo:dd/MM/yyyy}");
            return prestamo;
        }
    }
}
