﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaEFCore.Data.Models;
using BibliotecaEFCore.Data;

namespace BibliotecaEFCore.Services
{
    public interface IAutorService
    {
        Autor CrearAutor(string nombres, string apellidos);
        Autor? ObtenerAutor(int id);
        List<Autor> ObtenerAutores();
    }

    public class AutorService : IAutorService
    {
        private readonly BibliotecaContext _context;
        public AutorService(BibliotecaContext context) => _context = context;

        public Autor CrearAutor(string nombres, string apellidos)
        {
            var autor = new Autor { Nombres = nombres, Apellidos = apellidos };
            _context.Autores.Add(autor);
            _context.SaveChanges();
            Console.WriteLine($"Autor creado: {nombres}, {apellidos}");
            return autor;
        }

        public Autor? ObtenerAutor(int id) 
        {
            return _context.Autores.Find(id);
        }

        public List<Autor> ObtenerAutores()
        {
            return _context.Autores.ToList();
        }
    }
}