using MyGame.DB.DB.Models;
using MyGame.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.UpdateProgram.Updates
{
    class Update
    {
        public bool RegularUpdate()
        {
            return true;
        }
        private bool HandleMissions()
        {
            using (var repo = new MissionRepository())
            {
                // Getting and handling all attackmissions that happened since the last updatecycle
                var list = repo.GetAllCurrentMissions();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].MissionType.MissionTypeCategory == "Attack")
                    {
                        if (!HandleAttackMission(list[i]))
                            return false;
                    }
                    else if (list[i].MissionType.MissionTypeCategory == "Protect")
                    {
                        if (!HandleProtectionMission(list[i]))
                            return false;
                    }
                }
                if (!repo.RemoveListOfMissions(list))
                    return false;
            }
            return true;
        }
        private bool HandleAttackMission(Missions mission)
        {
            #region OrbitalDefense
            // Declaring the two players to be able to acess them throughout the method
            var attackingPlayer = new Players();
            var defendingPlayer = new Players();

            // Randomizer that will be used during the method
            Random rnd = new Random();

            // This is where attack/defense value will be saved, with attack in 0, health in 1, and armor in 2
            // Also declaring modifying amount that will be manipulated by research and racialtraits
            // Also total size of all owned planets
            int[] defenderStats = new int[3];
            int[] attackerStats = new int[3];
            int[] modifierAmount = new int[3];
            int totalSizeOfSolarSystem = 0;

            // Declaring the list of both players ships
            var defendingPlayerShips = new List<PlayerShips>();
            var attackingPlayerShips = mission.Ships.ToList();

            // This is where all the ships for both players are retrieved
            #region GettingShips
            using (var repo = new PlayerShipsRepository())
            {
                var ps = new PlayerShips();
                using (var playerRepo = new PlayerRepository())
                {
                    var location = new Location();
                    location.LocationId = mission.LocationRefId;
                    defendingPlayer = playerRepo.GetPlayerByLocation(location);
                    attackingPlayer = playerRepo.GetPlayerById(mission.PlayerRefId);
                    ps.PlayerId = defendingPlayer.PlayerId;
                };
                defendingPlayerShips = repo.GetAllShipsAPlayerHasAtHome(ps);
            }
            #endregion

            // This is where all the unmoderated stats of both players are gathered
            #region CalculatingVanillaStats
            for (int i = 0; i < attackingPlayerShips.Count; i++)
            {
                attackerStats[0] += attackingPlayerShips[i].Ships.AttackValue;
                attackerStats[1] += attackingPlayerShips[i].Ships.Health;
                attackerStats[2] += attackingPlayerShips[i].Ships.Armor;
            }
            for (int i = 0; i < defendingPlayerShips.Count; i++)
            {
                defenderStats[0] += defendingPlayerShips[i].Ships.DefenseValue;
                defenderStats[1] += defendingPlayerShips[i].Ships.Health;
                defenderStats[2] += defendingPlayerShips[i].Ships.Armor;
            }
            #endregion
            #region ApplyingResearchAnRacialTraitsAndRandom
            // For the attacking player
            #region AttackingPlayer
            using (var repo = new RacesRacialTraitsRepository())
            {
                var list = repo.GetAllByRace(attackingPlayer.Race);
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].RacialTrait.ModifierName == "Attack Bonus")
                    {
                        modifierAmount[0] += (int)list[i].ModifierAmount;
                    }
                    else if (list[i].RacialTrait.ModifierName == "Health Bonus")
                    {
                        modifierAmount[1] += (int)list[i].ModifierAmount;
                    }
                    else if (list[i].RacialTrait.ModifierName == "Armor Bonus")
                    {
                        modifierAmount[2] += (int)list[i].ModifierAmount;
                    }
                }
            }
            using (var repo = new PlayerPlanetsRepository())
            {
                var pp = new PlayerPlanets();
                pp.PlayerId = attackingPlayer.PlayerId;
                var list = repo.GetAllByPlayerAndPlanet(pp);
                for (int i = 0; i < list.Count; i++)
                {
                    totalSizeOfSolarSystem += list[i].Planet.CurrentSize;
                }
            }
            using (var repo = new PlayerResearchRepository())
            {
                var pr = new PlayerResearch();
                pr.PlayerId = attackingPlayer.PlayerId;
                var list = repo.GetAllByPlayerAndResearch(pr);
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Research.ResearchName == "Attack Bonus")
                    {
                        if (((list[i].Quantity / totalSizeOfSolarSystem) * 10) > 40)
                            modifierAmount[0] += 40;
                        else
                            modifierAmount[0] += (int)((list[i].Quantity / totalSizeOfSolarSystem) * 10);
                    }
                    else if (list[i].Research.ResearchName == "Health Bonus")
                    {
                        if (((list[i].Quantity / totalSizeOfSolarSystem) * 10) > 40)
                            modifierAmount[1] += 40;
                        else
                            modifierAmount[1] += (int)((list[i].Quantity / totalSizeOfSolarSystem) * 10);
                    }
                    else if (list[i].Research.ResearchName == "Armor Bonus")
                    {
                        if (((list[i].Quantity / totalSizeOfSolarSystem) * 10) > 40)
                            modifierAmount[2] += 40;
                        else
                            modifierAmount[2] += (int)((list[i].Quantity / totalSizeOfSolarSystem) * 10);
                    }
                }
            }
            attackerStats[0] *= (int)(1 + (modifierAmount[0] / 100));
            attackerStats[1] *= (int)(1 + (modifierAmount[1] / 100));
            attackerStats[2] *= (int)(1 + (modifierAmount[2] / 100));
            #endregion
            // Cleaning modifying amounts between players
            for (int i = 0; i < modifierAmount.Length; i++)
            {
                modifierAmount[i] = 0;
            }
            #region DefendingPlayer
            using (var repo = new RacesRacialTraitsRepository())
            {
                var list = repo.GetAllByRace(attackingPlayer.Race);
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].RacialTrait.ModifierName == "Attack Bonus")
                    {
                        modifierAmount[0] += (int)list[i].ModifierAmount;
                    }
                    else if (list[i].RacialTrait.ModifierName == "Health Bonus")
                    {
                        modifierAmount[1] += (int)list[i].ModifierAmount;
                    }
                    else if (list[i].RacialTrait.ModifierName == "Armor Bonus")
                    {
                        modifierAmount[2] += (int)list[i].ModifierAmount;
                    }
                }
            }
            using (var repo = new PlayerPlanetsRepository())
            {
                var pp = new PlayerPlanets();
                pp.PlayerId = attackingPlayer.PlayerId;
                var list = repo.GetAllByPlayerAndPlanet(pp);
                for (int i = 0; i < list.Count; i++)
                {
                    totalSizeOfSolarSystem += list[i].Planet.CurrentSize;
                }
            }
            using (var repo = new PlayerResearchRepository())
            {
                var pr = new PlayerResearch();
                pr.PlayerId = attackingPlayer.PlayerId;
                var list = repo.GetAllByPlayerAndResearch(pr);
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Research.ResearchName == "Attack Bonus")
                    {
                        if (((list[i].Quantity / totalSizeOfSolarSystem) * 10) > 40)
                            modifierAmount[0] += 40;
                        else
                            modifierAmount[0] += (int)((list[i].Quantity / totalSizeOfSolarSystem) * 10);
                    }
                    else if (list[i].Research.ResearchName == "Health Bonus")
                    {
                        if (((list[i].Quantity / totalSizeOfSolarSystem) * 10) > 40)
                            modifierAmount[1] += 40;
                        else
                            modifierAmount[1] += (int)((list[i].Quantity / totalSizeOfSolarSystem) * 10);
                    }
                    else if (list[i].Research.ResearchName == "Armor Bonus")
                    {
                        if (((list[i].Quantity / totalSizeOfSolarSystem) * 10) > 40)
                            modifierAmount[2] += 40;
                        else
                            modifierAmount[2] += (int)((list[i].Quantity / totalSizeOfSolarSystem) * 10);
                    }
                }
            }
            defenderStats[0] *= (int)(1 + (modifierAmount[0] / 100));
            defenderStats[1] *= (int)(1 + (modifierAmount[1] / 100));
            defenderStats[2] *= (int)(1 + (modifierAmount[2] / 100));
            #endregion

            #endregion
            #region CalculatingWhatWillDie
            using (var repo = new PlayerShipsRepository())
            {


                // Variables that remembers how much is left after armor mitigation
                int defenderAfterArmor = defenderStats[0] - attackerStats[2];
                int defenderRelevantArmor;
                #region AttackerKilledAll

                // Gives the armor it's value in this specific situation
                defenderRelevantArmor = (attackerStats[2] * ((rnd.Next(30, 61) / 100)) / 2);

                if (attackerStats[0] > (defenderStats[1] + defenderRelevantArmor))
                {
                    for (int i = 0; i < defendingPlayerShips.Count; i++)
                    {
                        defendingPlayerShips[i].Quantity = 0;
                    }
                    
                }
                #endregion
                #endregion
            }
            #endregion


            return true;
        }
        private bool HandleProtectionMission(Missions mission)
        {
            return true;
        }
        /// <summary>
        /// Updates the resources for all the players in the database
        /// </summary>
        /// <param name="players"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        private bool UpdateResources(List<Players> players, double modifier)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (!UpdateResourcesForSinglePlayer(players[i], modifier))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// This updates the resources for a single player based on the buildings he has.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private bool UpdateResourcesForSinglePlayer(Players player, double modifier)
        {
            int bankCount = 0;
            int drillCount = 0;
            int mineCount = 0;
            int specialResourceCount = 0;
            // The repo that we will use to update the resources
            using (var repo = new PlayerResourcesRepository())
            {
                // The repo we will use to find out how many of the resource generating buildings a player has
                using (var secondRepo = new PlayerBuildingsRepository())
                {
                    PlayerBuildings pb = new PlayerBuildings();
                    pb.PlayerId = player.PlayerId;
                    var buildingList = secondRepo.GetAllByPlayerAndPlanet(pb);
                    for (int i = 0; i < buildingList.Count; i++)
                    {
                        if (buildingList[i].Building.BuildingName == "Bank")
                            bankCount += buildingList[i].Quantity;
                        if (buildingList[i].Building.BuildingName == "Drill")
                            drillCount += buildingList[i].Quantity;
                        if (buildingList[i].Building.BuildingName == "Mine")
                            mineCount += buildingList[i].Quantity;
                        if (buildingList[i].Building.BuildingName == "Special Resource")
                            specialResourceCount += buildingList[i].Quantity;
                    }
                }
                // Adding the quantity of resources generated to the players total
                PlayerResources pr = new PlayerResources();
                pr.PlayerId = player.PlayerId;
                var resourceList = repo.GetAllByPlayer(pr);
                for (int i = 0; i < resourceList.Count; i++)
                {
                    if (resourceList[i].Resource.ResourceName == "Minerals")
                        resourceList[i].Quantity += (int)(mineCount * modifier);
                    if (resourceList[i].Resource.ResourceName == "Oil")
                        resourceList[i].Quantity += (int)(drillCount * modifier);
                    if (resourceList[i].Resource.ResourceName == "Credits")
                        resourceList[i].Quantity += (int)(bankCount * modifier);
                    if (resourceList[i].Resource.ResourceName == "Special Resource")
                        resourceList[i].Quantity += (int)(specialResourceCount * modifier);
                }
                // Updates the database with the new amount
                if (repo.AddOrUpdate(resourceList))
                    return true;
                return false;

            }

        }
    }
}
