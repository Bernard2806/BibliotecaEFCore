using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaEFCore.Data.Models
{
    public class Usuario
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombres del usuario.
        /// </summary>
        public string Nombres { get; set; } = null!;

        /// <summary>
        /// Apellidos del usuario.
        /// </summary>
        public string Apellidos { get; set; } = null!;

        /// <summary>
        /// Lista de préstamos realizados por el usuario.
        /// </summary>
        public List<Prestamo> Prestamos { get; set; } = new();
    }
}
