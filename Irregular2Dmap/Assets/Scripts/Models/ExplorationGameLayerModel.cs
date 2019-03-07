using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    public struct ExplorationGameLayerModel
    {
        public TileLayerModel TileLayerModel;
        public List<IFoundableObjectModel> Items;

        public ExplorationGameLayerModel(TileLayerModel tileLayerModel)
        {
            TileLayerModel = tileLayerModel;
            Items = new List<IFoundableObjectModel>();
        }
    }
}
