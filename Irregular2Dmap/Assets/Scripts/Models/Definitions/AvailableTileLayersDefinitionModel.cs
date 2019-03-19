using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("AvailableTileLayers")]
    public class AvailableTileLayersDefinitionModel
    {
        [XmlArray("TileLayers")]
        [XmlArrayItem("TileLayer")]
        public List<TileLayerDefinitionModel> TileLayers = new List<TileLayerDefinitionModel>();

        public static AvailableTileLayersDefinitionModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(AvailableTileLayersDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as AvailableTileLayersDefinitionModel;
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(AvailableTileLayersDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
