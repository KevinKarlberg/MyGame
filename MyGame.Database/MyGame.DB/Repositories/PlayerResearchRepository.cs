using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class PlayerResearchRepository : IJuncRepository<List<PlayerResearch>>, IDisposable
    {
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
        public bool RemoveOrUpdate(List<PlayerResearch> playerResearches)
        {
            string issues = "";
            for (int i = 0; i < playerResearches.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerBuildings.FirstOrDefault(p => p.PlayerId == playerResearches[i].PlayerId && p.BuildingId == playerResearches[i].BuildingId);
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


        public bool AddOrUpdate(List<PlayerBuildings> playerBuildings)
        {
            string issues = "";
            for (int i = 0; i < playerBuildings.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerBuildings.FirstOrDefault(p => p.BuildingId == playerBuildings[i].BuildingId && p.PlanetId == playerBuildings[i].PlanetId);

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

    }
}

