using Assets.Scripts.Models;
using Assets.Scripts.Models.Definitions;
using Assets.Scripts.Readers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.RegionUI.ExplorationPanel
{
    public class ExplorationGameAreaPanelController : MonoBehaviour
    {
        private const int TileSideSize = 36;
        private const int TileOffset = 2;
        private const int TileXPadding = 20;
        private const int TileYPadding = 10;
        private const string GrassLayer = "Grass";
        private const string RocksLayer = "Rocks";
        private const string IgneousLayer = "Igneous";

        public GameObject SelectedTile;
        public GameObject Sprites;
        public GameObject Dig;

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

        void OnDisable()
        {
            Dig.SetActive(false);
        }

        public void SetupPanel(BiomeModel biomeModel)
        {
            ClearPanel();

            gameObject.SetActive(true);
            Dig.SetActive(true);
            Dig.GetComponent<Button>().interactable = false;
            SelectedTile.SetActive(false);

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
                    BuildLayers(biomeModel, drawnTilesNumber);
                }

                var holeSprite = spritesReader.LayersImageSprites.FirstOrDefault(lis => lis.name.Contains("Hole"));
                explorationGameAreaTile.GetComponent<ExplorationGameTileController>().SetupTile(biomeModel.Tiles[drawnTilesNumber], SelectedTile, Dig, holeSprite);
                drawnTilesNumber++;
            }
            
            biomeModel.TilesInitialized = true;

            SelectedTile.transform.SetAsLastSibling();
        }

        private void BuildLayers(BiomeModel biomeModel, int currentTileNumber)
        {
            //TODO: THIS ALGORITHM SHOULD BE CHANGED
            var tilesGeneratedNumber = GameController.Instance.Rng.Next(1, 11);

            biomeModel.Tiles.Add(new List<ExplorationGameLayerModel>());

            for (int i = tilesGeneratedNumber; i >= 0; i--)
            {
                TileLayerDefinitionModel tileLayerDefinitionModel;

                if (i >= 5)
                {
                    tileLayerDefinitionModel = DefinitionsController.Instance.TileLayerDefinitions[GrassLayer];
                }
                else if (i >= 1)
                {
                    tileLayerDefinitionModel = DefinitionsController.Instance.TileLayerDefinitions[RocksLayer];
                }
                else
                {
                    tileLayerDefinitionModel = DefinitionsController.Instance.TileLayerDefinitions[IgneousLayer];
                }

                var tileLayerModel = new TileLayerModel(tileLayerDefinitionModel.Name, tileLayerDefinitionModel.MaterialHardness);
                var sprite = spritesReader.LayersImageSprites.OrderBy(x => GameController.Instance.Rng.Next())
                                      .FirstOrDefault(s => s.name.Contains(tileLayerModel.Name));

                var explorationGameLayerModel = new ExplorationGameLayerModel(tileLayerModel, sprite);

                biomeModel.Tiles[currentTileNumber].Add(explorationGameLayerModel);
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
