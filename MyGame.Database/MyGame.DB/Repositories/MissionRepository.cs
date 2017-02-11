using MyGame.DB.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.DB.Repositories
{
   public class MissionRepository : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Missions> GetAllCurrentMissions()
        {
            List<Missions> list = new List<Missions>();
            using (var ctx = new MyGameDBContext())
            {
                list = ctx.Missions.Where(m => m.Landing < DateTime.Now).ToList();
            }
            return list;
        }
        /// <summary>
        /// Will remove all missions from database that are no longer active
        /// </summary>
        /// <param name="missions"></param>
        /// <returns></returns>
        public bool RemoveListOfMissions(List<Missions> missions)
        {
            bool remove = true;
            string issues = "";
            using (var ctx = new MyGameDBContext())
            {
                for (int i = 0; i < missions.Count; i++)
                {
                    // Code for handling the removal of Attacking missions
                    #region Attack
                    if (missions[i].MissionType.MissionTypeCategory == "Attack")
                    {
                        // Checks if the attack has landed yet
                        if (missions[i].Landing < DateTime.Now)
                        {
                            var ships = missions[i].Ships.ToList();
                            var troops = missions[i].Troops.ToList();
                            // Checks there are any troops or ships left alive after the attack, if there is, it does not remove the mission
                            for (int j = 0; i < ships.Count; j++)
                            {
                                if (ships[i].Quantity != 0)
                                    remove = false;
                            }
                            for (int j = 0; i < troops.Count; j++)
                            {
                                if (troops[i].Quantity != 0)
                                    remove = false;
                            }
                            // If there are no living ships or troops, it removes the mission from the database
                            // else it updates the returning and returned attributes.
                            if (remove)
                            {
                                try
                                {
                                    var obj = ctx.Missions.FirstOrDefault(m => m.MissionId == missions[i].MissionId);
                                    ctx.Missions.Remove(obj);
                                }
                                catch (Exception)
                                {

                                    issues += $"Something went wrong with removing item {i}. ";
                                }
                            }
                            else
                            {
                                try
                                {
                                    var obj = ctx.Missions.FirstOrDefault(m => m.MissionId == missions[i].MissionId);
                                    obj.Returning = DateTime.Now;
                                    obj.Returned = DateTime.Now.AddHours(1);
                                    ctx.SaveChanges();
                                }
                                catch (Exception)
                                {

                                    issues += $"Something went wrong with adding returning/returned datetimes or updating item {i}. ";
                                }
                            }

                        }
                    }
                    #endregion
                    // Code for handling the removal of Protection missions
                    #region Protect
                    else if (missions[i].MissionType.MissionTypeCategory == "Protect")
                    {
                        // Checks if the mission has returned home yet
                        if (missions[i].Returned < DateTime.Now)
                        {
                            var ships = missions[i].Ships.ToList();
                            var troops = missions[i].Troops.ToList();
                            // If the mission has returned home, it adds the ships and troops to the PlayerShips and Playertroops tables
                            // and removes the mission from the database
                            using (var repo = new PlayerShipsRepository())
                            {
                                repo.AddOrUpdate(ships);
                            }
                            using (var repo = new PlayerTroopsRepository())
                            {
                                repo.AddOrUpdate(troops);
                            }
                            try
                            {
                                ctx.Missions.Remove(missions[i]);
                                ctx.SaveChanges();
                            }
                            catch (Exception)
                            {
                                issues += $"Issues removing mission {i}";

                            }

                        }
                    }
                    #endregion
                }
            }
            if (issues == "")
                return true;
            return false;
        }
        public bool CreateOrUpdateAMission(Missions mission)
        {
            using (var ctx = new MyGameDBContext())
            {
                var obj = ctx.Missions.FirstOrDefault(m => m.MissionId == mission.MissionId);
                if (obj != null)
                {
                    obj.
                }
            }
                return true;
        }
    }
}
