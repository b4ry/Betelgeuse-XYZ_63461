using Assets.Scripts.Managers.WorldMap;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers.Panels.RegionSummary
{
    public class PanelAllResourcesController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
            localMousePosition.y = localMousePosition.y - 30;

            RegionSummaryPanelManager.Instance.PositionAllResourcesTooltip(localMousePosition);
            RegionSummaryPanelManager.Instance.SetAllResourcesTooltipText(gameObject.name);
            RegionSummaryPanelManager.Instance.ShowAllResourcesTooltip(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RegionSummaryPanelManager.Instance.ShowAllResourcesTooltip(false);
            RegionSummaryPanelManager.Instance.PositionAllResourcesTooltip(localMousePosition * -1);
        }
    }
}
