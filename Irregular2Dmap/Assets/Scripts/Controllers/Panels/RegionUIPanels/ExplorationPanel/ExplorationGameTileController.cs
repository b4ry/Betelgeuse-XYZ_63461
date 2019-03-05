using Assets.Scripts.Models;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels.ExplorationPanel
{
    public class ExplorationGameTileController : MonoBehaviour
    {
        private List<ExplorationGameTileModel> layers;

        public void SetupTile(List<ExplorationGameTileModel> tileLayers)
        {
            layers = tileLayers;
        }

        public void OnMouseDown()
        {
            Debug.Log("Layers number: " + layers.Capacity);
        }
    }
}
