using Assets.Scripts.Enums;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("Region")]
    public class RegionDefinitionModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        public RegionSizeEnum Size { get; set; }
        [XmlArray("Biomes")]
        [XmlArrayItem("Biome")]
        public List<BiomeDefinitionModel> Biomes = new List<BiomeDefinitionModel>();
        [XmlArray("Neighbours")]
        [XmlArrayItem("Neighbour")]
        public List<RegionDefinitionModel> Neighbours = new List<RegionDefinitionModel>();

        public static RegionDefinitionModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(RegionDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as RegionDefinitionModel;
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(RegionDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
