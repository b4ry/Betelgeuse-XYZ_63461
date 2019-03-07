namespace Assets.Scripts.Models
{
    public class TileLayerModel
    {
        public string Name;
        public MaterialHardnessEnum MaterialHardness;

        public TileLayerModel(string name, MaterialHardnessEnum materialHardness)
        {
            Name = name;
            MaterialHardness = materialHardness;
        }
    }
}
