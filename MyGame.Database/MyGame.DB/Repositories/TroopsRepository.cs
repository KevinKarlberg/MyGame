using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyGame.DB;

namespace MyGame.Database.Repositories
{
    public class TroopsRepository : IRepository<Troops>, IDisposable
    {
        public void Dispose()
        {
            // Dispose runs after Using
        }
        public IQueryable<Troops> GetAll()
        {
             var troops = new List<Troops>();

            using (var ctx = new MyGameDBContext())
            {
                troops = ctx.Troops
                     .ToList();
            }
            return troops.AsQueryable();
        }
        public Troops GetById(Guid id)
        {
            Troops troop;
            using (var ctx = new MyGameDBContext())
            {
                troop = ctx.Troops.FirstOrDefault(t => t.TroopID == id);
            }
            return troop;
        }
        public bool Delete(Troops troops)
        {
            return Delete(troops.TroopID);
        }
        public bool Delete(Guid id)
        {
            using (var ctx = new MyGameDBContext())
            {
                var troops = ctx.Troops.FirstOrDefault(t => t.TroopID == id);
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
            using (var ctx = new MyGameDBContext())
            {
                var item = ctx.Troops.FirstOrDefault(t => t.TroopID == troops.TroopID);
                if (item != null)
                {
                    item.TroopName = troops.TroopName;
                    ctx.SaveChanges();
                }
            }
        }
        public void Add(Troops troops)
        {
            try
            {
                using (var ctx = new MyGameDBContext())
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