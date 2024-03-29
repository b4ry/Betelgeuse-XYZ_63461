﻿using Assets.Scripts.Managers.WorldMap;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Definitions;
using System.IO;
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

        public void DefineModel()
        {
            var path = $"{Directory.GetCurrentDirectory()}/Assets/Definitions/Regions/{GameController.Instance.MapName}/{gameObject.name}.xml";

            var regionDefinition = RegionDefinitionModel.Load(path);
            var neighbourRegions = GameController.Instance.RegionObjects.Where(region => regionDefinition.Neighbours.Any(rd => rd.Name == region.name)).ToList();

            RegionModel = new RegionModel(regionDefinition.Name, regionDefinition.Size, regionDefinition.Biomes, neighbourRegions);

            gameObject.SetActive(true);

            spriteRenderer.sprite = RegionFogOfWarSprite;
            //gameObject.SetActive(false);

            //if (isInitial)
            //{
            //    //gameObject.SetActive(true);
            //    //RegionModel.Visited = true;

            //    //regionSelected = true;

            //    spriteRenderer.sprite = RegionOutlineSprite;

            //    //RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(RegionModel, false);
            //    //SelectedRegionsController.SelectedRegionObjects.Add(this);
            //    RegionSummaryPanelManager.Instance.SetupEnterButton(delegate
            //    {
            //        EnterRegion();
            //    });
            //}
            //else
            //{
            //    var isInitialTileNeighbour = RegionModel.NeighbourRegions.FirstOrDefault(nr => nr.GetComponent<RegionController>().isInitial);

            //    if (isInitialTileNeighbour != null)
            //    {
            //        gameObject.SetActive(true);

            //        spriteRenderer.sprite = RegionFogOfWarSprite;
            //    }
            //    else
            //    {
            //        gameObject.SetActive(false);

            //        spriteRenderer.sprite = RegionFogOfWarSprite;
            //    }
            //}

            gameObject.AddComponent<PolygonCollider2D>();
        }

        public void SetInitial()
        {
            isInitial = true;
        }

        public void SetActive()
        {
            SelectedRegionsController.SelectedRegionObjects.Clear();

            gameObject.SetActive(true);

            var regionsVisited = GameController.Instance.ActivePlayer.PlayerManager.VisitedRegions;
            if (!regionsVisited.Contains(RegionModel.Name))
            {
                regionsVisited.Add(RegionModel.Name);
            }

            regionSelected = true;

            spriteRenderer.sprite = RegionOutlineSprite;

            RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(RegionModel, false);
            SelectedRegionsController.SelectedRegionObjects.Add(this);
            RegionSummaryPanelManager.Instance.SetupEnterButton(delegate
            {
                EnterRegion();
            });
        }

        public void SetCharted()
        {
            spriteRenderer.sprite = RegionSprite;
        }

        public void OnMouseDown()
        {
            var regionsVisited = GameController.Instance.ActivePlayer.PlayerManager.VisitedRegions;

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (regionSelected) // DESELECT region
                {
                    if (regionsVisited.Contains(RegionModel.Name))
                    {
                        spriteRenderer.sprite = RegionSprite;
                    }
                    else
                    {
                        spriteRenderer.sprite = RegionFogOfWarSprite;
                    }

                    regionSelected = false;

                    RegionSummaryPanelManager.Instance.SetActive(false);

                    SelectedRegionsController.SelectedRegionObjects.Remove(this);

                    //if (regionsVisited.Contains(RegionModel.Name))
                    //{
                    //    foreach (var neighbourRegion in RegionModel.NeighbourRegions)
                    //    {
                    //        var neighbourRegionController = neighbourRegion.GetComponent<RegionController>();

                    //        if (neighbourRegionController.RegionModel.Visited)
                    //        {
                    //            neighbourRegion.SetActive(true);
                    //        }
                    //        else
                    //        {
                    //            neighbourRegion.SetActive(false);
                    //        }
                    //    }
                    //}
                }
                else
                {
                    SelectRegion();
                }
            }
            else
            {
                Debug.Log("UI");
            }
        }

        public void PlaceFogOfWar()
        {
            gameObject.SetActive(true);

            regionSelected = false;
            spriteRenderer.sprite = RegionFogOfWarSprite;
        }

        private void SelectRegion()
        {
            regionSelected = true;
            var regionsVisited = GameController.Instance.ActivePlayer.PlayerManager.VisitedRegions;

            if (regionsVisited.Contains(RegionModel.Name))
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

            foreach (var selectedRegion in SelectedRegionsController.SelectedRegionObjects)
            {
                selectedRegion.DeselectRegion();
            }

            //if (RegionModel.Visited)
            //{
            //    foreach (var neighbourRegion in RegionModel.NeighbourRegions)
            //    {
            //        var neighbourRegionController = neighbourRegion.GetComponent<RegionController>();

            //        if (!neighbourRegionController.RegionModel.Visited)
            //        {
            //            neighbourRegionController.PlaceFogOfWar();
            //        }
            //    }
            //}

            //foreach (var region in GameController.Instance.RegionObjects)
            //{
            //    var regionController = region.GetComponent<RegionController>();

            //    if (!regionController.RegionModel.Visited && !regionController.regionSelected)
            //    {
            //        if (RegionModel.Visited && RegionModel.NeighbourRegions.Contains(region))
            //        {
            //            continue;
            //        }
            //        else
            //        {
            //            region.SetActive(false);
            //        }
            //    }
            //}

            SelectedRegionsController.SelectedRegionObjects.Clear();
            SelectedRegionsController.SelectedRegionObjects.Add(this);
        }

        private void DeselectRegion()
        {
            var regionsVisited = GameController.Instance.ActivePlayer.PlayerManager.VisitedRegions;

            if (regionsVisited.Contains(RegionModel.Name))
            {
                spriteRenderer.sprite = RegionSprite;
            }
            else
            {
                spriteRenderer.sprite = RegionFogOfWarSprite;
            }

            regionSelected = false;
        }

        private void EnterRegion()
        {
            GameController.Instance.LoadRegionView();
        }

        private void ChartRegion()
        {
            var regionsVisited = GameController.Instance.ActivePlayer.PlayerManager.VisitedRegions;
            if (!regionsVisited.Contains(RegionModel.Name))
            {
                regionsVisited.Add(RegionModel.Name);
            }

            spriteRenderer.sprite = RegionOutlineSprite;

            //    foreach (var neighbourRegion in RegionModel.NeighbourRegions)
            //    {
            //        var neighbourRegionController = neighbourRegion.GetComponent<RegionController>();

            //        if (!neighbourRegionController.RegionModel.Visited)
            //        {
            //            neighbourRegionController.PlaceFogOfWar();
            //        }
            //    }
            //}

            RegionSummaryPanelManager.Instance.SetupRegionSummaryPanel(RegionModel, false);
        }
    }
}
