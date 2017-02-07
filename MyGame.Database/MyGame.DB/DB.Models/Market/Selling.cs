﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.DB.Models.Market
{
   public class Selling
    {
        [Key, Column(Order = 0)]
        public int SellingID { get; set; }
        public Guid SellingMarketContentRefId { get; set; }
        [ForeignKey("MarketContentRefId")]
        public virtual MarketContent MarketContent { get; set; }

    }
}
