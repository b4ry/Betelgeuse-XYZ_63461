using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Models;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers
{
    public class RegionController : MonoBehaviour, IPointerClickHandler
    {
        private const string UnknownValue = "???";

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
        }

        void Start()
        {
            var path = $"/Assets/RegionsDefinition/{GameController.Instance.MapName}/{gameObject.name}.txt";
            var directory = Directory.GetCurrentDirectory();
            var fullPath = string.Concat(directory, path);

            string[] regionDefinition = File.ReadAllLines(fullPath);
            string[] biomeRarities = regionDefinition[3].Split(';');
            string[] neighbourNames = regionDefinition[4].Split(';');

            var neighbourRegions = GameController.Instance.RegionObjects.Where(region => neighbourNames.Contains(region.name)).ToList();

            regionModel = new RegionModel(regionDefinition[0], (RegionSizeEnum)int.Parse(regionDefinition[1]), (BiomeEnum)int.Parse(regionDefinition[2]), biomeRarities, neighbourRegions);

            if (isInitial)
            {
                gameObject.SetActive(true);
                regionModel.Visited = true;

                regionSelected = true;

                spriteRenderer.sprite = regionOutlineSprite;
                RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(regionModel, false);

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

                    spriteRenderer.sprite = regionFogOfWarSprite;
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

                RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(regionModel, false);
            }
            else
            {
                spriteRenderer.sprite = regionFogOfWarOutlineSprite;
                
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

            RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(regionModel, false);
        }
    }
}
