using Assets.Scripts.Controllers.Panels.RegionUIPanels;
using Assets.Scripts.Controllers.Panels.RegionUIPanels.LeftPanel;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class RegionUIController : MonoBehaviour
    {
        public void GoBackToWorldMap()
        {
            GameController.Instance.LoadWorldMapView();
        }

        public void SetupView()
        {
            var regionModel = SelectedRegionsController.Instance.SelectedRegionObjects.FirstOrDefault().RegionModel;

            GetComponentInChildren<TopPanelController>().SetupPanel(regionModel);
            GetComponentInChildren<LeftPanelController>().SetupPanel(regionModel);
        }
    }
}
