using System;
using System.Collections.Generic;

#nullable disable

namespace EvoPC.Models.Entities
{
    public partial class Feedback : IEntity<int> 
    {
        public int Id { get; set; }
        public string CommentTitle { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int ProcesorId { get; set; }
        public Guid? UserId { get; set; }

        public virtual Procesor Procesor { get; set; }
        public virtual User User { get; set; }
    }
}
