using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaEFCore.Data.Models
{
    /// <summary>
    /// Clase que representa un autor de libros.
    /// </summary>
    public class Autor
    {
        /// <summary>
        /// Identificador único del autor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombres del autor.
        /// </summary>
        public string Nombres { get; set; } = null!;

        /// <summary>
        /// Apellidos del autor.
        /// </summary>
        public string Apellidos { get; set; } = null!;

        /// <summary>
        /// Lista de libros publicados por el autor.
        /// </summary>
        public List<Libro> LibrosPublicados { get; set; } = new();
    }
}
