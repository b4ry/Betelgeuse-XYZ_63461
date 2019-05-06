using Assets.Scripts.Controllers;
using Assets.Scripts.Enums;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Managers.Player
{
    public class HumanPlayerManager : IPlayerManager
    {
        public RaceEnum Race { get; set; }
        public GameObject InitialRegion { get; set; }

        public HumanPlayerManager()
        {
            foreach(var regionObject in GameController.Instance.RegionObjects)
            {
                var regionController = regionObject.GetComponent<RegionController>();

                regionController.RegionModel.PopulatePlayerBuildings(RaceEnum.Human);
            }
        }

        public void DisplayBuiltBuildings(string regionName)
        {
            var builtBuildings = SelectedRegionsController.SelectedRegionObjects.FirstOrDefault().RegionModel.PlayerBuildings.Where(b => b.Built);

            foreach (var buildingModel in builtBuildings)
            {
                Debug.Log(buildingModel.Name);
            }
        }

        public void DisplayAvailableBuildings(string regionName)
        {
            var availableBuildings = SelectedRegionsController.SelectedRegionObjects.FirstOrDefault().RegionModel.PlayerBuildings.Where(b => !b.Built && b.Available);

            foreach (var buildingModel in availableBuildings)
            {
                Debug.Log(buildingModel.Name);
            }
        }
    }
}
