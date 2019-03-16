using Assets.Scripts.Controllers.Panels.RegionUIPanels;
using Assets.Scripts.Controllers.Panels.RegionUIPanels.LeftPanel;
using Assets.Scripts.Readers;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class RegionUIController : MonoBehaviour
    {
        public GameObject Sprites;

        private SpritesReader spritesReader;

        void Awake()
        {
            spritesReader = Sprites.GetComponent<SpritesReader>();
        }

        public void GoBackToWorldMap()
        {
            GameController.Instance.LoadWorldMapView();
        }

        public void SetupView()
        {
            var regionModel = SelectedRegionsController.SelectedRegionObjects.FirstOrDefault().RegionModel;

            GetComponentInChildren<TopPanelController>().SetupPanel(regionModel, spritesReader);
            GetComponentInChildren<LeftPanelController>().SetupPanel(regionModel, spritesReader);
        }
    }
}
