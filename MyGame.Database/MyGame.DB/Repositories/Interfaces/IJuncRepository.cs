using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories.Interfaces
{
    public interface IJuncRepository<T> where T : class
    {
        bool RemoveOrUpdate(T entity);
        bool AddOrUpdate(T entity);
    }
}
