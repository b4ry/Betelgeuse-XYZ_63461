using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    [XmlRoot("Race")]
    public class RaceDefinitionModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
