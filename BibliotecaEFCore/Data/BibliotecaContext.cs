using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaEFCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaEFCore.Data
{
    public class BibliotecaContext : DbContext
    {
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=biblioteca.db");
        }
    }
}
