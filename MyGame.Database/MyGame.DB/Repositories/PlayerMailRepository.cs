using MyGame.DB.DB.Models;
using MyGame.DB.DB.Models.Mailfunction;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    class PlayerMailRepository : IDisposable
    {
        public void Dispose()
        {

        }
        /// <summary>
        /// Fetches a list of mails. If the Sending and Recieving playerid is the same, it fetches all mails sent by, and recieved by a single player. If only sender or reciver is filled in,
        /// it fetches only the one filled in (sending/recieving). If two different players are listed in sending and recieving, it fetches all mails both sent and recieved between those two
        /// players
        /// </summary>
        /// <param name="playerBuilding"></param>
        /// <returns></returns>
        public IQueryable<PlayerMail> GetAllMailsBySenderOrReciever(PlayerMail playerMail)
        {
            var playerMails = new List<PlayerMail>();
            using (var ctx = new MyGameDBContext())
            {
                if (playerMail.RecievingPlayerId == null)
                {
                    playerMails = ctx.PlayerMail.Where(p => p.SendingPlayerId == playerMail.SendingPlayerId)
                    .ToList();
                }
                else if (playerMail.SendingPlayerId == null)
                {
                    playerMails = ctx.PlayerMail.Where(p => p.RecievingPlayerId == playerMail.RecievingPlayerId)
                  .ToList();
                }
                else
                {
                        playerMails = ctx.PlayerMail.Where(p => p.SendingPlayerId == playerMail.SendingPlayerId)
                            .ToList();
                        playerMails = ctx.PlayerMail.Where(p => p.RecievingPlayerId == playerMail.RecievingPlayerId)
                            .ToList();
                }

            }
            return playerMails.AsQueryable();
        }
        /// <summary>
        /// Removes all mails recieved by a player (Cleaning their inbox) by their RecieverId.
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool RemoveAllMails(PlayerMail playerMail)
        {
                string issues = "";
                using (var ctx = new MyGameDBContext())
                {
                var obj = ctx.PlayerMail.Where(p => p.RecievingPlayerId == playerMail.RecievingPlayerId)
                .ToList();

                for (int i = 0; i < obj.Count; i++)
                {
                    try
                    {
                        ctx.PlayerMail.Remove(obj[i]);
                        ctx.SaveChanges();
                    }
                    catch
                    {
                        issues += $"Issues with removing object {i}. ";
                    }
                }
             
                }
            if (issues != "")
                return false;
            return true;
        }


        /// <summary>
        /// Adds a complete mail to the database, needs to be sent in with all attributes filled in
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool AddMail(PlayerMail playerMail)
        {
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                try
                {
                    ctx.PlayerMail.Add(playerMail);
                    ctx.SaveChanges();

                }
                catch
                {
                    issues += $"Issues with object {i}. ";
                }
            }

            if (issues != "")
                return false;
            return true;
        }

    }
}
