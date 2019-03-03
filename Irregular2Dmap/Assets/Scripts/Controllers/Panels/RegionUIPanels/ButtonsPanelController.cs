using Assets.Scripts.Controllers.Panels.RegionUIPanels.ExplorationPanel;
using UnityEngine;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels
{
    public class ButtonsPanelController : MonoBehaviour
    {
        public GameObject SeparationPanel;
        public GameObject ExplorationPanel;

        public void DisplayExplorationPanel()
        {
            SeparationPanel.SetActive(true);
            ExplorationPanel.SetActive(true);

            if(ExplorationPanel.activeSelf)
            {
                ExplorationPanel.GetComponent<ExplorationPanelController>().SetupPanel();
            }
        }

        public void CloseExplorationPanel()
        {
            SeparationPanel.SetActive(false);
            ExplorationPanel.SetActive(false);
        }
    }
}
