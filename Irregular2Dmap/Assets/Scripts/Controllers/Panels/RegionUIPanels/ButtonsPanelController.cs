using Assets.Scripts.Controllers.Panels.RegionUIPanels.ExplorationPanel;
using Assets.Scripts.Enums;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels
{
    public class ButtonsPanelController : MonoBehaviour
    {
        public GameObject SeparationPanel;
        public GameObject ExplorationPanel;
        public GameObject BuildingPanel;
        public GameObject DevelopShipPanel;
        public GameObject BuildButton;
        public GameObject DevelopShipButton;

        private ExplorationPanelController explorationPanelController;

        void Awake()
        {
            explorationPanelController = ExplorationPanel.GetComponent<ExplorationPanelController>();
        }

        void Start()
        {
            var race = GameController.Instance.PlayerManager.Race;

            if(race == RaceEnum.Human)
            {
                BuildButton.SetActive(true);
            }
            else if(race == RaceEnum.TechHuman)
            {
                DevelopShipButton.SetActive(true);
            }
        }

        public void DisplayExplorationPanel()
        {
            SeparationPanel.SetActive(true);
            ExplorationPanel.SetActive(true);

            explorationPanelController.SetupPanel();
        }

        public void CloseExplorationPanel()
        {
            SeparationPanel.SetActive(false);
            ExplorationPanel.SetActive(false);

            explorationPanelController.ClearPanel();
        }

        public void DisplayBuildPanel()
        {
            SeparationPanel.SetActive(true);
            BuildingPanel.SetActive(true);

            Debug.Log("BUILT");
            GameController.Instance.PlayerManager.DisplayBuiltBuildings(SelectedRegionsController.SelectedRegionObjects.FirstOrDefault().name);

            Debug.Log("AVAILABLE");
            GameController.Instance.PlayerManager.DisplayAvailableBuildings(SelectedRegionsController.SelectedRegionObjects.FirstOrDefault().name);
        }

        public void DisplayDevelopShipPanel()
        {
            SeparationPanel.SetActive(true);
            DevelopShipPanel.SetActive(true);

            Debug.Log("BUILT");
            GameController.Instance.PlayerManager.DisplayBuiltBuildings("");

            Debug.Log("AVAILABLE");
            GameController.Instance.PlayerManager.DisplayAvailableBuildings("");
        }
    }
}
