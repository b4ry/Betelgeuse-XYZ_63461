using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class RegionController : MonoBehaviour
    {
        private const string UnknownValue = "???";
        private const string UnchartedLandName = "Uncharted land";
        public GameObject regionSummaryPanel;
        public GameObject regionSummaryPanelLabel;
        public GameObject regionSummaryPanelSize;
        public GameObject regionSummaryPanelBiomes;

        public Button regionSummaryPanelExploreButton;

        public Sprite regionSprite;
        public Sprite regionOutlineSprite;
        public Sprite regionFogOfWarSprite;
        public Sprite regionFogOfWarOutlineSprite;

        private bool regionSelected = false;
        private bool isInitial = false;

        private SpriteRenderer spriteRenderer;
        private RegionModel regionModel;

        private TextMeshProUGUI regionSummaryPanelLabelTextMesh;
        private TextMeshProUGUI regionSummaryPanelSizeTextMesh;
        private TextMeshProUGUI regionSummaryPanelBiomesTextMesh;

        void Awake()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            regionSummaryPanelLabelTextMesh = regionSummaryPanelLabel.GetComponent<TextMeshProUGUI>();
            regionSummaryPanelSizeTextMesh = regionSummaryPanelSize.GetComponent<TextMeshProUGUI>();
            regionSummaryPanelBiomesTextMesh = regionSummaryPanelBiomes.GetComponent<TextMeshProUGUI>();
        }

        void Start()
        {
            var path = $"/Assets/RegionsDefinition/{gameObject.name}.txt";
            var directory = Directory.GetCurrentDirectory();
            var fullPath = string.Concat(directory, path);

            string[] regionDefinition = File.ReadAllLines(fullPath);
            string[] neighbourNames = regionDefinition[3].Split(';');

            var neighbourRegions = GameController.instance.Regions.Where(region => neighbourNames.Contains(region.name)).ToList();

            regionModel = new RegionModel(regionDefinition[0], (Size)int.Parse(regionDefinition[1]), (Biome)int.Parse(regionDefinition[2]), neighbourRegions);

            if (isInitial)
            {
                gameObject.SetActive(true);
                regionModel.Visited = true;

                regionSelected = true;

                spriteRenderer.sprite = regionOutlineSprite;
                SetupRegionSummaryPanel(regionModel.Name, regionModel.Size.ToString(), regionModel.Biomes.ToString(), false);

                SelectedRegionsController.instance.SelectedRegionObjects.Add(this);
            }
            else
            {
                var isInitialTileNeighbour = regionModel.NeighbourRegions.FirstOrDefault(nr => nr.GetComponent<RegionController>().isInitial);

                if (isInitialTileNeighbour != null)
                {
                    gameObject.SetActive(true);

                    spriteRenderer.sprite = regionFogOfWarSprite;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        private void SetupRegionSummaryPanel(string regionName, string size, string biomes, bool exploreButtonInteractable)
        {
            regionSummaryPanelLabelTextMesh.SetText(regionName);
            regionSummaryPanelSizeTextMesh.SetText("Size: " + size);
            regionSummaryPanelBiomesTextMesh.SetText("Biomes: " + biomes);

            regionSummaryPanelExploreButton.interactable = exploreButtonInteractable;

            regionSummaryPanel.SetActive(true);
        }

        void OnMouseDown()
        {
            if (regionSelected) // DESELECT region
            {
                if (regionModel.Visited)
                {
                    spriteRenderer.sprite = regionSprite;
                }
                else
                {
                    spriteRenderer.sprite = regionFogOfWarSprite;
                }

                regionSelected = false;

                regionSummaryPanel.SetActive(false);

                SelectedRegionsController.instance.SelectedRegionObjects.Remove(this);

                if (regionModel.Visited)
                {
                    foreach (var neighbourRegion in regionModel.NeighbourRegions)
                    {
                        var neighbourRegionController = neighbourRegion.GetComponent<RegionController>();

                        if (neighbourRegionController.regionModel.Visited)
                        {
                            neighbourRegion.SetActive(true);
                        }
                        else
                        {
                            neighbourRegion.SetActive(false);
                        }
                    }
                }
            }
            else
            {
                SelectRegion();
            }
        }

        private void SelectRegion()
        {
            regionSelected = true;

            if (regionModel.Visited)
            {
                spriteRenderer.sprite = regionOutlineSprite;

                SetupRegionSummaryPanel(regionModel.Name, regionModel.Size.ToString(), regionModel.Biomes.ToString(), false);
            }
            else
            {
                spriteRenderer.sprite = regionFogOfWarOutlineSprite;

                SetupRegionSummaryPanel(UnchartedLandName, UnknownValue, UnknownValue, true);

                regionSummaryPanelExploreButton.onClick.RemoveAllListeners();
                regionSummaryPanelExploreButton.onClick.AddListener(delegate
                {
                    Explore();
                });
            }

            foreach (var selectedRegion in SelectedRegionsController.instance.SelectedRegionObjects)
            {
                selectedRegion.DeselectRegion();
            }

            if (regionModel.Visited)
            {
                foreach (var neighbourRegion in regionModel.NeighbourRegions)
                {
                    var neighbourRegionController = neighbourRegion.GetComponent<RegionController>();

                    if (!neighbourRegionController.regionModel.Visited)
                    {
                        neighbourRegionController.PlaceFogOfWar();
                    }
                }
            }

            foreach(var region in GameController.instance.Regions)
            {
                var regionController = region.GetComponent<RegionController>();

                if(!regionController.regionModel.Visited && !regionController.regionSelected)
                {
                    if (regionModel.Visited && regionModel.NeighbourRegions.Contains(region))
                    {
                        continue;
                    }
                    else
                    {
                        region.SetActive(false);
                    }
                }
            }

            SelectedRegionsController.instance.SelectedRegionObjects.Clear();
            SelectedRegionsController.instance.SelectedRegionObjects.Add(this);
        }

        private void DeselectRegion()
        {
            if (regionModel.Visited)
            {
                spriteRenderer.sprite = regionSprite;
            }
            else
            {
                spriteRenderer.sprite = regionFogOfWarSprite;
            }

            regionSelected = false;
        }

        private void PlaceFogOfWar()
        {
            gameObject.SetActive(true);

            GetComponent<SpriteRenderer>().sprite = regionFogOfWarSprite;
        }

        public void SetInitial()
        {
            isInitial = true;
        }

        public void Explore()
        {
            regionModel.Visited = true;

            spriteRenderer.sprite = regionOutlineSprite;

            regionSummaryPanelLabelTextMesh.SetText(regionModel.Name);
            regionSummaryPanelSizeTextMesh.SetText("Size: " + regionModel.Size.ToString());
            regionSummaryPanelBiomesTextMesh.SetText("Biomes: " + regionModel.Biomes.ToString());

            if (regionModel.Visited)
            {
                foreach (var neighbourRegion in regionModel.NeighbourRegions)
                {
                    var neighbourRegionController = neighbourRegion.GetComponent<RegionController>();

                    if (!neighbourRegionController.regionModel.Visited)
                    {
                        neighbourRegionController.PlaceFogOfWar();
                    }
                }
            }

            regionSummaryPanelExploreButton.interactable = false;
        }
    }
}
