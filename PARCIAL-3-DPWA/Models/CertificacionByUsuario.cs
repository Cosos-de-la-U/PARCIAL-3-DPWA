using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models
{
    public partial class CertificacionByUsuario
    {
        public int IdCertificacionByUsuario { get; set; }
        public int? IdCertificacion { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Certificacion? IdCertificacionNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
