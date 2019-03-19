using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("AvailableMaps")]
    public class AvailableMapsDefinitionModel
    {
        [XmlArray("Maps")]
        [XmlArrayItem("Map")]
        public List<MapDefinitionModel> Maps = new List<MapDefinitionModel>();

        public static AvailableMapsDefinitionModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(AvailableMapsDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as AvailableMapsDefinitionModel;
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(AvailableMapsDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
