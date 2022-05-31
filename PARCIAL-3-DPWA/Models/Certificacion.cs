using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models
{
    public partial class Certificacion
    {
        public Certificacion()
        {
            UsuarioCertificacions = new HashSet<UsuarioCertificacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Institucion { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Objetivos { get; set; } = null!;
        public string LinkAccesso { get; set; } = null!;

        public virtual ICollection<UsuarioCertificacion> UsuarioCertificacions { get; set; }
    }
}
