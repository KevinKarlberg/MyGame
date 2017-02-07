using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Data.Entity;
using MyGame.DB.DB.Models.Mailfunction;

namespace MyGame.DB.Repositories
{
    public class MailRepository : IRepository<Mail>, IDisposable
    {
        /// <summary>
        /// Fetches a list of all Mails ever sent, yikes!
        /// </summary>
        /// <returns></returns>
        public IQueryable<Mail> GetAll()
        {
            var mails = new List<Mail>();

            using (var ctx = new MyGameDBContext())
            {
                mails = ctx.Mails

                     .ToList();
            }
            return mails.AsQueryable();
        }
        public void Dispose()
        {
            // Dispose runs after Using
        }
        /// <summary>
        /// Finds  a mail by it's Guid and returns it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Mail GetById(Guid id)
        {
            Mail mail;
            using (var ctx = new MyGameDBContext())
            {
                mail = ctx.Mails.FirstOrDefault(b => b.MailID == id);
            }
            return mail;
        }
        /// <summary>
        /// Sends a Mail into a different method which deletes it, returns true or false depending on success
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public bool Delete(Mail mail)
        {
            return Delete(mail.MailID);
        }
        /// <summary>
        /// Deletes a mail if found by ID, returns true or false depending on success
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            using (var ctx = new MyGameDBContext())
            {
                var mail = ctx.Mails.FirstOrDefault(b => b.MailID == id);
                if (mail != null)
                {
                    ctx.Mails.Remove(mail);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }

        }
        /// <summary>
        /// Updates a mail with a new content message if the mail is found by ID
        /// </summary>
        /// <param name="building"></param>
        public void Update(Mail mail)
        {
            using (var ctx = new MyGameDBContext())
            {
                var item = ctx.Mails.FirstOrDefault(m => m.MailID == mail.MailID);
                if (item != null)
                {
                    item.MailContentMessage = mail.MailContentMessage;
                    ctx.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Adds a new mail if successful
        /// </summary>
        /// <param name="mail"></param>
        public void Add(Mail mail)
        {
            try
            {
                using (var ctx = new MyGameDBContext())
                {
                    ctx.Mails.Add(mail);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }


    }
}