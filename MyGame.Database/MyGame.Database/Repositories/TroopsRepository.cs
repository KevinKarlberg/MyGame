using MyGame.Database.DB.Models;
using MyGame.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyGame.Database.Repositories
{
    public class TroopsRepository : IRepository<Troops>
    {
        private MyGameDBContext ctx;
        public TroopsRepository()
        {
            this.ctx = new MyGameDBContext();
        }
        public IQueryable<Troops> GetAll()
        {
             var troops = new List<Troops>();

            using (ctx)
            {
                troops = ctx.Troops
                     .Include(t => t.TroopID)
                     .Include(t => t.TroopName)
                     .ToList();
            }
            return troops.AsQueryable();
        }
        public Troops GetById(Guid id)
        {
            Troops troop;
            using (ctx)
            {
                troop = ctx.Troops.Where(b => b.TroopID == id)
                    .Include(t => t.TroopID)
                    .Include(t => t.TroopName)
                    .FirstOrDefault();
            }
            return troop;
        }
        public bool Delete(Troops troops)
        {
            return Delete(troops.TroopID);
        }
        public bool Delete(Guid id)
        {
            using (ctx)
            {
                var troops = ctx.Troops.Where(t => t.TroopID == id)
                       .Include(t => t.TroopID)
                       .Include(t => t.TroopName)
                       .FirstOrDefault();
                if (troops != null)
                {
                    ctx.Troops.Remove(troops);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public void Update(Troops troops)
        {
            using (ctx)
            {
                var item = ctx.Troops.Where(t => t.TroopID == troops.TroopID).FirstOrDefault();
                if (item != null)
                {
                    item.TroopName = troops.TroopName;
                }
            }
        }
        public void Add(Troops troops)
        {
            try
            {
                using (ctx)
                {
                    ctx.Troops.Add(troops);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}