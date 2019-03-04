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

        public GameObject ExplorationGameAreaTilePrefab;

        private SortedList<int, int> ranges = new SortedList<int, int>();
        private List<GameObject> tiles = new List<GameObject>();

        void Awake()
        {
            for(int i = 10, j = 2; i <= 120; i += 10, j++)
            {
                ranges.Add(i, j);
            }
        }

        public void SetupPanel(BiomeModel biomeModel)
        {
            ClearPanel();

            gameObject.SetActive(true);

            var tilesNumber = ranges.FirstOrDefault(r => r.Key >= biomeModel.Area).Value;

            var width = (tilesNumber * TileSideSize + (tilesNumber - 1) * TileOffset + TileXPadding);
            var height = (tilesNumber * TileSideSize + (tilesNumber - 1) * TileOffset + TileYPadding);

            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

            int drawnTilesNumber = 0;
            int tilesToDrawNumber = tilesNumber * tilesNumber;

            while(drawnTilesNumber < tilesToDrawNumber)
            {
                var xPosition = drawnTilesNumber % tilesNumber * TileSideSize;

                if(drawnTilesNumber % tilesNumber != 0)
                {
                    xPosition += drawnTilesNumber % tilesNumber * TileOffset;
                }

                var yPosition = 0;

                if(drawnTilesNumber / tilesNumber > 0)
                {
                    yPosition = drawnTilesNumber / tilesNumber * TileSideSize + drawnTilesNumber / tilesNumber * TileOffset;
                }

                var explorationGameAreaTile = Instantiate(ExplorationGameAreaTilePrefab, gameObject.transform);
                explorationGameAreaTile.transform.localPosition += new Vector3(xPosition, yPosition * (-1));
                tiles.Add(explorationGameAreaTile);

                drawnTilesNumber++;
            }
        }

        private void ClearPanel()
        {
            foreach (var tile in tiles)
            {
                Destroy(tile);
            }

            tiles.Clear();
        }
    }
}
