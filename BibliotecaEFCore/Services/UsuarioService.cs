using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaEFCore.Data.Models;
using BibliotecaEFCore.Data;

namespace BibliotecaEFCore.Services
{
    public interface IUsuarioService
    {
        Usuario CrearUsuario(string nombre, string apellidos);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly BibliotecaContext _context;
        public UsuarioService(BibliotecaContext context) => _context = context;

        public Usuario CrearUsuario(string nombre, string apellidos)
        {
            var usuario = new Usuario { Nombres = nombre, Apellidos = apellidos };
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            Console.WriteLine($"Usuario registrado: {nombre}, {apellidos}");
            return usuario;
        }
    }
}
