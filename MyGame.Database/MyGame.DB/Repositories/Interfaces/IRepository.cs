using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame.DB.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        bool Delete(T entity);
        bool Delete(Guid id);

    }
}