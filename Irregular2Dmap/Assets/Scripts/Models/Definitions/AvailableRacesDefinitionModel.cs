using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("AvailableRaces")]
    public class AvailableRacesDefinitionModel
    {
        [XmlArray("Races")]
        [XmlArrayItem("Race")]
        public List<RaceDefinitionModel> Races = new List<RaceDefinitionModel>();

        public static AvailableRacesDefinitionModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(AvailableRacesDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as AvailableRacesDefinitionModel;
            }
        }

        public void Save(string path)
        {
            var serializer = new XmlSerializer(typeof(AvailableRacesDefinitionModel));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
