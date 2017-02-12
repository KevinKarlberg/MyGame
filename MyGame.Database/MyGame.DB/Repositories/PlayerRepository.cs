using MyGame.DB.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    public class PlayerRepository : IDisposable
    {
        public void Dispose()
        {
            
        }
        public Guid GetAdminGuidForAdminMessages(string type)
        {
            var admin = new Players();
            using (var ctx = new MyGameDBContext())
            {
                if (type == "Attack")
                    admin = ctx.Players.FirstOrDefault(p => p.EmpireName == "War Council");
                else if(type == "War")
                    admin = ctx.Players.FirstOrDefault(p => p.EmpireName == "Local Cluster");
                else if (type == "Defense")
                    admin = ctx.Players.FirstOrDefault(p => p.EmpireName == "Defense Council");
            }
            return admin.PlayerId;
        }
        public Players GetPlayerByLocation(Location location)
        {
            var player = new Players();
            using (var ctx = new MyGameDBContext())
            {
               player = ctx.Players.FirstOrDefault(p => p.LocationRefId == location.LocationId);
            }
            return player;
        }
        public Players GetPlayerById(Guid playerId)
        {
            var player = new Players();

            using (var ctx = new MyGameDBContext())
            {
                player = ctx.Players.FirstOrDefault(p => p.PlayerId == playerId);
            }
            return player;
        }
        /// <summary>
        /// Removes a player and everything related to the player via PlayerId
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool RemoveAPlayer(Players player)
        {
            string issues = "";

            using (var ctx = new MyGameDBContext())
            {
                try
                {
                    var temp = ctx.Players.FirstOrDefault(p => p.PlayerId == player.PlayerId);
                    if (temp != null)
                    {
                        using (var repo = new PlayerBuildingsRepository())
                        {
                            try
                            {
                                repo.RemoveAllByPlayer(player);
                            }
                            catch (Exception)
                            {
                                issues += "Something went wrong in the PlayerBuildingsRepo ";
                                
                            }
                           
                        }
                        using (var repo = new PlayerTroopsRepository())
                        {
                            try
                            {
                                repo.RemoveAllByPlayer(player);
                            }
                            catch (Exception)
                            {
                                issues += "Something went wrong in the PlayerTroopsRepo ";

                            }
                        }
                        using (var repo = new PlayerResourcesRepository())
                        {
                            try
                            {
                                repo.RemoveAllByPlayer(player);
                            }
                            catch (Exception)
                            {
                                issues += "Something went wrong in the PlayerResourcesRepo ";

                            }
                        }
                        using (var repo = new PlayerShipsRepository())
                        {
                            try
                            {
                                repo.RemoveAllByPlayer(player);
                            }
                            catch (Exception)
                            {
                                issues += "Something went wrong in the PlayerShipsRepo ";

                            }
                        }
                        using (var repo = new PlayerResearchRepository())
                        {
                            try
                            {
                                repo.RemoveAllByPlayer(player);
                            }
                            catch (Exception)
                            {
                                issues += "Something went wrong in the PlayerResearchRepo ";

                            }
                        }
                        using (var repo = new PlayerMarketRepository())
                        {
                            try
                            {
                                repo.RemoveAllByPlayer(player);
                            }
                            catch (Exception)
                            {
                                issues += "Something went wrong in the PlayerMarketRepo ";

                            }
                        }
                        using (var repo = new PlayerPlanetsRepository())
                        {
                            try
                            {
                                repo.RemoveAllByPlayer(player);
                            }
                            catch (Exception)
                            {
                                issues += "Something went wrong in the PlayerPlanetsRepo ";

                            }
                        }
                        using (var repo = new PlayerProtectionLevelRepository())
                        {
                            try
                            {
                                repo.RemoveAllByPlayer(player);
                            }
                            catch (Exception)
                            {
                                issues += "Something went wrong in the PlayerProtectionLevelRepo ";

                            }
                        }
                        using (var repo = new PlayerMailRepository())
                        {
                            try
                            {
                                repo.RemoveAllMails(player);
                            }
                            catch (Exception)
                            {
                                issues += "Something went wrong in the PlayerMailRepo ";

                            }
                        }
                    }
                    ctx.Players.Remove(player);
                    ctx.SaveChanges();
                }
                catch (Exception)
                {

                    issues += "Was unable to find or remove the player from the database";
                }
                
                    
            }
            if (issues == "")
                return false;
            return true;
        }
        public bool AddPlayer(Players player)
        {
            string issues = "";

            using (var ctx = new MyGameDBContext())
            {
                var item = ctx.Players.FirstOrDefault(p => p.PlayerId == player.PlayerId);
                if (item == null)
                {
                    ctx.Players.Add(player);
                    ctx.SaveChanges();
                    return true;
                }
                else
                {
                    issues += "Player ID not unique, could not create new player";
                    return false;
                }
            }
        }
    }
}
