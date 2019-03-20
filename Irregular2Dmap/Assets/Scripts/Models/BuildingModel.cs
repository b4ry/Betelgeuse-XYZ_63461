using Assets.Scripts.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public class BuildingModel
    {
        public string Name { get; set; }
        public BuildingTypeEnum BuildingType { get; set; }
        public List<ResourceCostModel> Cost = new List<ResourceCostModel>();
        public bool Built { get; set; }
        public bool Available { get; set; }

        public BuildingModel(string name, BuildingTypeEnum buildingType, List<ResourceCostModel> cost, bool available)
        {
            Name = name;
            BuildingType = buildingType;
            Built = false;
            Available = available;

            foreach (var resourceCostModel in cost)
            {
                Cost.Add(resourceCostModel);
            }
        }
    }
}
