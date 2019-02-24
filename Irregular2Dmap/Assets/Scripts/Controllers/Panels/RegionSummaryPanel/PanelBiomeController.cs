using Assets.Scripts.Managers.WorldMap;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers.Panels.RegionSummaryPanel
{
    public class PanelBiomeController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
            localMousePosition.y = localMousePosition.y - 20;

            RegionSummaryPanelManager.Instance.PositionBiomeTooltip(localMousePosition);
            RegionSummaryPanelManager.Instance.SetBiomeTooltipText(gameObject.name);
            RegionSummaryPanelManager.Instance.ShowBiomeTooltip(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RegionSummaryPanelManager.Instance.ShowBiomeTooltip(false);
            RegionSummaryPanelManager.Instance.PositionBiomeTooltip(localMousePosition * -1);
        }
    }
}
