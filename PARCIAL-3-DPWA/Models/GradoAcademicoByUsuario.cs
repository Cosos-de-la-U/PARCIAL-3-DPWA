using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models
{
    public partial class GradoAcademicoByUsuario
    {
        public int IdGradoAcademicoByUsuario { get; set; }
        public string? Profesion { get; set; }
        public string? Universidad { get; set; }
        public string? Objetivos { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
