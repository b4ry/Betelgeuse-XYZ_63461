using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Managers.Player
{
    public class TechPlayerManager : IPlayerManager
    {
        public RaceEnum Race { get; set; }
        public List<BuildingModel> ShipModules { get; set; }
        public GameObject InitialRegion { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public TechPlayerManager()
        {
            ShipModules = new List<BuildingModel>();

            foreach (var buildingDefinition in DefinitionsController.Instance.BuildingDefinitions[RaceEnum.TechHuman].Values)
            {
                var buildingModel = new BuildingModel(buildingDefinition.Name, buildingDefinition.BuildingType, buildingDefinition.Cost, buildingDefinition.Available);

                ShipModules.Add(buildingModel);
            }
        }

        public void DisplayBuiltBuildings(string regionName = "")
        {
            var builtShipModules = ShipModules.Where(b => b.Built);

            foreach(var shipModule in builtShipModules)
            {
                Debug.Log(shipModule.Name);
            }
        }

        public void DisplayAvailableBuildings(string regionName = "")
        {
            var availableShipModules = ShipModules.Where(b => !b.Built && b.Available);

            foreach (var availableShipModule in availableShipModules)
            {
                Debug.Log(availableShipModule.Name);
            }
        }
    }
}
