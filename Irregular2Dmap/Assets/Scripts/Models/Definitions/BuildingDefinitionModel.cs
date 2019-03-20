using Assets.Scripts.Enums;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("Building")]
    public class BuildingDefinitionModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        public BuildingTypeEnum BuildingType { get; set; }
        [XmlArray("Costs")]
        [XmlArrayItem("ResourceCost")]
        public List<ResourceCostModel> Cost = new List<ResourceCostModel>();
        public bool Built { get; set; }
        public bool Available { get; set; }

        public BuildingDefinitionModel()
        {
        }

        public BuildingDefinitionModel(string name, BuildingTypeEnum buildingType, List<ResourceCostModel> cost, bool available)
        {
            Name = name;
            BuildingType = buildingType;
            Built = false;
            Available = available;

            foreach(var resourceCostModel in cost)
            {
                Cost.Add(resourceCostModel);
            }
        }

        public static BuildingDefinitionModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(BuildingDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as BuildingDefinitionModel;
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(BuildingDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
