using Assets.Scripts.Models;
using Assets.Scripts.Models.Definitions;
using Assets.Scripts.Readers;
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
        public GameObject Sprites;

        public GameObject ExplorationGameAreaTilePrefab;

        private SortedList<int, int> ranges = new SortedList<int, int>();
        private List<GameObject> tiles = new List<GameObject>();
        private SpritesReader spritesReader;

        void Awake()
        {
            for (int i = 10, j = 2; i <= 120; i += 10, j++)
            {
                ranges.Add(i, j);
            }

            spritesReader = Sprites.GetComponent<SpritesReader>();
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

            while (drawnTilesNumber < tilesToDrawNumber)
            {
                var residual = drawnTilesNumber % tilesNumber;
                var xPosition = residual * TileSideSize;

                if (residual != 0)
                {
                    xPosition += residual * TileOffset;
                }

                var fullResidual = drawnTilesNumber / tilesNumber;
                var yPosition = 0;

                if (fullResidual > 0)
                {
                    yPosition = fullResidual * TileSideSize + fullResidual * TileOffset;
                }

                var explorationGameAreaTile = Instantiate(ExplorationGameAreaTilePrefab, gameObject.transform);
                explorationGameAreaTile.transform.localPosition += new Vector3(xPosition, yPosition * (-1));
                tiles.Add(explorationGameAreaTile);

                if (!biomeModel.TilesInitialized)
                {
                    //TODO: THIS ALGORITHM SHOULD BE CHANGED
                    var tilesGeneratedNumber = GameController.Instance.RNG.Next(1, 11);

                    biomeModel.Tiles.Add(new List<ExplorationGameLayerModel>());

                    for (int i = tilesGeneratedNumber; i >= 0; i--)
                    {
                        TileLayerDefinitionModel tileLayerDefinitionModel;

                        if (i >= 5)
                        {
                            tileLayerDefinitionModel = DefinitionsController.Instance.TileLayerDefinitions["Grass"];
                        }
                        else if (i >= 1)
                        {
                            tileLayerDefinitionModel = DefinitionsController.Instance.TileLayerDefinitions["Rocks"];
                        }
                        else
                        {
                            tileLayerDefinitionModel = DefinitionsController.Instance.TileLayerDefinitions["Igneous"];
                        }

                        var tileLayerModel = new TileLayerModel(tileLayerDefinitionModel.Name, tileLayerDefinitionModel.MaterialHardness);
                        var explorationGameLayerModel = new ExplorationGameLayerModel(tileLayerModel);

                        biomeModel.Tiles[drawnTilesNumber].Add(explorationGameLayerModel);
                    }
                }
                
                explorationGameAreaTile.GetComponent<ExplorationGameTileController>().SetupTile(biomeModel.Tiles[drawnTilesNumber], spritesReader);
                drawnTilesNumber++;
            }
            
            biomeModel.TilesInitialized = true;
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
