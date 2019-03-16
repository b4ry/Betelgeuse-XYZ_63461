using Assets.Scripts.Enums;

namespace Assets.Scripts.Models.Definitions
{
    public class BuildingDefinitionModel
    {
        public string Name { get; set; }
        public BuildingTypeEnum BuildingType { get; set; }
        public CostModel Cost { get; set; }
        public bool Built { get; set; }
        public bool Available { get; set; }

        public BuildingDefinitionModel(string name, BuildingTypeEnum buildingType, CostModel cost, bool available)
        {
            Name = name;
            BuildingType = buildingType;
            Cost = cost;
            Built = false;
            Available = available;
        }
    }
}
