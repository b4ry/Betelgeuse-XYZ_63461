using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("AvailableBiomes")]
    public class AvailableBiomesDefinitionModel
    {
        [XmlArray("Biomes")]
        [XmlArrayItem("Biome")]
        public List<BiomeDefinitionModel> Biomes = new List<BiomeDefinitionModel>();

        public static AvailableBiomesDefinitionModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(AvailableBiomesDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as AvailableBiomesDefinitionModel;
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(AvailableBiomesDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
