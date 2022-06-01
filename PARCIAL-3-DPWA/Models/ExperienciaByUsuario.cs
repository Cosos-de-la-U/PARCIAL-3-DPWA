using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models
{
    public partial class ExperienciaByUsuario
    {
        public int IdExperienciaByUsuario { get; set; }
        public string? NombreProyecto { get; set; }
        public string? Rol { get; set; }
        public string? Resumen { get; set; }
        public string? Responsabilidades { get; set; }
        public string? Tecnologias { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
