using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaEFCore.Data.Models
{
    /// <summary>
    /// Clase que representa un libro en la biblioteca.
    /// </summary>
    public class Libro
    {
        /// <summary>
        /// Identificador único del libro.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Título del libro.
        /// </summary>
        public string Titulo { get; set; } = null!;

        /// <summary>
        /// Año de publicación del libro.
        /// </summary>
        public int AnioPublicacion { get; set; }


        /// <summary>
        /// Identificador único del autor del libro.
        /// </summary>
        public int AutorId { get; set; }

        /// <summary>
        /// Autor del libro.
        /// </summary>
        [ForeignKey("AutorId")]
        public Autor Autor { get; set; } = null!;

        /// <summary>
        /// Lista de préstamos asociados al libro.
        /// </summary>
        public List<Prestamo> Prestamos { get; set; } = new();
    }
}
