using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels.ExplorationPanel
{
    public class ExplorationGameTileController : MonoBehaviour
    {
        private List<ExplorationGameLayerModel> layers;

        public void SetupTile(List<ExplorationGameLayerModel> tileLayers)
        {
            layers = tileLayers;
            gameObject.GetComponent<Image>().sprite = layers.FirstOrDefault().LayerSprite;
        }

        public void OnMouseDown()
        {
            Debug.Log("---------------------------------------------------------------------");
            Debug.Log("Layer type: " + layers.FirstOrDefault().TileLayerModel.Name);
            Debug.Log("Layers number: " + layers.Count);
        }
    }
}
