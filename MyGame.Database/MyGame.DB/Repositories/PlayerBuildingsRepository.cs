using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
   public class PlayerBuildingsRepository : IJuncRepository<List<PlayerBuildings>>, IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Fetches a list of all the buildings a certain player has on a certain planet, if planet ID is not provided it fetches all the buildings a player has in total
        /// </summary>
        /// <param name="playerBuilding"></param>
        /// <returns></returns>
        public List<PlayerBuildings> GetAllByPlayerAndPlanet(PlayerBuildings playerBuilding)
        {
            var playerBuildings = new List<PlayerBuildings>();
            using (var ctx = new MyGameDBContext())
            {
                if (playerBuilding.PlanetId == null)
                {
                    playerBuildings = ctx.PlayerBuildings.Where(p => p.PlayerId == playerBuilding.PlayerId)
                    .ToList();
                }
                else
                {
                    playerBuildings = ctx.PlayerBuildings.Where(p => p.PlayerId == playerBuilding.PlayerId && p.PlanetId == playerBuilding.PlanetId)
                   .ToList();
                }

            }
            return playerBuildings;
        }
        /// <summary>
        /// Removes a certain quantity of buildings a player owns. If the quantity removed is greater than the quantity in total the building is removed completely
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool RemoveOrUpdate(List<PlayerBuildings> playerBuildings)
        {
            string issues = "";
            for (int i = 0; i < playerBuildings.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerBuildings.FirstOrDefault(p => p.PlayerId == playerBuildings[i].PlayerId && p.BuildingId == playerBuildings[i].BuildingId && p.PlanetId == playerBuildings[i].PlanetId);
                    if (obj != null)
                    {
                        if (obj.Quantity > playerBuildings[i].Quantity)
                        {
                            try
                            {
                                obj.Quantity = obj.Quantity - playerBuildings[i].Quantity;
                                ctx.SaveChanges();
                            }
                            catch
                            {
                                issues += $"Issues with changing quantity on object {i}. ";
                            }

                        }
                        else
                        {
                            try
                            {
                                ctx.PlayerBuildings.Remove(obj);
                                ctx.SaveChanges();
                            }
                            catch
                            {
                                issues += $"Issues with removing object {i}. ";
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
        /// Adds a certain building to PlayerBuildings list, if the building already exists, it adds the quantity provided ontop of the existing quantity
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool AddOrUpdate(List<PlayerBuildings> playerBuildings)
        {
            string issues = "";
            for (int i = 0; i < playerBuildings.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerBuildings.FirstOrDefault(p => p.BuildingId == playerBuildings[i].BuildingId && p.PlanetId == playerBuildings[i].PlanetId && p.PlayerId == playerBuildings[i].PlayerId);

                    if (obj == null)
                    {
                        try
                        {
                            ctx.PlayerBuildings.Add(playerBuildings[i]);
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
                            obj.Quantity = obj.Quantity + playerBuildings[i].Quantity;
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
                var list  = ctx.PlayerBuildings.Where(p => p.PlayerId == player.PlayerId).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        ctx.PlayerBuildings.Remove(list[i]);
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

