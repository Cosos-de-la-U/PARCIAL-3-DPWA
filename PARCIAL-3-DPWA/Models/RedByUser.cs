using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models
{
    public partial class RedByUser
    {
        public int IdRedByUser { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdRed { get; set; }
        public string? Accesslink { get; set; }

        public virtual Red? IdRedNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
