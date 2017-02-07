using MyGame.DB.DB.Models;
using MyGame.DB.DB.Models.Market;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class PlayerMarketRepository : IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Fetches a list of all auctions held by a player (both buying and selling)
        /// </summary>
        /// <param name="playerMarket"></param>
        /// <returns></returns>
        public IQueryable<PlayerMarket> GetAllByPlayer(PlayerMarket playerMarket)
        {
            var auctions = new List<PlayerMarket>();
            using (var ctx = new MyGameDBContext())
            {
                auctions = ctx.PlayerMarket.Where(p => p.PlayerId == playerMarket.PlayerId)
                .ToList();
            }


            return auctions.AsQueryable();
        }
        /// <summary>
        /// Removes one or more auctions held by a player (including removing the whole chain)
        /// </summary>
        /// <param name="auctions"></param>
        /// <returns></returns>
        public bool Remove(List<PlayerMarket> auctions)
        {
            string issues = "";
            for (int i = 0; i < auctions.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerMarket.FirstOrDefault(p => p.PlayerId == auctions[i].PlayerId);

                    if (obj != null)
                    {
                        try
                        {
                            Selling selling = new Selling();
                            Buying buying = new Buying();
                            selling.SellingID = auctions[i].SellingID;
                            buying.BuyingID = auctions[i].BuyingID;
                            using (var repo = new BuyingRepository())
                                repo.Remove(buying);
                            using (var repo = new SellingRepository())
                                repo.Remove(selling);
                            ctx.PlayerMarket.Remove(obj);
                            ctx.SaveChanges();
                        }
                        catch
                        {
                            issues += $"Issues with removing object {i}. ";
                        }
                    }
                }


            }
            if (issues != "")
                return false;
            return true;
        }


        /// <summary>
        /// Adds a certain resource to PlayerResources list, if the resource already exists, it adds the quantity provided ontop of the existing quantity
        /// </summary>
        /// <param name="auctions"></param>
        /// <returns></returns>
        public bool Add(List<PlayerMarket> auctions)
        {
            string issues = "";
            for (int i = 0; i < auctions.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerMarket.FirstOrDefault(p => p.PlayerId == auctions[i].PlayerId);
                        try
                        {
                            ctx.PlayerMarket.Add(auctions[i]);
                            ctx.SaveChanges();

                        }
                        catch
                        {
                            issues += $"Issues with object {i}. ";
                        }
                }

            }
            if (issues != "")
                return false;
            return true;
        }
        public bool RemoveAllByPlayer(Players player)
        {
            string issues = "";

            using (var ctx = new MyGameDBContext())
            {
                var list = ctx.PlayerMarket.Where(p => p.PlayerId == player.PlayerId).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        ctx.PlayerMarket.Remove(list[i]);
                    }
                    catch (Exception)
                    {

                        issues += $"Issues with removing obj {i}";
                    }

                }
            }
            if (issues == "")
                return false;
            return true;


        }

    }
}

