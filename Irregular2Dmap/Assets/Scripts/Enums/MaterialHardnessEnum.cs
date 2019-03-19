using System.Xml.Serialization;

public enum MaterialHardnessEnum
{
    //TODO: IMPLEMENT https://flexiblelearning.auckland.ac.nz/rocks_minerals/minerals/hardness.html
    [XmlEnum("Soft")]
    Soft = 1,
    [XmlEnum("Medium")]
    Medium = 2,
    [XmlEnum("Hard")]
    Hard = 3
}