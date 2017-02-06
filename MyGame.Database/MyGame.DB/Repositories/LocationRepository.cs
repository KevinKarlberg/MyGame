using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyGame.DB.Repositories
{
    public class LocationRepository : IRepository<Location>, IDisposable
    {
        public void Dispose()
        {
            // Dispose runs after Using
        }
        public IQueryable<Location> GetAll()
        {
            var locations = new List<Location>();

            using (var ctx = new MyGameDBContext())
            {
                locations = ctx.Locations
                     .ToList();
            }
            return locations.AsQueryable();
        }
        public Location GetById(Guid id)
        {
            Location location;
            using (var ctx = new MyGameDBContext())
            {
                location = ctx.Locations.FirstOrDefault(l => l.LocationId == id);
            }
            return location;
        }
        public bool Delete(Location location)
        {
            return Delete(location.LocationId);
        }
        public bool Delete(Guid id)
        {
            using (var ctx = new MyGameDBContext())
            {
                var location = ctx.Locations.FirstOrDefault(l => l.LocationId == id);
                if (location != null)
                {
                    ctx.Locations.Remove(location);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public void Update(Location location)
        {
            using (var ctx = new MyGameDBContext())
            {
                var item = ctx.Locations.Where(b => b.LocationId == location.LocationId).FirstOrDefault();
                if (item != null)
                {
                    item.GalaxyNumber = location.GalaxyNumber;
                    item.LocalCluster = location.LocalCluster;
                    item.SystemNumber = location.SystemNumber;
                    ctx.SaveChanges();
                }
            }
        }
        public void Add(Location location)
        {
            try
            {
                using (var ctx = new MyGameDBContext())
                {
                    ctx.Locations.Add(location);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}