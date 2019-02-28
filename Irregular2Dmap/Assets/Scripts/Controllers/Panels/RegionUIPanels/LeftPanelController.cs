using UnityEngine;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels
{
    public class LeftPanelController : MonoBehaviour
    {
        [SerializeField]
        private GameObject resourcesDetails;

        public void SlideResourcesPanel(float newValue)
        {
            var position = resourcesDetails.transform.localPosition;

            position.x = newValue * (-1);
            resourcesDetails.transform.localPosition = position;
        }
    }
}
