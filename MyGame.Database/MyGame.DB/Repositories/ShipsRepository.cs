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
    public class ShipsRepository : IRepository<Ships>, IDisposable
    {
        public void Dispose()
        {
            // Dispose runs after Using
        }
        public IQueryable<Ships> GetAll()
        {
            var ships = new List<Ships>();

            using (var ctx = new MyGameDBContext())
            {
                ships = ctx.Ships
                     .ToList();
            }
            return ships.AsQueryable();
        }
        public Ships GetById(Guid id)
        {
            Ships ship;
            using (var ctx = new MyGameDBContext())
            {
                ship = ctx.Ships.FirstOrDefault(t => t.ShipId == id);
            }
            return ship;
        }
        public bool Delete(Ships ship)
        {
            return Delete(ship.ShipId);
        }
        public bool Delete(Guid id)
        {
            using (var ctx = new MyGameDBContext())
            {
                var ship = ctx.Ships.FirstOrDefault(t => t.ShipId == id);
                if (ship != null)
                {
                    ctx.Ships.Remove(ship);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public void Update(Ships ship)
        {
            using (var ctx = new MyGameDBContext())
            {
                var item = ctx.Ships.FirstOrDefault(t => t.ShipId == ship.ShipId);
                if (item != null)
                {
                    item.ShipName = ship.ShipName;
                    ctx.SaveChanges();
                }
            }
        }
        public void Add(Ships ship)
        {
            try
            {
                using (var ctx = new MyGameDBContext())
                {
                    ctx.Ships.Add(ship);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}