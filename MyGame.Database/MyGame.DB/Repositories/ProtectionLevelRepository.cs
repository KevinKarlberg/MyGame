using MyGame.DB.DB.Models;
using MyGame.DB.DB.Models.TheProtectionLevels;
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
        /// Fetches a Selling based on a Selling ID
        /// </summary>
        /// <param name="protectionLevel"></param>
        /// <returns></returns>
        public ProtectionLevel GetById(ProtectionLevel protectionLevel)
        {
            using (var ctx = new MyGameDBContext())
            {

                protectionLevel = ctx.ProtectionLevel.FirstOrDefault(p => p.ProtectionLevelId == protectionLevel.ProtectionLevelId);
            }
            return protectionLevel;
        }
        /// <summary>
        /// Removes a selling by an ID and returns true or false based on the success of the action. Also removes the marketcontent attached to the buying
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool Remove(Selling selling)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                selling = ctx.Selling.FirstOrDefault(m => m.SellingID == selling.SellingID);
                try
                {
                    MarketContent marketContent = new MarketContent();
                    marketContent.MarketContentID = selling.SellingMarketContentRefId;
                    using (var repo = new MarketContentRepository())
                        repo.Remove(marketContent);
                    ctx.Selling.Remove(selling);
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
        /// Adds a selling by an ID and returns true or false based on the success of the action
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool Add(Selling selling)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                try
                {

                    ctx.Selling.Add(selling);
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

    }
}