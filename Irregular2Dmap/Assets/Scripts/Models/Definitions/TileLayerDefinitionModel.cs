using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("TileLayer")]
    public class TileLayerDefinitionModel
    {
        [XmlAttribute("name")]
        public string Name;
        public MaterialHardnessEnum MaterialHardness;

        public TileLayerDefinitionModel()
        {
            
        }

        public TileLayerDefinitionModel(string name, MaterialHardnessEnum materialHardness)
        {
            Name = name;
            MaterialHardness = materialHardness;
        }

        public static TileLayerDefinitionModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(TileLayerDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as TileLayerDefinitionModel;
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(TileLayerDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
