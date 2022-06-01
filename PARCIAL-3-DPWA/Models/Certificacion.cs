﻿using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models
{
    public partial class Certificacion
    {
        public Certificacion()
        {
            CertificacionByUsuarios = new HashSet<CertificacionByUsuario>();
        }

        public int IdCertificacion { get; set; }
        public string? Nombre { get; set; }
        public string? Institucion { get; set; }
        public string? Link { get; set; }
        public string? Descripcion { get; set; }
        public string? Obtivos { get; set; }

        public virtual ICollection<CertificacionByUsuario> CertificacionByUsuarios { get; set; }
    }
}
