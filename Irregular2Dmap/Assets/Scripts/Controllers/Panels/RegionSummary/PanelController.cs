using UnityEngine;

namespace Assets.Scripts.Controllers.Panels.RegionSummary
{
    public class PanelController : MonoBehaviour
    {
        [SerializeField]
        private GameObject moreBiomesPanel;

        [SerializeField]
        private GameObject moreResourcesPanel;

        private float currentPositionX = 0;
        private float currentPositionY = 0;

        private float offsetX;
        private float offsetY;

        private Vector3 localMousePosition;
        private Camera cameraComponent;

        void Start()
        {
            cameraComponent = GameController.Instance.MainCamera.GetComponent<Camera>();
        }

        public void BeginDrag()
        {
            offsetX = transform.position.x - Input.mousePosition.x;
            offsetY = transform.position.y - Input.mousePosition.y;
        }

        public void OnDrag()
        {
            var newPosition = new Vector3(currentPositionX + offsetX + Input.mousePosition.x, currentPositionY + offsetY + Input.mousePosition.y, 0)
            {
                z = 100
            };

            newPosition.y += Screen.height;

            transform.position = cameraComponent.ScreenToWorldPoint(newPosition);
        }

        public void EndDrag()
        {
            var newPosition = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y, 0)
            {
                z = 100
            };

            currentPositionX += newPosition.x;
            currentPositionY += newPosition.y;
        }

        public void MoreBiomesButtonClick()
        {
            var panelActivity = moreBiomesPanel.activeSelf;

            moreBiomesPanel.SetActive(!panelActivity);
        }

        public void MoreResourcesButtonClick()
        {
            var panelActivity = moreResourcesPanel.activeSelf;

            moreResourcesPanel.SetActive(!panelActivity);
        }
    }
}
