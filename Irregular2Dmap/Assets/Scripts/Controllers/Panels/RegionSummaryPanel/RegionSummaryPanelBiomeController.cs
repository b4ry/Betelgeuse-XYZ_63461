using Assets.Scripts.Managers;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Controllers.Panels.RegionSummaryPanel
{
    public class RegionSummaryPanelBiomeController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
            localMousePosition.y = -20 - eventData.pointerEnter.transform.position.y;

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
