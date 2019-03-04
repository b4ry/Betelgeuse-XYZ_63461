using Assets.Scripts.Controllers.Panels.RegionUIPanels.ExplorationPanel;
using UnityEngine;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels
{
    public class ButtonsPanelController : MonoBehaviour
    {
        public GameObject SeparationPanel;
        public GameObject ExplorationPanel;

        private ExplorationPanelController explorationPanelController;

        void Awake()
        {
            explorationPanelController = ExplorationPanel.GetComponent<ExplorationPanelController>();
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
    }
}
