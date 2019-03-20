using System.Xml.Serialization;

namespace Assets.Scripts.Enums
{
    public enum BuildingTypeEnum
    {
        [XmlEnum("Economic")]
        Economic,
        [XmlEnum("Military")]
        Military,
        [XmlEnum("Science")]
        Science
    }
}
