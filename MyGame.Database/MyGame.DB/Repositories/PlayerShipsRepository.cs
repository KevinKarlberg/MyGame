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
