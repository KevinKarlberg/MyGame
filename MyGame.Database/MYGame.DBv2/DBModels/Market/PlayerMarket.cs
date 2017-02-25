using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYGame.DBv2.Models
{
   public class PlayerMarket
    {
        [Key, Column(Order = 0)]
        public Guid PlayerId { get; set; }

        [Key, Column(Order = 1)]
        public Guid SellingID { get; set; }

        [Key, Column(Order = 2)]
        public Guid BuyingID { get; set; }
        public virtual Players Player { get; set; }
        public virtual Buying Buyer { get; set; }
        public virtual Selling Seller { get; set; }
        [Required]
        public DateTime Made { get; set; }
        [Required]
        public DateTime Expires { get; set; }
    }
}
