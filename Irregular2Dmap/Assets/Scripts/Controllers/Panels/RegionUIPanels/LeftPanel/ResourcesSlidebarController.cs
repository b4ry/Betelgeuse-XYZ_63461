using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers.Panels.RegionUIPanels.LeftPanel
{
    public class ResourcesSlidebarController : MonoBehaviour
    {
        public GameObject ResourceCollection;

        private void Start()
        {
            var maxValue = ResourceCollection.GetComponent<RectTransform>().sizeDelta.y - 100;

            if(maxValue == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.GetComponent<Slider>().maxValue = maxValue;
            }
        }

        public void HandleResourcesScrollbar(float newValue)
        {
            var position = ResourceCollection.transform.localPosition;

            position.y = newValue;
            ResourceCollection.transform.localPosition = position;
        }
    }
}
