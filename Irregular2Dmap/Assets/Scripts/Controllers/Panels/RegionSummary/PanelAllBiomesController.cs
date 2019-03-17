using Assets.Scripts.Managers.WorldMap;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers.Panels.RegionSummary
{
    public class PanelAllBiomesController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

            RegionSummaryPanelManager.Instance.PositionAllBiomesTooltip(localMousePosition);
            RegionSummaryPanelManager.Instance.SetAllBiomesTooltipText(gameObject.name);
            RegionSummaryPanelManager.Instance.ShowAllBiomesTooltip(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RegionSummaryPanelManager.Instance.ShowAllBiomesTooltip(false);
            RegionSummaryPanelManager.Instance.PositionAllBiomesTooltip(localMousePosition * -1);
        }
    }
}
