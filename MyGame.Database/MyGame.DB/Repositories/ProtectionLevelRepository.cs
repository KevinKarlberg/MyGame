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
    class ProtectionLevelRepository : IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Fetches a ProtectionLevel based on a Selling ID
        /// </summary>
        /// <param name="protectionLevel"></param>
        /// <returns></returns>
        public ProtectionLevel GetById(ProtectionLevel protectionLevel)
        {
            using (var ctx = new MyGameDBContext())
            {
                protectionLevel = ctx.ProtectionLevel.FirstOrDefault(p => p.ProtectionLevelID == protectionLevel.ProtectionLevelID);
            }
            return protectionLevel;
        }
        /// <summary>
        /// Fetches all the protectionlevels there are
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProtectionLevel> GetAll()
        {
            var list = new List<ProtectionLevel>();
            using (var ctx = new MyGameDBContext())
            {
                list = ctx.ProtectionLevel.ToList();
            }
            return list.AsQueryable();
        }
        /// <summary>
        /// Removes a protectionlevel by ID and returns true or false based on the success of the action
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool Remove(ProtectionLevel protectionLevel)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                protectionLevel = ctx.ProtectionLevel.FirstOrDefault(p => p.ProtectionLevelID == protectionLevel.ProtectionLevelID);
                try
                {
                    ctx.ProtectionLevel.Remove(protectionLevel);
                    ctx.SaveChanges();
                }
                catch
                {
                    issues += "There was an issue removing the object";
                }
            }
            if (issues != "")
                return false;
            return true;
        }


        /// <summary>
        /// Adds a ProtectionLevel by an ID and returns true or false based on the success of the action
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool Add(ProtectionLevel protectionLevel)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                try
                {

                    ctx.ProtectionLevel.Add(protectionLevel);
                    ctx.SaveChanges();
                }
                catch
                {
                    issues += "There was an issue adding the object";
                }
            }
            if (issues != "")
                return false;
            return true;
        }
        public bool Update(PlayerProtectionLevel playerProtectionLevel)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                try
                {
                    var temp = ctx.PlayerProtectionLevel.FirstOrDefault(m => m.PlayerID == playerProtectionLevel.PlayerID);
                    temp.ProtectionLevel = playerProtectionLevel.ProtectionLevel;
                    ctx.SaveChanges();
                }
                catch (Exception)
                {

                    issues += "Problem finding or updating protectionlevel";
                }
                
            }
            if (issues != "")
                return false;
            return true;
        }

    }
}