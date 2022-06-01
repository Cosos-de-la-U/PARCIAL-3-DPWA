using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            CertificacionByUsuarios = new HashSet<CertificacionByUsuario>();
            ExperienciaByUsuarios = new HashSet<ExperienciaByUsuario>();
            GradoAcademicoByUsuarios = new HashSet<GradoAcademicoByUsuario>();
            RedByUsers = new HashSet<RedByUser>();
        }

        public int IdUsuario { get; set; }
        public string? UName { get; set; }
        public string? Urlfoto { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Intro { get; set; }

        public virtual ICollection<CertificacionByUsuario> CertificacionByUsuarios { get; set; }
        public virtual ICollection<ExperienciaByUsuario> ExperienciaByUsuarios { get; set; }
        public virtual ICollection<GradoAcademicoByUsuario> GradoAcademicoByUsuarios { get; set; }
        public virtual ICollection<RedByUser> RedByUsers { get; set; }
    }
}
