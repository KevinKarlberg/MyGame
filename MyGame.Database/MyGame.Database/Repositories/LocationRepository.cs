using MyGame.Database.DB.Models;
using MyGame.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame.Database.Repositories
{
    public class LocationRepository : IRepository<Location>
    {
        private MyGameDBContext ctx;
        public LocationRepository()
        {
            this.ctx = new MyGameDBContext();
        }
        public IQueryable<Location> GetAll()
        {
            throw new NotImplementedException();
        }
        public Location GetById(Guid id)
        {
            throw new NotImplementedException();
        }
        public bool Delete(Location location)
        {
            throw new NotImplementedException();
        }
        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        public void Update(Location location)
        {
            throw new NotImplementedException();
        }
        public void Add(Location location)
        {
            throw new NotImplementedException();
        }
    }
}