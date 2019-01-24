using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class RegionController : MonoBehaviour
    {
        private const string UnknownValue = "???";
        private const string UnchartedLandName = "Uncharted land";

        public Sprite regionSprite;
        public Sprite regionOutlineSprite;
        public Sprite regionFogOfWarSprite;
        public Sprite regionFogOfWarOutlineSprite;

        private bool regionSelected = false;
        private bool isInitial = false;

        private SpriteRenderer spriteRenderer;
        private RegionModel regionModel;

        void Awake()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            gameObject.AddComponent<PolygonCollider2D>();
        }
        
        void Start()
        {
            var path = $"/Assets/RegionsDefinition/{gameObject.name}.txt";
            var directory = Directory.GetCurrentDirectory();
            var fullPath = string.Concat(directory, path);

            string[] regionDefinition = File.ReadAllLines(fullPath);
            string[] neighbourNames = regionDefinition[3].Split(';');

            var neighbourRegions = GameController.Instance.Regions.Where(region => neighbourNames.Contains(region.name)).ToList();

            regionModel = new RegionModel(regionDefinition[0], (Size)int.Parse(regionDefinition[1]), (Biome)int.Parse(regionDefinition[2]), neighbourRegions);

            if (isInitial)
            {
                gameObject.SetActive(true);
                regionModel.Visited = true;

                regionSelected = true;

                spriteRenderer.sprite = regionOutlineSprite;
                RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(regionModel.Name, regionModel.Size.ToString(), regionModel.Biomes.ToString(), false);

                SelectedRegionsController.Instance.SelectedRegionObjects.Add(this);
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

                RegionSummaryPanelManager.Instance.SetActive(false);

                SelectedRegionsController.Instance.SelectedRegionObjects.Remove(this);

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

                RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(regionModel.Name, regionModel.Size.ToString(), regionModel.Biomes.ToString(), false);
            }
            else
            {
                spriteRenderer.sprite = regionFogOfWarOutlineSprite;

                RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(UnchartedLandName, UnknownValue, UnknownValue, true);
                RegionSummaryPanelManager.Instance.AddButtonListener(delegate
                {
                    ChartRegion();
                });
            }

            foreach (var selectedRegion in SelectedRegionsController.Instance.SelectedRegionObjects)
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

            foreach (var region in GameController.Instance.Regions)
            {
                var regionController = region.GetComponent<RegionController>();

                if (!regionController.regionModel.Visited && !regionController.regionSelected)
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

            SelectedRegionsController.Instance.SelectedRegionObjects.Clear();
            SelectedRegionsController.Instance.SelectedRegionObjects.Add(this);
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

        public void ChartRegion()
        {
            regionModel.Visited = true;

            spriteRenderer.sprite = regionOutlineSprite;

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

            RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(regionModel.Name, regionModel.Size.ToString(), regionModel.Biomes.ToString(), false);
        }
    }
}
