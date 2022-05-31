using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models
{
    public partial class UsuarioCertificacion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdCertificacion { get; set; }

        public virtual Certificacion IdCertificacionNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
