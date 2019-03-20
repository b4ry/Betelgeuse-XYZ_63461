using Assets.Scripts.Enums;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("Resource")]
    public class ResourceDefinitionModel
    {
        [XmlAttribute("name")]
        public string Name;
        public RarityEnum Rarity;

        public ResourceDefinitionModel()
        {
        }

        public ResourceDefinitionModel(string name, RarityEnum rarity)
        {
            Name = name;
            Rarity = rarity;
        }

        public static ResourceDefinitionModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(ResourceDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as ResourceDefinitionModel;
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(ResourceDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
