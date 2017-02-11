using MyGame.DB.DB.Models;
using MyGame.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame.MVCSite.BuissnessLogic
{
    public class ResearchRelatedLogic
    {
        /// <summary>
        /// Checks if the player can afford doing the research suggested, and if he can it executes the command to do it
        /// </summary>
        public bool ConductNewResearch(Players player, List<PlayerResearch> playerResearch)
        {
            int counter = 0;
            int? minerals = 0;
            int? oil = 0;
            int? credits = 0;
            int? specialcredits = 0;
            int? specialresource = 0;
            int? totalQuantity = 0;


            // This loops through all the troops that the player wants to train and adds the entire cost of everything
            for (int i = 0; i < playerResearch.Count; i++)
            {
                minerals += playerResearch[i].Research.Price.Minerals * playerResearch[i].Quantity;
                oil += playerResearch[i].Research.Price.Oil * playerResearch[i].Quantity;
                credits += playerResearch[i].Research.Price.Credits * playerResearch[i].Quantity;
                specialcredits += playerResearch[i].Research.Price.SpecialCredits * playerResearch[i].Quantity;
                specialresource += playerResearch[i].Research.Price.SpecialResource * playerResearch[i].Quantity;
                totalQuantity += playerResearch[i].Quantity;

            }

            // This part loops through all the available resources for the player, and if the player has more of a given resource than he needs, counter gets one additional value
            using (var repo = new PlayerResourcesRepository())
            {
                PlayerResources pr = new PlayerResources();
                pr.PlayerId = player.PlayerId;
                var resources = repo.GetAllByPlayer(pr);
                for (int i = 0; i < resources.Count; i++)
                {
                    if (resources[i].Resource.ResourceName == "Minerals" && resources[i].Quantity >= minerals)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Oil" && resources[i].Quantity >= oil)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Credits" && resources[i].Quantity >= credits)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Special Credits" && resources[i].Quantity >= specialcredits)
                        counter++;
                    else if (resources[i].Resource.ResourceName == "Special Resource" && resources[i].Quantity >= specialresource)
                        counter++;

                }
            }

            // If the counter has 5 here, it means that the player has more than he needs to pay for the troops and that there is available population to be drafted
            if (counter == 5)
            {
                using (var repo = new PlayerResearchRepository())
                {
                    repo.AddOrUpdate(playerResearch);
                    counter++;
                }
            }
            if (counter == 6)
                return true;
            return false;
        }
        /// <summary>
        /// Removes troops from the player
        /// </summary>
        /// <param name="playerBuildings"></param>
        /// <returns></returns>
        public bool RemovingTroopsFromPlayer(List<PlayerResearch> playerResearch)
        {
            using (var repo = new PlayerResearchRepository())
            {

                if (repo.RemoveOrUpdate(playerResearch))
                    return true;
                return false;
            }
        }
    }
}