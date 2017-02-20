using MyGame.DB.DB.Models;
using MyGame.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame.MVCSite.BuissnessLogic
{
    public class AttackRelatedLogic
    {
        public bool CreateAnAttackMission(Players player, List<PlayerShips> attackingShips, List<PlayerTroops> attackingTroops, Location targetLocation)
        {
            Missions mission = new Missions();
            mission.LocationRefId = targetLocation.LocationId;
            mission.MissionId = Guid.NewGuid();
            mission.PlayerRefId = player.PlayerId;
            mission.Ships = attackingShips;
            mission.Troops = attackingTroops;
            using (var repo = new MissionTypesRepository())
            {
                var mt = repo.GetByName("Attack");
                mission.MissionTypeRefId = mt.MissionTypeId;
            }
            mission = SetAllDateTimesForMission(player, targetLocation, mission);
            using (var repo = new MissionRepository())
            {
                repo.CreateOrUpdateAMission(mission);
            }
            return true;

        }
        public Missions SetAllDateTimesForMission(Players player, Location targetLocation, Missions mission)
        {
            int minutes = 15;
            if (player.Location.GalaxyNumber != targetLocation.GalaxyNumber)
            {
                minutes += 15;
                int galaxyAddition = Math.Abs(player.Location.GalaxyNumber - targetLocation.GalaxyNumber);
                minutes += (galaxyAddition * 3);
            }
            else
            {
                minutes += 5;
                int clusterAddition = Math.Abs(player.Location.LocalCluster - targetLocation.LocalCluster);
                minutes += (clusterAddition * 1);
            }

            DateTime dt = DateTime.Now;
            dt.AddMinutes(minutes);
            mission.Landing = dt;
            mission.Returned = dt.AddMinutes(minutes);
            mission.Returning = null;
            mission.Started = DateTime.Now;
            return mission;

        }
        private bool DoesThePlayerHaveTheAmountOfShipsAndTroopsHeWantsToSend(List<PlayerShips> playerShips, List<PlayerTroops> playerTroops)
        {
            string issues = "";
            using (var repo = new PlayerShipsRepository())
            {
                var list = repo.GetAllShipsAPlayerHasAtHome(playerShips[0]);
                for (int i = 0; i < playerShips.Count; i++)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (playerShips[i].ShipId == list[j].ShipId)
                            if (!(playerShips[i].Quantity >= list[j].Quantity))
                                issues += $"Player is trying to send more of Ship Type: {playerShips[i].Ships.ShipName} than he owns or currently has at home.";
                    }
                }
            }
            using (var repo = new PlayerTroopsRepository())
            {
                var list = repo.GetAllTroopsAPlayerHasAtHome(playerTroops[0]);
                for (int i = 0; i < playerTroops.Count; i++)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (playerTroops[i].TroopId == list[j].TroopId)
                            if (!(playerTroops[i].Quantity >= list[j].Quantity))
                                issues += $"Player is trying to send more of Troop Type: {playerTroops[i].Troops.TroopName} than he owns or currently has at home.";
                    }
                }
            }
            if (issues == "")
                return true;
            return false;

        }
        private bool TakeTroopsAwayFromHomeAndApplyThemOnMission(List<PlayerShips> playerShips, List<PlayerTroops> playerTroops)
        {
            string issues = "";
            using (var repo = new PlayerShipsRepository())
            {
                if (!repo.RemoveOrUpdate(playerShips))
                    issues += "Problem updating playerships";
            }
            using (var repo = new PlayerTroopsRepository())
            {
                if (!repo.RemoveOrUpdate(playerTroops))
                    issues += "Problem updating playertroops";
            }
            if (issues == "")
                return true;
            return false;
        }

    }
}
