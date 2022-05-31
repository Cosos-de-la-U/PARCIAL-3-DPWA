using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models
{
    public partial class Portafolio
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public string Resumen { get; set; } = null!;
        public string Responsabilidades { get; set; } = null!;
        public string Tecnologias { get; set; } = null!;

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
