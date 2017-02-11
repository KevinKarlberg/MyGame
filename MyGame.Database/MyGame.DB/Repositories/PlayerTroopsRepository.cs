using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
   public class PlayerTroopsRepository : IJuncRepository<List<PlayerTroops>>, IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Gets all troops a player has at home
        /// </summary>
        /// <param name="playership"></param>
        /// <returns></returns>
        public List<PlayerTroops> GetAllTroopsAPlayerHasAtHome(PlayerTroops playerTroop)
        {
            var playerTroops = new List<PlayerTroops>();
            using (var ctx = new MyGameDBContext())
            {
                playerTroops = ctx.PlayerTroops.Where(p => p.PlayerId == playerTroop.PlayerId)
                .ToList();
            }
            return playerTroops;
        }
        /// <summary>
        /// Returns the total amount of troops a player has both at home and out on missions
        /// </summary>
        /// <param name="playerTroop"></param>
        /// <returns></returns>
        public List<PlayerTroops> GetAllTroopsByPlayer(PlayerTroops playerTroop)
        {
            var list = new List<PlayerTroops>();
            var secondList = new List<Missions>();
            var thirdlist = new List<PlayerTroops>();
            using (var ctx = new MyGameDBContext())
            {
                list = ctx.PlayerTroops.Where(p => p.PlayerId == playerTroop.PlayerId)
                .ToList();
                secondList = ctx.Missions.Where(m => m.PlayerRefId == playerTroop.PlayerId).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    thirdlist = secondList[i].Troops.ToList();
                    var temp = thirdlist.FirstOrDefault(p => p.TroopId == list[i].TroopId);
                    list[i].Quantity += temp.Quantity;

                }

            }
            return list;
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
                    var obj = ctx.PlayerTroops.FirstOrDefault(p => p.PlayerId == playerTroops[i].PlayerId && p.TroopId == playerTroops[i].TroopId);

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
                    var obj = ctx.PlayerTroops.FirstOrDefault(p => p.TroopId == playerTroops[i].TroopId && p.PlayerId == playerTroops[i].PlayerId);

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
        public bool RemoveAllByPlayer(Players player)
        {
            string issues = "";

            using (var ctx = new MyGameDBContext())
            {
                var list = ctx.PlayerTroops.Where(p => p.PlayerId == player.PlayerId).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        ctx.PlayerTroops.Remove(list[i]);
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
