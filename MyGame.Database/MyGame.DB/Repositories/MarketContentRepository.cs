using MyGame.DB.DB.Models;
using MyGame.DB.DB.Models.Market;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class MarketContentRepository : IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Fetches a marketContent based on a MarketContent ID
        /// </summary>
        /// <param name="marketContent"></param>
        /// <returns></returns>
        public MarketContent GetMarketContentById(MarketContent marketContent)
        {
            using (var ctx = new MyGameDBContext())
            {
                marketContent = ctx.MarketContent.FirstOrDefault(m => m.MarketContentID == marketContent.MarketContentID);
            }
            return marketContent;
        }
        /// <summary>
        /// Removes a MarketContent by an ID and returns true or false based on the success of the action
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool Remove(MarketContent marketContent)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                marketContent = ctx.MarketContent.FirstOrDefault(m => m.MarketContentID == marketContent.MarketContentID);
                try
                {
                    ctx.MarketContent.Remove(marketContent);
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
        /// Adds a MarketContent by an ID and returns true or false based on the success of the action
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool Add(MarketContent marketContent)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                try
                {
                    ctx.MarketContent.Add(marketContent);
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