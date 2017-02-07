using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class PlayerPlanetsRepository : IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Fetches a list of all planets that are attached to a specific player
        /// </summary>
        /// <param name="playerPlanet"></param>
        /// <returns></returns>
        public IQueryable<PlayerPlanets> GetAllByPlayerAndPlanet(PlayerPlanets playerPlanet)
        {
            var playerPlanets = new List<PlayerPlanets>();
            using (var ctx = new MyGameDBContext())
            {
                if (playerPlanet.PlanetId == null)
                {
                    playerPlanets = ctx.PlayerPlanets.Where(p => p.PlayerId == playerPlanet.PlayerId)
                    .ToList();
                }
                else
                {
                    playerPlanets = ctx.PlayerPlanets.Where(p => p.PlayerId == playerPlanet.PlayerId && p.PlanetId == playerPlanet.PlanetId)
                   .ToList();
                }

            }
            return playerPlanets.AsQueryable();
        }
        /// <summary>
        /// Removes a planet and all the buildings that are associated with that planet
        /// </summary>
        /// <param name="playerPlanets"></param>
        /// <returns></returns>
        public bool RemoveBuildingsAndPlanet(List<PlayerPlanets> playerPlanets)
        {
            string issues = "";
            for (int i = 0; i < playerPlanets.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerPlanets.FirstOrDefault(p => p.PlayerId == playerPlanets[i].PlayerId && p.PlanetId == playerPlanets[i].PlanetId);
                    if (obj != null)
                    {
                        try
                        {
                            List<PlayerBuildings> pbList = new List<PlayerBuildings>();
                            PlayerBuildings p = new PlayerBuildings();
                            p.PlanetId = playerPlanets[i].PlanetId;
                            p.PlayerId = playerPlanets[i].PlayerId;
                            p.Quantity = 1000000;
                            pbList.Add(p);
                            using (var repo = new PlayerBuildingsRepository())
                                repo.RemoveOrUpdate(pbList);
                            ctx.PlayerPlanets.Remove(obj);
                            ctx.SaveChanges();
                        }
                        catch
                        {
                            issues += $"Issues with removing object {i}. ";
                        }
                    }
                    else
                    {
                        issues += $"Could not find Item{i} in database";
                    }

                }

            }
            if (issues != "")
                return false;
            return true;
        }


        /// <summary>
        /// Adds an existing planet to a players planets
        /// </summary>
        /// <param name="playerPlanets"></param>
        /// <returns></returns>
        public bool AddPlanetToPlayer(List<PlayerPlanets> playerPlanets)
        {
            string issues = "";
            for (int i = 0; i < playerPlanets.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerPlanets.FirstOrDefault(p => p.PlayerId == playerPlanets[i].PlayerId && p.PlanetId == playerPlanets[i].PlanetId);

                    if (obj == null)
                    {
                        try
                        {
                            ctx.PlayerPlanets.Add(playerPlanets[i]);
                            ctx.SaveChanges();

                        }
                        catch
                        {
                            issues += $"Issues with adding object {i}. ";
                        }

                    }
                    else
                    {
                        issues += "That planet already exists in the PlayerPlanets table. ";
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
                var list = ctx.PlayerPlanets.Where(p => p.PlayerId == player.PlayerId).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        ctx.PlayerPlanets.Remove(list[i]);
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
