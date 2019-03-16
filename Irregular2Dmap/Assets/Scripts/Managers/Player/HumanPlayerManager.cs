using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Managers.Player
{
    public class HumanPlayerManager : IPlayerManager
    {
        public RaceEnum Race { get; set; }
        public Dictionary<string, List<BuildingModel>> Buildings { get; set; }

        public void DisplayBuildings(string regionName)
        {
            var builtBuildings = Buildings[regionName].Where(b => !b.Built);

            foreach (var buildingModel in builtBuildings)
            {
                Debug.Log(buildingModel.Name);
            }
        }

        public void Build(string regionName)
        {
            var availableBuildings = Buildings[regionName].Where(b => !b.Built && b.Available);

            foreach (var buildingModel in availableBuildings)
            {
                Debug.Log(buildingModel.Name);
            }
        }
    }
}
