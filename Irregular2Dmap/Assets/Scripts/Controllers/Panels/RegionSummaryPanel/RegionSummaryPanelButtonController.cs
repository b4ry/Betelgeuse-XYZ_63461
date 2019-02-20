using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers.Panels.RegionSummaryPanel
{
    public class RegionSummaryPanelButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private GameObject cameraObject;
        private Camera cameraComponent;
        private Vector3 localMousePosition;

        void Start()
        {
            cameraObject = GameController.Instance.MainCamera;
            cameraComponent = cameraObject.GetComponent<Camera>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            localMousePosition = transform.InverseTransformPoint(cameraComponent.ScreenToWorldPoint(Input.mousePosition));
            var rectTransform = transform.GetComponent<RectTransform>();

            localMousePosition.x += rectTransform.anchoredPosition.x;
            localMousePosition.y = -10;

            RegionSummaryPanelManager.Instance.PositionButtonTooltip(localMousePosition);
            RegionSummaryPanelManager.Instance.ShowButtonTooltip(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RegionSummaryPanelManager.Instance.ShowButtonTooltip(false);
            RegionSummaryPanelManager.Instance.PositionButtonTooltip(localMousePosition * -1);
        }
    }
}
