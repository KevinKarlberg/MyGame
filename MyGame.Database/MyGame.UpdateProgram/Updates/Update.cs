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
            bool didAttackerWin = false;

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
                attackerStats[0] += (attackingPlayerShips[i].Ships.AttackValue * attackingPlayerShips[i].Quantity);
                attackerStats[1] += (attackingPlayerShips[i].Ships.Health * attackingPlayerShips[i].Quantity);
                attackerStats[2] += (attackingPlayerShips[i].Ships.Armor * attackingPlayerShips[i].Quantity);
            }
            for (int i = 0; i < defendingPlayerShips.Count; i++)
            {
                defenderStats[0] += (defendingPlayerShips[i].Ships.DefenseValue * defendingPlayerShips[i].Quantity);
                defenderStats[1] += (defendingPlayerShips[i].Ships.Health * defendingPlayerShips[i].Quantity);
                defenderStats[2] += (defendingPlayerShips[i].Ships.Armor * defendingPlayerShips[i].Quantity);
            }
            // This part adds all allied forces that are on your planet to the combined defensive power
            var missionList = AreThereAlliedShipsOnMyPlanet(defendingPlayer);
            if (missionList != null)
            {
                for (int i = 0; i < missionList.Count; i++)
                {
                    var assistingAllyShips = missionList[i].Ships.ToList();
                    for (int j = 0; i < assistingAllyShips.Count; j++)
                    {
                        defenderStats[0] += (assistingAllyShips[j].Ships.DefenseValue * assistingAllyShips[j].Quantity);
                        defenderStats[1] += (assistingAllyShips[j].Ships.Health * assistingAllyShips[j].Quantity);
                        defenderStats[2] += (assistingAllyShips[j].Ships.Armor * assistingAllyShips[j].Quantity);
                    }
                }
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
            #region CalculatingShipDeaths

            bool tooMuchArmor = false;
            using (var repo = new PlayerShipsRepository())
            {
                double percentageOfKillsShips;
                // Variables that remembers how much is left after armor mitigation
                // Gives the armor it's value in this specific situation
                // This part removes all ships from the defender if the attacker has enough to wipe him
                if (attackerStats[0] > (defenderStats[1] + defenderStats[2]))
                {
                    tooMuchArmor = true;
                    // Removing all the dead ships from the defending player
                    for (int i = 0; i < defendingPlayerShips.Count; i++)
                    {
                        defendingPlayerShips[i].Quantity = 0;
                    }
                    didAttackerWin = true;
                    for (int i = 0; i < attackingPlayerShips.Count; i++)
                    {
                        percentageOfKillsShips = 0;
                        percentageOfKillsShips = (1 - (defenderStats[0] / (attackerStats[1] + attackerStats[2])));
                        // If the quantity of ships after the quantity has been changed is less than one
                        // this part gives it a percentage based upon the percentage of surviving ships to live
                        if ((attackingPlayerShips[i].Quantity *= (int)percentageOfKillsShips) < 1)
                        {
                            if (rnd.Next(0, 101) >= (int)(percentageOfKillsShips * 100))
                            {
                                attackingPlayerShips[i].Quantity = 1;
                            }
                            else
                                attackingPlayerShips[i].Quantity = 0;
                        }
                        else if (tooMuchArmor)
                            attackingPlayerShips[i].Quantity -= (int)(attackingPlayerShips[i].Quantity * (percentageOfKillsShips) / 2);
                        attackingPlayerShips[i].Quantity -= (int)(attackingPlayerShips[i].Quantity * percentageOfKillsShips);
                    }
                }
                // This part removes all the ships from teh attacker if the defender has enough to wipe him
                else if (defenderStats[0] > (attackerStats[1] + attackerStats[2]))
                {
                    // Removing all the dead ships from the defending player
                    for (int i = 0; i < defendingPlayerShips.Count; i++)
                    {
                        attackingPlayerShips[i].Quantity = 0;
                    }
                    percentageOfKillsShips = 0;
                    percentageOfKillsShips = (1 - (defenderStats[0] / (attackerStats[1] + attackerStats[2])));
                    for (int i = 0; i < defendingPlayerShips.Count; i++)
                    {
                        // If the quantity of ships after the quantity has been changed is less than one
                        // this part gives it a percentage based upon the percentage of surviving ships to live
                        if ((defendingPlayerShips[i].Quantity *= (int)percentageOfKillsShips) < 1)
                        {
                            if (rnd.Next(0, 101) <= (int)(percentageOfKillsShips * 100))
                            {
                                defendingPlayerShips[i].Quantity = 1;
                            }
                            else
                                defendingPlayerShips[i].Quantity = 0;
                        }
                        if (tooMuchArmor)
                            defendingPlayerShips[i].Quantity -= (int)(defendingPlayerShips[i].Quantity * (percentageOfKillsShips) / 2);
                        defendingPlayerShips[i].Quantity -= (int)(defendingPlayerShips[i].Quantity * percentageOfKillsShips);
                    }
                }
                // This part is if neither the attacker nor the defender has enough to wipe eachother
                else
                {
                    // This line calculates the percentage of ships that should survive from the defending player
                    percentageOfKillsShips = 0;
                    percentageOfKillsShips = (1 - (attackerStats[0] / (defenderStats[1] + defenderStats[2])));
                    if (defenderStats[2] > attackerStats[0])
                        tooMuchArmor = true;

                    for (int i = 0; i < defendingPlayerShips.Count; i++)
                    {
                        // If the quantity of ships after the quantity has been changed is less than one
                        // this part gives it a percentage based upon the percentage of surviving ships to live
                        if ((defendingPlayerShips[i].Quantity *= (int)percentageOfKillsShips) < 1)
                        {
                            if (rnd.Next(0, 101) <= (int)(percentageOfKillsShips * 100))
                            {
                                defendingPlayerShips[i].Quantity = 1;
                            }
                            else
                                defendingPlayerShips[i].Quantity = 0;
                        }
                        if (tooMuchArmor)
                            defendingPlayerShips[i].Quantity -= (int)(defendingPlayerShips[i].Quantity * (percentageOfKillsShips) / 2);
                        defendingPlayerShips[i].Quantity -= (int)(defendingPlayerShips[i].Quantity * percentageOfKillsShips);
                    }
                    percentageOfKillsShips = 0;
                    percentageOfKillsShips = (1 - (defenderStats[0] / (attackerStats[1] + attackerStats[2])));
                    for (int i = 0; i < attackingPlayerShips.Count; i++)
                    {
                        // If the quantity of ships after the quantity has been changed is less than one
                        // this part gives it a percentage based upon the percentage of surviving ships to live
                        if ((attackingPlayerShips[i].Quantity *= (int)percentageOfKillsShips) < 1)
                        {
                            if (rnd.Next(0, 101) <= (int)(percentageOfKillsShips * 100))
                            {
                                attackingPlayerShips[i].Quantity = 1;
                            }
                            else
                                attackingPlayerShips[i].Quantity = 0;
                        }
                        if (tooMuchArmor)
                            attackingPlayerShips[i].Quantity -= (int)(attackingPlayerShips[i].Quantity * (percentageOfKillsShips) / 2);
                        attackingPlayerShips[i].Quantity -= (int)(attackingPlayerShips[i].Quantity * percentageOfKillsShips);
                    }
                    #endregion


                }
            }

            #endregion
            #region LandingParty

            // Resetting attacker and defenderstats for 
            for (int i = 0; i < attackerStats.Length; i++)
            {
                attackerStats[i] = 0;
            }
            for (int i = 0; i < defenderStats.Length; i++)
            {
                defenderStats[i] = 0;
            }
            var attackerTroopList = new List<PlayerTroops>();
            var defenderTroopList = new List<PlayerTroops>();
            if (didAttackerWin)
            {
                using (var troopRepo = new PlayerTroopsRepository())
                {
                    var pt = new PlayerTroops();
                    pt.PlayerId = attackingPlayer.PlayerId;
                    attackerTroopList = mission.Troops.ToList();
                    defenderTroopList = troopRepo.GetAllTroopsAPlayerHasAtHome(pt);
                }
            }
            else
            {
                return true;
            }
            #region CalculatingVanillaStats
            for (int i = 0; i < attackingPlayerShips.Count; i++)
            {
                attackerStats[0] += (attackerTroopList[i].Troops.AttackValue * attackerTroopList[i].Quantity);
                attackerStats[1] += (attackerTroopList[i].Troops.Health * attackerTroopList[i].Quantity);
                attackerStats[2] += (attackerTroopList[i].Troops.Armor * attackerTroopList[i].Quantity);
            }
            for (int i = 0; i < defendingPlayerShips.Count; i++)
            {
                defenderStats[0] += (defenderTroopList[i].Troops.DefenseValue * defenderTroopList[i].Quantity);
                defenderStats[1] += (defenderTroopList[i].Troops.Health * defenderTroopList[i].Quantity);
                defenderStats[2] += (defenderTroopList[i].Troops.Armor * defenderTroopList[i].Quantity);
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
            #region CalculatingTroopDeaths

            using (var repo = new PlayerTroopsRepository())
            {
                double percentageOfTroopsKilled;
                // Variables that remembers how much is left after armor mitigation
                // Gives the armor it's value in this specific situation
                // This part removes all ships from the defender if the attacker has enough to wipe him
                if (attackerStats[0] > (defenderStats[1] + defenderStats[2]))
                {
                    tooMuchArmor = true;
                    // Removing all the dead ships from the defending player
                    for (int i = 0; i < defenderTroopList.Count; i++)
                    {
                        defenderTroopList[i].Quantity = 0;
                    }
                    didAttackerWin = true;
                    for (int i = 0; i < attackerTroopList.Count; i++)
                    {
                        percentageOfTroopsKilled = 0;
                        percentageOfTroopsKilled = (1 - (defenderStats[0] / (attackerStats[1] + attackerStats[2])));
                        // If the quantity of ships after the quantity has been changed is less than one
                        // this part gives it a percentage based upon the percentage of surviving ships to live
                        if ((attackerTroopList[i].Quantity *= (int)percentageOfTroopsKilled) < 1)
                        {
                            if (rnd.Next(0, 101) >= (int)(percentageOfTroopsKilled * 100))
                            {
                                attackerTroopList[i].Quantity = 1;
                            }
                            else
                                attackerTroopList[i].Quantity = 0;
                        }
                        attackerTroopList[i].Quantity -= (int)(attackerTroopList[i].Quantity * percentageOfTroopsKilled);
                    }
                }
                // This part removes all the ships from teh attacker if the defender has enough to wipe him
                else if (defenderStats[0] > (attackerStats[1] + attackerStats[2]))
                {
                    didAttackerWin = false;
                    // Removing all the dead ships from the defending player
                    for (int i = 0; i < defenderTroopList.Count; i++)
                    {
                        attackerTroopList[i].Quantity = 0;
                    }
                    percentageOfTroopsKilled = 0;
                    percentageOfTroopsKilled = (1 - (defenderStats[0] / (attackerStats[1] + attackerStats[2])));
                    for (int i = 0; i < defenderTroopList.Count; i++)
                    {
                        // If the quantity of ships after the quantity has been changed is less than one
                        // this part gives it a percentage based upon the percentage of surviving ships to live
                        if ((defenderTroopList[i].Quantity *= (int)percentageOfTroopsKilled) < 1)
                        {
                            if (rnd.Next(0, 101) <= (int)(percentageOfTroopsKilled * 100))
                            {
                                defenderTroopList[i].Quantity = 1;
                            }
                            else
                                defenderTroopList[i].Quantity = 0;
                        }
                        defenderTroopList[i].Quantity -= (int)(defenderTroopList[i].Quantity * percentageOfTroopsKilled);
                    }
                }
                // This part is if neither the attacker nor the defender has enough to wipe eachother
                else
                {
                    didAttackerWin = false;
                    // This line calculates the percentage of ships that should survive from the defending player
                    percentageOfTroopsKilled = 0;
                    percentageOfTroopsKilled = (1 - (attackerStats[0] / (defenderStats[1] + defenderStats[2])));

                    for (int i = 0; i < defenderTroopList.Count; i++)
                    {
                        // If the quantity of ships after the quantity has been changed is less than one
                        // this part gives it a percentage based upon the percentage of surviving ships to live
                        if ((defenderTroopList[i].Quantity *= (int)percentageOfTroopsKilled) < 1)
                        {
                            if (rnd.Next(0, 101) <= (int)(percentageOfTroopsKilled * 100))
                            {
                                defenderTroopList[i].Quantity = 1;
                            }
                            else
                                defenderTroopList[i].Quantity = 0;
                        }
                        defenderTroopList[i].Quantity -= (int)(defenderTroopList[i].Quantity * percentageOfTroopsKilled);
                    }
                    percentageOfTroopsKilled = 0;
                    percentageOfTroopsKilled = (1 - (defenderStats[0] / (attackerStats[1] + attackerStats[2])));
                    for (int i = 0; i < attackerTroopList.Count; i++)
                    {
                        // If the quantity of ships after the quantity has been changed is less than one
                        // this part gives it a percentage based upon the percentage of surviving ships to live
                        if ((attackerTroopList[i].Quantity *= (int)percentageOfTroopsKilled) < 1)
                        {
                            if (rnd.Next(0, 101) <= (int)(percentageOfTroopsKilled * 100))
                            {
                                attackerTroopList[i].Quantity = 1;
                            }
                            else
                                attackerTroopList[i].Quantity = 0;
                        }
                        attackerTroopList[i].Quantity -= (int)(attackerTroopList[i].Quantity * percentageOfTroopsKilled);
                    }
                    


                }
            }
            #endregion
            #endregion
            #region EffectOfAttack
            if (didAttackerWin)
            {
                int totalAmountOfResourcesPlayerCanTake = 0;
                int totalAmountOfResourcesDefenderHas = 0;
                using (var troopRepo = new PlayerTroopsRepository())
                {
                    switch (mission.MissionType.MissionTypeName)
                    {
                        #region Standard attack
                        case "Standard":
                            using (var repo = new PlayerResourcesRepository())
                            {
                                var pr = new PlayerResources();
                                pr.PlayerId = defendingPlayer.PlayerId;
                                var list = repo.GetAllByPlayer(pr);
                                for (int i = 0; i < attackerTroopList.Count; i++)
                                {
                                   totalAmountOfResourcesPlayerCanTake += attackerTroopList[i].Troops.AbleToCarry;
                                }
                                for (int i = 0; i < list.Count; i++)
                                {
                                    totalAmountOfResourcesDefenderHas += list[i].Quantity;
                                }
                                if (totalAmountOfResourcesPlayerCanTake > (totalAmountOfResourcesDefenderHas * 0.8))
                                {
                                    for (int i = 0; i < list.Count; i++)
                                    {
                                        if (list[i].Resource.ResourceName == "Minerals")
                                        {
                                            mission.CarryingResources.Minerals += (int)(list[i].Quantity * 0.8);
                                            list[i].Quantity -= (int)(list[i].Quantity * 0.8);
                                        }
                                        else if (list[i].Resource.ResourceName == "Oil")
                                        {
                                            mission.CarryingResources.Oil += (int)(list[i].Quantity * 0.8);
                                            list[i].Quantity -= (int)(list[i].Quantity * 0.8);
                                        }
                                        else if (list[i].Resource.ResourceName == "Credits")
                                        {
                                            mission.CarryingResources.Oil += (int)(list[i].Quantity * 0.8);
                                            list[i].Quantity -= (int)(list[i].Quantity * 0.8);
                                        }
                                        else if (list[i].Resource.ResourceName == "Special Resource")
                                        {
                                            mission.CarryingResources.Oil += (int)(list[i].Quantity * 0.8);
                                            list[i].Quantity -= (int)(list[i].Quantity * 0.8);
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < list.Count; i++)
                                    {
                                        if (list[i].Resource.ResourceName == "Minerals")
                                        {
                                            mission.CarryingResources.Minerals += (int)(list[i].Quantity * (totalAmountOfResourcesPlayerCanTake / totalAmountOfResourcesDefenderHas));
                                            list[i].Quantity -= (int)(list[i].Quantity * 0.8);
                                        }
                                        else if (list[i].Resource.ResourceName == "Oil")
                                        {
                                            mission.CarryingResources.Oil += (int)(list[i].Quantity * (totalAmountOfResourcesPlayerCanTake / totalAmountOfResourcesDefenderHas));
                                            list[i].Quantity -= (int)(list[i].Quantity * 0.8);
                                        }
                                        else if (list[i].Resource.ResourceName == "Credits")
                                        {
                                            mission.CarryingResources.Oil += (int)(list[i].Quantity * (totalAmountOfResourcesPlayerCanTake / totalAmountOfResourcesDefenderHas));
                                            list[i].Quantity -= (int)(list[i].Quantity * 0.8);
                                        }
                                        else if (list[i].Resource.ResourceName == "Special Resource")
                                        {
                                            mission.CarryingResources.Oil += (int)(list[i].Quantity * (totalAmountOfResourcesPlayerCanTake / totalAmountOfResourcesDefenderHas));
                                            list[i].Quantity -= (int)(list[i].Quantity * 0.8);
                                        }
                                    }
                                }
                                repo.RemoveOrUpdate(list);
                            }
                            using (var repo = new MissionRepository())
                            {
                                repo.CreateOrUpdateAMission(mission);
                            }
                                break;
                        #endregion
                        case "Raze":
                            break;
                        case "Capture":
                            break;
                    }
                }
            }
            else
                return true;
            #endregion
            return true;

        }
        /// <summary>
        /// If there are allies on my planet, they loose ships too, this method removes them
        /// </summary>
        /// <param name="missions"></param>
        /// <param name="percentageOfShipsKilled"></param>
        /// <returns></returns>
        private bool RemoveShipsAlliesOnMyPlanetLooses(List<Missions> missions, int percentageOfShipsKilled)
        {
            Random rnd = new Random();
            for (int i = 0; i < missions.Count; i++)
            {
                var defendingPlayerShips = missions[i].Ships.ToList();
                for (int j = 0; i < defendingPlayerShips.Count; j++)
                {
                    // If the quantity of ships after the quantity has been changed is less than one
                    // this part gives it a percentage based upon the percentage of surviving ships to live
                    if ((defendingPlayerShips[i].Quantity *= (int)percentageOfShipsKilled) < 1)
                    {
                        if (rnd.Next(0, 101) <= (int)(percentageOfShipsKilled * 100))
                        {
                            defendingPlayerShips[i].Quantity = 1;
                        }
                        else
                            defendingPlayerShips[i].Quantity = 0;
                    }
                    defendingPlayerShips[i].Quantity -= (int)(defendingPlayerShips[i].Quantity * percentageOfShipsKilled);
                }
                using (var repo = new PlayerShipsRepository())
                {
                    repo.RemoveOrUpdate(defendingPlayerShips);
                }
            }
            return true;
        }
        private List<Missions> AreThereAlliedShipsOnMyPlanet(Players player)
        {
            using (var repo = new MissionRepository())
            {
                var list = repo.GetAllMissionsByLocation(player);

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].MissionType.MissionTypeName == "Attack")
                    {
                        list.Remove(list[i]);
                        i--;
                    }
                }
                return list;
            }

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
