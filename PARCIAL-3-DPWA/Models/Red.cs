using System;
using System.Collections.Generic;

namespace PARCIAL_3_DPWA.Models
{
    public partial class Red
    {
        public Red()
        {
            RedByUsers = new HashSet<RedByUser>();
        }

        public int IdRed { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<RedByUser> RedByUsers { get; set; }
    }
}
