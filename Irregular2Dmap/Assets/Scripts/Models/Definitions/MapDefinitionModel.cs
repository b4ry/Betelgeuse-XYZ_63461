using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    public class MapDefinitionModel
    {
        [XmlAttribute("name")]
        public string MapName { get; set; }
    }
}
