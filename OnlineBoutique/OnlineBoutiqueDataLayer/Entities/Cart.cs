using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBoutiqueDataLayer.Entities
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int TotalPrice { get; set; }

        public virtual ICollection<CartItem>? Items { get; set; }
    }
}
