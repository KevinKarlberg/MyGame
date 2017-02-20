using MyGame.DB.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
    public class MissionTypesRepository : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public List<MissionTypes> GetAll()
        {
            var list = new List<MissionTypes>();
            using (var ctx = new MyGameDBContext())
            {
                list = ctx.MissionTypes.ToList();
            }
            return list;

        }
        public MissionTypes GetByName(string type)
        {
            var mt = new MissionTypes();
            using (var ctx = new MyGameDBContext())
            {
               mt = ctx.MissionTypes.FirstOrDefault(m => m.MissionTypeName == type);
            }
            return mt;
        }
    }
}
