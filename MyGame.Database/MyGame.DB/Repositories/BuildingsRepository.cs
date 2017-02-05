using MyGame.DB.DB.Models;
using MyGame.DB.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace MyGame.DB.Repositories
{
    public class BuildingsRepository : IRepository<Buildings>
    {
        public IQueryable<Buildings> GetAll()
        {
            var buildings = new List<Buildings>();

            using (var ctx = new MyGameDBContext())
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
            using (var ctx = new MyGameDBContext())
            {
                building = ctx.Buildings.Where(b => b.BuildingId == id)
                    .Include(b => b.BuildingId)
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
            using (var ctx = new MyGameDBContext())
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
            using (var ctx = new MyGameDBContext())
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
                using (var ctx = new MyGameDBContext())
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