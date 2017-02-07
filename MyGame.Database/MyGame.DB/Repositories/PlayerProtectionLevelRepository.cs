using MyGame.DB.DB.Models;
using MyGame.DB.DB.Models.ProtectionLevel;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class PlayerProtectionLevelRepository : IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Fetches the ProtectionLevel of a player based on his ID.
        /// </summary>
        /// <param name="playerResearch"></param>
        /// <returns></returns>
        public PlayerProtectionLevel GetProtectionLevelByPlayer(PlayerProtectionLevel playerProtectionLevel)
        {
            using (var ctx = new MyGameDBContext())
            {
                playerProtectionLevel = ctx.PlayerProtectionLevel.FirstOrDefault(p => p.PlayerID == playerProtectionLevel.PlayerID);


            }
            return playerProtectionLevel;
        }

        /// <summary>
        ///  Alters a player protectionlevel to a different protectionlevel
        /// </summary>
        /// <param name="playerResearches"></param>
        /// <returns></returns>
        public bool UpdateProtectionLevel(PlayerProtectionLevel currentProtectonLevel, PlayerProtectionLevel desiredProtectionLevel)
        {
            string issues = "";

            using (var ctx = new MyGameDBContext())
            {
                try
                {
                    currentProtectonLevel = ctx.PlayerProtectionLevel.FirstOrDefault(p => p.PlayerID == currentProtectonLevel.PlayerID);
                    currentProtectonLevel.ProtectionLevelID = desiredProtectionLevel.ProtectionLevelID;
                    ctx.SaveChanges();
                }
                catch (Exception)
                {

                    issues += "Problem retrieving or updating the item in the database";
                }

            }
            if (issues != "")
                return false;
            return true;
        }

        /// <summary>
        /// Adds a protectionlevel to a player, and if the player for some reason has one, updates that one to what was sent in
        /// </summary>
        /// <param name="playerResearches"></param>
        /// <returns></returns>
        public bool Add(PlayerProtectionLevel playerProtectionLevel)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                var obj = ctx.PlayerProtectionLevel.FirstOrDefault(p => p.PlayerID == playerProtectionLevel.PlayerID);

                if (obj == null)
                {
                    try
                    {
                        ctx.PlayerProtectionLevel.Add(playerProtectionLevel);
                        ctx.SaveChanges();

                    }
                    catch
                    {
                        issues += $"Issues with adding player protectionlevel . ";
                    }

                }
                else
                {
                    try
                    {
                        obj.ProtectionLevelID = playerProtectionLevel.ProtectionLevelID;
                        ctx.SaveChanges();
                    }
                    catch (Exception)
                    {

                        issues += "Issues with updating the players protectionlevel";
                    }
                   
                }
            }
            if (issues != "")
                return false;
            return true;





        }
    }
}

