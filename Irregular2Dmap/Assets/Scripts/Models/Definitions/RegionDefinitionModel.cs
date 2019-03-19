using System.Xml.Serialization;

namespace Assets.Scripts.Models.Definitions
{
    public class RegionDefinitionModel
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
