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
    public class ResourceRepository : IRepository<Troops>, IDisposable
    {
        public void Dispose()
        {
            // Dispose runs after Using
        }
        public IQueryable<Resources> GetAll()
        {
            var resources = new List<Resources>();

            using (var ctx = new MyGameDBContext())
            {
                resources = ctx.Resources
                     .ToList();
            }
            return resources.AsQueryable();
        }
        public Resources GetById(Guid id)
        {
            Resources resource;
            using (var ctx = new MyGameDBContext())
            {
                resource = ctx.Resources.FirstOrDefault(r => r.ResourceId == id);
            }
            return resource;
        }
        public bool Delete(Resources resource)
        {
            return Delete(resource.ResourceId);
        }
        public bool Delete(Guid id)
        {
            using (var ctx = new MyGameDBContext())
            {
                var resource = ctx.Resources.FirstOrDefault(r => r.ResourceId == id);
                if (resource != null)
                {
                    ctx.Resources.Remove(resource);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }
        public void Update(Resources resource)
        {
            using (var ctx = new MyGameDBContext())
            {
                var item = ctx.Resources.FirstOrDefault(t => t.ResourceId == resource.ResourceId);
                if (item != null)
                {
                    item.ResourceName = resource.ResourceName;
                    ctx.SaveChanges();
                }
            }
        }
        public void Add(Resources resource)
        {
            try
            {
                using (var ctx = new MyGameDBContext())
                {
                    ctx.Resources.Add(resource);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}