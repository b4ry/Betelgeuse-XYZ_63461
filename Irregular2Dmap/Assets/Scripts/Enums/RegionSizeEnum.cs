using System.Xml.Serialization;

namespace Assets.Scripts.Enums
{
    public enum RegionSizeEnum
    {
        [XmlEnum("Small")]
        Small = 80,
        [XmlEnum("Medium")]
        Medium = 100,
        [XmlEnum("Big")]
        Big = 120
    }
}