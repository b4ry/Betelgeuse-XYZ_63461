using Assets.Scripts.Enums;
using Assets.Scripts.Managers.WorldMap;
using Assets.Scripts.Models;
using Assets.Scripts.Readers;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers
{
    public class RegionController : MonoBehaviour
    {
        private const string UnknownValue = "???";

        public RegionModel RegionModel { get; set; }

        public Sprite RegionSprite { get; set; }
        public Sprite RegionOutlineSprite { get; set; }
        public Sprite RegionFogOfWarSprite { get; set; }
        public Sprite RegionFogOfWarOutlineSprite { get; set; }

        private bool regionSelected = false;
        private bool isInitial = false;

        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            var path = $"/Assets/Definitions/RegionsDefinition/{GameController.Instance.MapName}/{gameObject.name}.txt";

            string[] regionDefinition = FileReader.ReadFile(path);
            string[] biomeNames = regionDefinition[2].Split(';');
            string[] neighbourNames = regionDefinition[3].Split(';');

            var neighbourRegions = GameController.Instance.RegionObjects.Where(region => neighbourNames.Contains(region.name)).ToList();

            Enum.TryParse(regionDefinition[1], out RegionSizeEnum regionSizeEnum);

            RegionModel = new RegionModel(regionDefinition[0], regionSizeEnum, biomeNames, neighbourRegions);

            if (isInitial)
            {
                gameObject.SetActive(true);
                RegionModel.Visited = true;

                regionSelected = true;

                spriteRenderer.sprite = RegionOutlineSprite;

                RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(RegionModel, false);
                SelectedRegionsController.Instance.SelectedRegionObjects.Add(this);
                RegionSummaryPanelManager.Instance.SetupEnterButton(delegate
                {
                    EnterRegion();
                });
            }
            else
            {
                var isInitialTileNeighbour = RegionModel.NeighbourRegions.FirstOrDefault(nr => nr.GetComponent<RegionController>().isInitial);

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

        public void SetInitial()
        {
            isInitial = true;
        }

        public void OnMouseDown()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (regionSelected) // DESELECT region
                {
                    if (RegionModel.Visited)
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

                    if (RegionModel.Visited)
                    {
                        foreach (var neighbourRegion in RegionModel.NeighbourRegions)
                        {
                            var neighbourRegionController = neighbourRegion.GetComponent<RegionController>();

                            if (neighbourRegionController.RegionModel.Visited)
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
            } else
            {
                Debug.Log("UI");
            }
        }

        private void SelectRegion()
        {
            regionSelected = true;

            if (RegionModel.Visited)
            {
                spriteRenderer.sprite = RegionOutlineSprite;

                RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(RegionModel, false);
                RegionSummaryPanelManager.Instance.SetupEnterButton(delegate
                {
                    EnterRegion();
                });
            }
            else
            {
                spriteRenderer.sprite = RegionFogOfWarOutlineSprite;
                
                RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(RegionModel, true);
                RegionSummaryPanelManager.Instance.SetupChartButton(delegate
                {
                    ChartRegion();
                });
            }

            foreach (var selectedRegion in SelectedRegionsController.Instance.SelectedRegionObjects)
            {
                selectedRegion.DeselectRegion();
            }

            if (RegionModel.Visited)
            {
                foreach (var neighbourRegion in RegionModel.NeighbourRegions)
                {
                    var neighbourRegionController = neighbourRegion.GetComponent<RegionController>();

                    if (!neighbourRegionController.RegionModel.Visited)
                    {
                        neighbourRegionController.PlaceFogOfWar();
                    }
                }
            }

            foreach (var region in GameController.Instance.RegionObjects)
            {
                var regionController = region.GetComponent<RegionController>();

                if (!regionController.RegionModel.Visited && !regionController.regionSelected)
                {
                    if (RegionModel.Visited && RegionModel.NeighbourRegions.Contains(region))
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
            if (RegionModel.Visited)
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

        private void EnterRegion()
        {
            GameController.Instance.LoadRegionView();
        }

        private void ChartRegion()
        {
            RegionModel.Visited = true;
            spriteRenderer.sprite = RegionOutlineSprite;

            if (RegionModel.Visited)
            {
                foreach (var neighbourRegion in RegionModel.NeighbourRegions)
                {
                    var neighbourRegionController = neighbourRegion.GetComponent<RegionController>();

                    if (!neighbourRegionController.RegionModel.Visited)
                    {
                        neighbourRegionController.PlaceFogOfWar();
                    }
                }
            }

            RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(RegionModel, false);
        }
    }
}
