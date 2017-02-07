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
    class BuyingRepository : IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Fetches a buying based on a buying ID
        /// </summary>
        /// <param name="buying"></param>
        /// <returns></returns>
        public Buying GetById(Buying buying)
        {
            using (var ctx = new MyGameDBContext())
            {
                buying = ctx.Buying.FirstOrDefault(m => m.BuyingID == buying.BuyingID);
            }
            return buying;
        }
        /// <summary>
        /// Removes a buying by an ID and returns true or false based on the success of the action. Also removes the marketcontent attached to the buying
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool Remove(Buying buying)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                buying = ctx.Buying.FirstOrDefault(m => m.BuyingID == buying.BuyingID);
                try
                {
                    MarketContent marketContent = new MarketContent();
                    marketContent.MarketContentID = buying.BuyingMarketContentRefId;
                    using (var repo = new MarketContentRepository())
                        repo.Remove(marketContent);
                    ctx.Buying.Remove(buying);
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
        /// Adds a buying by an ID and returns true or false based on the success of the action
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool Add(Buying buying)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                try
                {
                    ctx.Buying.Add(buying);
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