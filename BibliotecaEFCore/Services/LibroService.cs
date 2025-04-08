using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaEFCore.Data;
using BibliotecaEFCore.Data.Models;

namespace BibliotecaEFCore.Services
{
    public interface ILibroService
    {
        Libro CrearLibro(string titulo, int anio, int autorId);
        List<Libro> ObtenerLibros();
        Libro? ObtenerLibro(int id);
    }

    public class LibroService : ILibroService
    {
        private readonly BibliotecaContext _context;
        public LibroService(BibliotecaContext context) => _context = context;

        public Libro CrearLibro(string titulo, int anio, int autorId)
        {
            var libro = new Libro { Titulo = titulo, AnioPublicacion = anio, AutorId = autorId };
            _context.Libros.Add(libro);
            _context.SaveChanges();
            Console.WriteLine($"Libro agregado: {titulo}");
            return libro;
        }

        public List<Libro> ObtenerLibros()
        {
            return _context.Libros.ToList();
        }

        public Libro? ObtenerLibro(int id) 
        {
            return _context.Libros.Find(id);
        }
    }
}
