using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers
{
    public class RegionSummaryPanelButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject cameraObject;

        private Vector3 localMousePosition;
        private Camera cameraComponent;

        void Awake()
        {
            cameraComponent = cameraObject.GetComponent<Camera>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            localMousePosition = transform.InverseTransformPoint(cameraComponent.ScreenToWorldPoint(Input.mousePosition));
            var rectTransform = transform.GetComponent<RectTransform>();

            localMousePosition.x += rectTransform.anchoredPosition.x;
            localMousePosition.y = -20;

            Debug.Log(localMousePosition);

            RegionSummaryPanelManager.Instance.PositionTooltip(localMousePosition);
            RegionSummaryPanelManager.Instance.ShowToolTip(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RegionSummaryPanelManager.Instance.ShowToolTip(false);
            RegionSummaryPanelManager.Instance.PositionTooltip(localMousePosition*-1);
        }
    }
}
