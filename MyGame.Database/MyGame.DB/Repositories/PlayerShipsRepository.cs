using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class PlayerShipsRepository : IDisposable, IJuncRepository<List<PlayerShips>>
    {
        public void Dispose()
        {

        }

        /// <summary>
        /// Gets all ships a player has at a certain location. If the location is left empty it gets all the ships a player has in all locations combined
        /// </summary>
        /// <param name="playership"></param>
        /// <returns></returns>
        public IQueryable<PlayerShips> GetAllByPlayerAndOrLocation(PlayerShips playership)
        {
            var playerShips = new List<PlayerShips>();
            using (var ctx = new MyGameDBContext())
            {
                if (playership.LocationId == null)
                {
                    playerShips = ctx.PlayerShips.Where(p => p.PlayerId == playership.PlayerId)
                    .ToList();
                }
                else
                {
                    playerShips = ctx.PlayerShips.Where(p => p.PlayerId == playership.PlayerId && p.LocationId == playership.LocationId)
                   .ToList();
                }

            }
            return playerShips.AsQueryable();
        }
        /// <summary>
        /// Removes a certain quantity of ships a player owns. If the quantity removed is greater than the quantity in total the ship is removed completely
        /// </summary>
        /// <param name="playerships"></param>
        /// <returns></returns>
        public bool RemoveOrUpdate(List<PlayerShips> playerships)
        {
            string issues = "";
            for (int i = 0; i < playerships.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerShips.FirstOrDefault(p => p.ShipId == playerships[i].ShipId && p.LocationId == playerships[i].LocationId && p.PlayerId == playerships[i].PlayerId);

                    if (obj.Quantity > playerships[i].Quantity)
                    {
                        try
                        {
                            obj.Quantity = obj.Quantity - playerships[i].Quantity;
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
                            ctx.PlayerShips.Remove(obj);
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
        /// Adds a certain ship to PlayerShips list, if the ship already exists, it adds the quantity provided ontop of the existing quantity
        /// </summary>
        /// <param name="playerships"></param>
        /// <returns></returns>

        public bool AddOrUpdate(List<PlayerShips> playerships)
        {
            string issues = "";
            for (int i = 0; i < playerships.Count; i++)
            {
                using (var ctx = new MyGameDBContext())
                {
                    var obj = ctx.PlayerShips.FirstOrDefault(p => p.ShipId == playerships[i].ShipId && p.LocationId == playerships[i].LocationId && p.PlayerId == playerships[i].PlayerId);

                    if (obj != null)
                    {


                        if (obj == null)
                        {
                            try
                            {
                                ctx.PlayerShips.Add(playerships[i]);
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
                                obj.Quantity = obj.Quantity + playerships[i].Quantity;
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
    }
}
