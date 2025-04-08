using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaEFCore
{
    public class Prestamo
    {
        /// <summary>
        /// Identificador único del préstamo.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Identificador único del libro prestado.
        /// </summary>
        public int LibroId { get; set; }

        /// <summary>
        /// Libro prestado.
        /// </summary>
        public Libro Libro { get; set; }


        /// <summary>
        /// Identificador único del usuario que realiza el préstamo.
        /// </summary>
        public int UsuarioId { get; set; }

        /// <summary>
        /// Usuario que realiza el préstamo.
        /// </summary>
        public Usuario Usuario { get; set; }


        /// <summary>
        /// Fecha en la que se realizó el préstamo.
        /// </summary>
        public DateTime FechaPrestamo { get; set; }

        /// <summary>
        /// Fecha en la que se debe devolver el libro.
        /// </summary>
        public DateTime? FechaDevolucion { get; set; }
    }
}