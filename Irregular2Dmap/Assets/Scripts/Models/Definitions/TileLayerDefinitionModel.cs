namespace Assets.Scripts.Models.Definitions
{
    public class TileLayerDefinitionModel
    {
        public string Name;
        public MaterialHardnessEnum MaterialHardness;

        public TileLayerDefinitionModel(string name, MaterialHardnessEnum materialHardness)
        {
            Name = name;
            MaterialHardness = materialHardness;
        }
    }
}
