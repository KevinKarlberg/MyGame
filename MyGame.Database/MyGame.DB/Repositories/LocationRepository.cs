﻿using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyGame.DB.Repositories
{
    public class LocationRepository : IRepository<Location>
    {
        public IQueryable<Location> GetAll()
        {
            var locations = new List<Location>();

            using (var ctx = new MyGameDBContext())
            {
                locations = ctx.Locations
                     .Include(l => l.GalaxyNumber)
                     .Include(l => l.LocalCluster)
                     .Include(l => l.SystemNumber)
                     .ToList();
            }
            return locations.AsQueryable();
        }
        public Location GetById(Guid id)
        {
            Location location;
            using (var ctx = new MyGameDBContext())
            {
                location = ctx.Locations.Where(b => b.LocationId == id)
                     .Include(l => l.GalaxyNumber)
                     .Include(l => l.LocalCluster)
                     .Include(l => l.SystemNumber)
                    .FirstOrDefault();
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
                var location = ctx.Locations.Where(b => b.LocationId == id)
                       .Include(l => l.GalaxyNumber)
                       .Include(l => l.LocalCluster)
                       .Include(l => l.SystemNumber)
                       .FirstOrDefault();
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