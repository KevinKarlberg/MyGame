using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
   public class PlayerResearchRepository : IJuncRepository<List<PlayerResearch>>, IDisposable
    {
        public void Dispose()
        {
            
        }
        /// <summary>
        /// Fetches the quantity of a specific research the player has. If research is left empty, it fetches all the researches a player has
        /// </summary>
        /// <param name="playerResearch"></param>
        /// <returns></returns>
        public IQueryable<PlayerResearch> GetAllByPlayerAndResearch(PlayerResearch playerResearch)
        {
            var playerResearches = new List<PlayerResearch>();
            using (var ctx = new MyGameDBContext())
            {
                if (playerResearch.ResearchId == null)
                {
                    playerResearches = ctx.PlayerResearch.Where(p => p.PlayerId == playerResearch.PlayerId)
                    .ToList();
                }
                else
                {
                    playerResearches = ctx.PlayerResearch.Where(p => p.PlayerId == playerResearch.PlayerId && p.ResearchId == playerResearch.ResearchId)
                   .ToList();
                }

            }
            return playerResearches.AsQueryable();
        }

        /// <summary>
        ///  Removes a certain quantity of research a player owns. If the quantity removed is greater than the quantity in total the research is removed completely
        /// </summary>
        /// <param name="playerResearches"></param>
        /// <returns></returns>
        public bool RemoveOrUpdate(List<PlayerResearch> playerResearches)
        {
            string issues = "";
            for (int i = 0; i < playerResearches.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerResearch.FirstOrDefault(p => p.PlayerId == playerResearches[i].PlayerId && p.ResearchId == playerResearches[i].ResearchId);
                    if (obj != null)
                    {
                        if (obj.Quantity > playerResearches[i].Quantity)
                        {
                            try
                            {
                                obj.Quantity = obj.Quantity - playerResearches[i].Quantity;
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
                                ctx.PlayerResearch.Remove(obj);
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
        /// Adds a certain research to PlayerResearch list, if the research already exists, it adds the quantity provided ontop of the existing quantity
        /// </summary>
        /// <param name="playerResearches"></param>
        /// <returns></returns>
        public bool AddOrUpdate(List<PlayerResearch> playerResearches)
        {
            string issues = "";
            for (int i = 0; i < playerResearches.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerResearch.FirstOrDefault(p => p.PlayerId == playerResearches[i].PlayerId && p.ResearchId == playerResearches[i].ResearchId);

                    if (obj == null)
                    {
                        try
                        {
                            ctx.PlayerResearch.Add(playerResearches[i]);
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
                            obj.Quantity = obj.Quantity + playerResearches[i].Quantity;
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
        /// <summary>
        /// Removes all ships by playerId
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool RemoveAllByPlayer(Players player)
        {
            string issues = "";

            using (var ctx = new MyGameDBContext())
            {
                var list = ctx.PlayerResearch.Where(p => p.PlayerId == player.PlayerId).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        ctx.PlayerResearch.Remove(list[i]);
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

