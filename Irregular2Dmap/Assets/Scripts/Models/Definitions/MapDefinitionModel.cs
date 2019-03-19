using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("Map")]
    public class MapDefinitionModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        public int RegionsNumber { get; set; }

        [XmlArray("Regions")]
        [XmlArrayItem("Region")]
        public List<RegionDefinitionModel> Regions = new List<RegionDefinitionModel>();

        public static MapDefinitionModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(MapDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as MapDefinitionModel;
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(MapDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
