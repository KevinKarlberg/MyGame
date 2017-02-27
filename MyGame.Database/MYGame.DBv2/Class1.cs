using MYGame.DBv2.Models;
using MYGame.DBv2.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYGame.DBv2
{
    public class Class1
    {
        public void TestMethod()
        {
            MarketContent mc = new MarketContent();
            Players p = new Players();
            using (var uow = new UnitOfWork())
            {
                uow.PlayerRepository.Update(p);
                uow.MarketContentRepository.Update(mc);
                uow.Save();   
            }
        }
       
    }
}
