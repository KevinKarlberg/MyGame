﻿using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class PlayerBuildingsRepository : IJuncRepository<List<PlayerBuildings>>, IDisposable
    {
        public void Dispose()
        {

        }
        public IQueryable<PlayerBuildings> GetAllByPlayerAndPlanet(PlayerBuildings playerBuilding)
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
            return playerBuildings.AsQueryable();
        }
        public bool RemoveOrUpdate(List<PlayerBuildings> playerBuildings)
        {
            string issues = "";
            for (int i = 0; i < playerBuildings.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerBuildings.FirstOrDefault(p => p.PlayerId == playerBuildings[i].PlayerId && p.BuildingId == playerBuildings[i].BuildingId);
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

