using Assets.Scripts.Enums;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("Biome")]
    public class BiomeDefinitionModel
    {
        [XmlAttribute("name")]
        public string Name;
        public RarityEnum Rarity;
        [XmlArray("Resources")]
        [XmlArrayItem("Resource")]
        public List<ResourceDefinitionModel> ResourceDefinitions;

        public BiomeDefinitionModel()
        {
        }

        public BiomeDefinitionModel(string name, RarityEnum rarity, List<ResourceDefinitionModel> resourceDefinitions)
        {
            Name = name;
            Rarity = rarity;
            ResourceDefinitions = resourceDefinitions;
        }

        public static BiomeDefinitionModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(BiomeDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as BiomeDefinitionModel;
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(BiomeDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
