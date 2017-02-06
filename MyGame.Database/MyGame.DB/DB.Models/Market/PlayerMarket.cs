using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models.Market
{
    class PlayerMarket
    {
        [Key, Column(Order = 0)]
        public int PlayerID { get; set; }


        [Key, Column(Order = 1)]
        public int SellingID { get; set; }

        [Key, Column(Order = 2)]
        public int BuyingID { get; set; }




        public virtual Players Player { get; set; }
        public virtual Buying Deal { get; set; }
        [Required]
        public DateTime Made { get; set; }
        [Required]
        public DateTime Expires { get; set; }
    }
}
