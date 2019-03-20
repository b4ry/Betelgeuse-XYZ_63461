using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("AvailableResources")]
    public class AvailableResourcesDefinitionModel
    {
        [XmlArray("Resources")]
        [XmlArrayItem("Resource")]
        public List<ResourceDefinitionModel> Resources = new List<ResourceDefinitionModel>();

        public static AvailableResourcesDefinitionModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(AvailableResourcesDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as AvailableResourcesDefinitionModel;
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(AvailableResourcesDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
