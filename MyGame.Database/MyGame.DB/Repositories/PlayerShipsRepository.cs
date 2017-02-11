using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    public class PlayerShipsRepository : IDisposable, IJuncRepository<List<PlayerShips>>
    {
        public void Dispose()
        {

        }

        /// <summary>
        /// Gets all ships a player has at home
        /// </summary>
        /// <param name="playership"></param>
        /// <returns></returns>
        public List<PlayerShips> GetAllShipsAPlayerHasAtHome(PlayerShips playerShip)
        {
            var playerShips = new List<PlayerShips>();
            using (var ctx = new MyGameDBContext())
            {
                playerShips = ctx.PlayerShips.Where(p => p.PlayerId == playerShip.PlayerId)
                .ToList();
            }
            return playerShips;
        }
        /// <summary>
        /// Returns the total amount of ships a player has both at home and out on missions
        /// </summary>
        /// <param name="playerShip"></param>
        /// <returns></returns>
        public List<PlayerShips> GetAllShipsByPlayer(PlayerShips playerShip)
        {
            var list = new List<PlayerShips>();
            var secondList = new List<Missions>();
            var thirdlist = new List<PlayerShips>();
            using (var ctx = new MyGameDBContext())
            {
                list = ctx.PlayerShips.Where(p => p.PlayerId == playerShip.PlayerId)
                .ToList();
                secondList = ctx.Missions.Where(m => m.PlayerRefId == playerShip.PlayerId).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    thirdlist = secondList[i].Ships.ToList();
                    var temp = thirdlist.FirstOrDefault(p => p.ShipId == list[i].ShipId);
                    list[i].Quantity += temp.Quantity;

                }

            }
            return list;
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
                    var obj = ctx.PlayerShips.FirstOrDefault(p => p.ShipId == playerships[i].ShipId && p.PlayerId == playerships[i].PlayerId);

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
                    var obj = ctx.PlayerShips.FirstOrDefault(p => p.ShipId == playerships[i].ShipId && p.PlayerId == playerships[i].PlayerId);

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
                var list = ctx.PlayerShips.Where(p => p.PlayerId == player.PlayerId).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    try
                    {
                        ctx.PlayerShips.Remove(list[i]);
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
