using MyGame.Database.DB.Models;
using MyGame.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace MyGame.Database.Repositories
{
    public class BuildingsRepository : IRepository<Buildings>
    {
        private MyGameDBContext ctx;
        public BuildingsRepository()
        {
            this.ctx = new MyGameDBContext();
        }
        public IQueryable<Buildings> GetAll()
        {
            var buildings = new List<Buildings>();

            using (ctx)
            {
                buildings = ctx.Buildings
                     .Include(b => b.BuildingId)
                     .Include(b => b.BuildingName)
                     .ToList();
            }
            return buildings.AsQueryable();
        }
        public Buildings GetById(Guid id)
        {
            Buildings building;
            using (ctx)
            {
                building = ctx.Buildings.Where(b => b.BuildingId == id)
                    .Include(b => b.BuildingName)
                    .FirstOrDefault();
            }
            return building;
        }
        public bool Delete(Buildings building)
        {
            return Delete(building.BuildingId);
        }
        public bool Delete(Guid id) 
        {
            using (ctx)
            {
                var building = ctx.Buildings.Where(b => b.BuildingId == id)
                       .Include(b => b.BuildingId)
                       .Include(b => b.BuildingName)
                       .FirstOrDefault();
                if (building != null)
                {
                    ctx.Buildings.Remove(building);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
            
        }
        public void Update(Buildings building)
        {
            using (ctx)
            {
                var item = ctx.Buildings.Where(b => b.BuildingId == building.BuildingId).FirstOrDefault();
                if (item != null)
                {
                    item.BuildingName = building.BuildingName;
                }
            }
        }
        public void Add(Buildings building)
        {
            try
            {
                using (ctx)
                {
                    ctx.Buildings.Add(building);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}