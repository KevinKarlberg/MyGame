using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    public class PlayerPlanetsRepository : IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Fetches a list of all planets that are attached to a specific player
        /// </summary>
        /// <param name="playerPlanet"></param>
        /// <returns></returns>
        public List<PlayerPlanets> GetAllByPlayerAndPlanet(PlayerPlanets playerPlanet)
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
            return playerPlanets;
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
        /// <summary>
        /// Removs all planets that belong to a specific player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Updates all values of PlayerPlanets that are not null or 0
        /// </summary>
        /// <param name="playerPlanets"></param>
        /// <returns></returns>
        public bool AddOrUpdate(List<PlayerPlanets> playerPlanets)
        {
            string issues = "";
            for (int i = 0; i < playerPlanets.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerPlanets.FirstOrDefault(p => p.PlanetId == playerPlanets[i].PlanetId && p.PlayerId == playerPlanets[i].PlayerId);

                    try
                    {
                        if (playerPlanets[i].Planet.CurrentPopulation != null)
                            obj.Planet.CurrentPopulation = playerPlanets[i].Planet.CurrentPopulation;
                        if (playerPlanets[i].Planet.CurrentSize != 0)
                            obj.Planet.CurrentSize = playerPlanets[i].Planet.CurrentSize;
                        if (playerPlanets[i].Planet.Free != 0)
                            obj.Planet.Free = playerPlanets[i].Planet.Free;
                        if (playerPlanets[i].Planet.MaxPopulation != null)
                            obj.Planet.MaxPopulation = playerPlanets[i].Planet.MaxPopulation;
                        if (playerPlanets[i].Planet.MaxSize != 0)
                            obj.Planet.MaxSize = playerPlanets[i].Planet.MaxSize;
                        if (playerPlanets[i].Planet.Occupied != 0)
                            obj.Planet.Occupied = playerPlanets[i].Planet.Occupied;
                        if (playerPlanets[i].Planet.PlanetName != null)
                            obj.Planet.PlanetName = playerPlanets[i].Planet.PlanetName;
                        if (playerPlanets[i].Planet.TerrainRefId != null)
                            obj.Planet.TerrainRefId = playerPlanets[i].Planet.TerrainRefId;
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

    }
}
