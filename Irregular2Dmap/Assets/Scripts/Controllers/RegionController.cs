using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers
{
    public class RegionController : MonoBehaviour, IPointerClickHandler
    {
        private const string UnknownValue = "???";

        public Sprite RegionSprite { get; set; }
        public Sprite RegionOutlineSprite { get; set; }
        public Sprite RegionFogOfWarSprite { get; set; }
        public Sprite RegionFogOfWarOutlineSprite { get; set; }

        private bool regionSelected = false;
        private bool isInitial = false;

        private SpriteRenderer spriteRenderer;
        private RegionModel regionModel;

        void Awake()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            var path = $"/Assets/RegionsDefinition/{GameController.Instance.MapName}/{gameObject.name}.txt";
            var directory = Directory.GetCurrentDirectory();
            var fullPath = string.Concat(directory, path);

            string[] regionDefinition = File.ReadAllLines(fullPath);
            string[] biomeNames = regionDefinition[2].Split(';');
            string[] neighbourNames = regionDefinition[3].Split(';');

            var neighbourRegions = GameController.Instance.RegionObjects.Where(region => neighbourNames.Contains(region.name)).ToList();

            Enum.TryParse(regionDefinition[1], out RegionSizeEnum regionSizeEnum);

            regionModel = new RegionModel(regionDefinition[0], regionSizeEnum, biomeNames, neighbourRegions);

            if (isInitial)
            {
                gameObject.SetActive(true);
                regionModel.Visited = true;

                regionSelected = true;

                spriteRenderer.sprite = RegionOutlineSprite;
                RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(regionModel, false);

                SelectedRegionsController.Instance.SelectedRegionObjects.Add(this);
            }
            else
            {
                var isInitialTileNeighbour = regionModel.NeighbourRegions.FirstOrDefault(nr => nr.GetComponent<RegionController>().isInitial);

                if (isInitialTileNeighbour != null)
                {
                    gameObject.SetActive(true);

                    spriteRenderer.sprite = RegionFogOfWarSprite;
                }
                else
                {
                    gameObject.SetActive(false);

                    spriteRenderer.sprite = RegionFogOfWarSprite;
                }
            }

            gameObject.AddComponent<PolygonCollider2D>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (regionSelected) // DESELECT region
            {
                if (regionModel.Visited)
                {
                    spriteRenderer.sprite = RegionSprite;
                }
                else
                {
                    spriteRenderer.sprite = RegionFogOfWarSprite;
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

                Debug.Log(regionModel.Biomes[0].Area);
            }
        }

        private void SelectRegion()
        {
            regionSelected = true;

            if (regionModel.Visited)
            {
                spriteRenderer.sprite = RegionOutlineSprite;

                RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(regionModel, false);
            }
            else
            {
                spriteRenderer.sprite = RegionFogOfWarOutlineSprite;
                
                RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(regionModel, true);
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

            foreach (var region in GameController.Instance.RegionObjects)
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
                spriteRenderer.sprite = RegionSprite;
            }
            else
            {
                spriteRenderer.sprite = RegionFogOfWarSprite;
            }

            regionSelected = false;
        }

        private void PlaceFogOfWar()
        {
            gameObject.SetActive(true);

            GetComponent<SpriteRenderer>().sprite = RegionFogOfWarSprite;
        }

        public void SetInitial()
        {
            isInitial = true;
        }

        public void ChartRegion()
        {
            regionModel.Visited = true;

            spriteRenderer.sprite = RegionOutlineSprite;

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

            RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(regionModel, false);
        }
    }
}
