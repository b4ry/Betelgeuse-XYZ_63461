using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels.ExplorationPanel
{
    public class ExplorationGameAreaPanelController : MonoBehaviour
    {
        private const int TileSideSize = 36;
        private const int TileOffset = 2;
        private const int TileXPadding = 20;
        private const int TileYPadding = 10;

        public GameObject ExplorationGamePanel;
        public GameObject ExplorationGameAreaPanel;

        private SortedList<int, int> ranges = new SortedList<int, int>();

        void Awake()
        {
            for(int i = 10, j = 2; i <= 120; i += 10, j++)
            {
                ranges.Add(i, j);
            }
        }

        public void SetupPanel(BiomeModel biomeModel)
        {
            gameObject.SetActive(true);

            var tilesNumber = ranges.FirstOrDefault(r => r.Key >= biomeModel.Area).Value;

            var width = (tilesNumber * TileSideSize + (tilesNumber - 1) * TileOffset + TileXPadding);
            var height = (tilesNumber * TileSideSize + (tilesNumber - 1) * TileOffset + TileYPadding);

            ExplorationGameAreaPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        }
    }
}
