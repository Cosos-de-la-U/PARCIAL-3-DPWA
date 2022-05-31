using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Portafolios = new HashSet<Portafolio>();
            UsuarioCertificacions = new HashSet<UsuarioCertificacion>();
        }

        public int Id { get; set; }
        public string? FotoUrl { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string? Introduccion { get; set; }

        public virtual ICollection<Portafolio> Portafolios { get; set; }
        public virtual ICollection<UsuarioCertificacion> UsuarioCertificacions { get; set; }
    }
}
