using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class PlayerTroopsRepository : IJuncRepository<List<PlayerTroops>>, IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Fetches a list of all the troops a certain player has on a certain location, if location ID is not provided it fetches all the troops a player has in total
        /// </summary>
        /// <param name="playerTroop"></param>
        /// <returns></returns>
        public IQueryable<PlayerTroops> GetAllByPlayerAndOrLocation(PlayerTroops playerTroop)
        {
            var playerTroops = new List<PlayerTroops>();
            using (var ctx = new MyGameDBContext())
            {
                if (playerTroop.LocationId == null)
                {
                    playerTroops = ctx.PlayerTroops.Where(p => p.PlayerId == playerTroop.PlayerId)
                    .ToList();
                }
                else
                {
                    playerTroops = ctx.PlayerTroops.Where(p => p.PlayerId == playerTroop.PlayerId && p.TroopId  == playerTroop.TroopId)
                   .ToList();
                }

            }
            return playerTroops.AsQueryable();
        }
        /// <summary>
        /// Removes a certain quantity of troops a player owns. If the quantity removed is greater than the quantity in total the troop is removed completely
        /// </summary>
        /// <param name="playerTroops"></param>
        /// <returns></returns>
        public bool RemoveOrUpdate(List<PlayerTroops> playerTroops)
        {
            string issues = "";
            for (int i = 0; i < playerTroops.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerTroops.FirstOrDefault(p => p.PlayerId == playerTroops[i].PlayerId && p.TroopId == playerTroops[i].TroopId && p.PlayerId == playerTroops[i].PlayerId);

                    if (obj != null)
                    {
                        if (obj.Quantity > playerTroops[i].Quantity)
                        {
                            try
                            {
                                obj.Quantity = obj.Quantity - playerTroops[i].Quantity;
                                ctx.SaveChanges();
                            }
                            catch
                            {
                                issues += $"Issues with object {i}. ";
                            }

                        }
                        else
                        {
                            try
                            {
                                ctx.PlayerTroops.Remove(obj);
                                ctx.SaveChanges();
                            }
                            catch
                            {
                                issues += $"Issues with object {i}. ";
                            }


                        }

                    }
                    else
                        issues += $"Could not find Item{i} in database";

                }

            }
            if (issues != "")
                return false;
            return true;
        }
        /// <summary>
        /// Adds a certain troop to PlayerTroops list, if the troop already exists, it adds the quantity provided ontop of the existing quantity
        /// </summary>
        /// <param name="playerTroops"></param>
        /// <returns></returns>
        public bool AddOrUpdate(List<PlayerTroops> playerTroops)
        {
            string issues = "";
            for (int i = 0; i < playerTroops.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerTroops.FirstOrDefault(p => p.TroopId == playerTroops[i].TroopId && p.LocationId == playerTroops[i].LocationId && p.PlayerId == playerTroops[i].PlayerId);

                    if (obj == null)
                    {
                        try
                        {
                            ctx.PlayerTroops.Add(playerTroops[i]);
                            ctx.SaveChanges();

                        }
                        catch
                        {
                            issues += $"Issues with object {i}. ";
                        }

                    }
                    else
                    {
                        try
                        {
                            obj.Quantity = obj.Quantity + playerTroops[i].Quantity;
                            ctx.SaveChanges();
                        }
                        catch
                        {
                            issues += $"Issues with object {i}. ";
                        }


                    }
                }

            }
            if (issues != "")
                return false;
            return true;
        }

    }
}
