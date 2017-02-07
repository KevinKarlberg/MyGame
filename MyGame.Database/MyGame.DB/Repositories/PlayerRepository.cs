using MyGame.DB.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class PlayerRepository : IDisposable
    {
        public void Dispose()
        {
            
        }
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
                            repo.RemoveAllByPlayer(player); 
                        }
                        using (var repo = new PlayerTroopsRepository())
                        {
                            repo.RemoveAllByPlayer(player);
                        }
                        using (var repo = new PlayerResourcesRepository())
                        {
                            repo.RemoveAllByPlayer(player);
                        }
                        using (var repo = new PlayerShipsRepository())
                        {
                            repo.RemoveAllByPlayer(player);
                        }
                        using (var repo = new PlayerResearchRepository())
                        {
                            repo.RemoveAllByPlayer(player);
                        }
                        using (var repo = new PlayerMarketRepository())
                        {
                            repo.RemoveAllByPlayer(player);
                        }
                        using (var repo = new PlayerPlanetsRepository())
                        {
                            repo.RemoveAllByPlayer(player);
                        }
                        using (var repo = new PlayerProtectionLevelRepository())
                        {
                            repo.RemoveAllByPlayer(player);
                        }
                    }
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
    }
}
