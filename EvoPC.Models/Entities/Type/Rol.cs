using System;
using System.Collections.Generic;

#nullable disable

namespace EvoPC.Models.Entities
{
    public partial class Rol
    {
        public Rol()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string NumeRol { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
