using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class PlayerResourcesRepository : IJuncRepository<List<PlayerResources>>, IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Fetches a list of all the resources a player currently owns
        /// </summary>
        /// <param name="playerResource"></param>
        /// <returns></returns>
        public IQueryable<PlayerResources> GetAllByPlayer(PlayerResources playerResource)
        {
            var playerResources = new List<PlayerResources>();
            using (var ctx = new MyGameDBContext())
            {
                playerResources = ctx.PlayerResources.Where(p => p.PlayerId == playerResource.PlayerId)
                .ToList();
            }

            
            return playerResources.AsQueryable();
        }
        /// <summary>
        /// Removes a certain quantity of resources a player owns. If the quantity removed is greater than the quantity in total the resource is removed completely
        /// </summary>
        /// <param name="playerResources"></param>
        /// <returns></returns>
        public bool RemoveOrUpdate(List<PlayerResources> playerResources)
        {
            string issues = "";
            for (int i = 0; i < playerResources.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerResources.FirstOrDefault(p => p.PlayerId == playerResources[i].PlayerId && p.ResourcesId == playerResources[i].ResourcesId);
                    if (obj != null)
                    {
                        if (obj.Quantity > playerResources[i].Quantity)
                        {
                            try
                            {
                                obj.Quantity = obj.Quantity - playerResources[i].Quantity;
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
                                ctx.PlayerResources.Remove(obj);
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
        /// Adds a certain resource to PlayerResources list, if the resource already exists, it adds the quantity provided ontop of the existing quantity
        /// </summary>
        /// <param name="playerResources"></param>
        /// <returns></returns>
        public bool AddOrUpdate(List<PlayerResources> playerResources)
        {
            string issues = "";
            for (int i = 0; i < playerResources.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerResources.FirstOrDefault(p => p.ResourcesId == playerResources[i].ResourcesId && p.PlayerId == playerResources[i].PlayerId);

                    if (obj == null)
                    {
                        try
                        {
                            ctx.PlayerResources.Add(playerResources[i]);
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
                            obj.Quantity = obj.Quantity + playerResources[i].Quantity;
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

