using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models.Market
{
   public class Buying
    {
        [Key, Column(Order = 0)]
        public int BuyingID { get; set; }

        public virtual MarketContent WantToBuy { get; set; }
    }
}
