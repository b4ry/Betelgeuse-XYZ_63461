using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Managers
{
    public interface IPanelManager
    {
        void SetActive(bool active);
        void SetupRegionSummaryPanel(string regionName, string size, string biomes, bool exploreButtonInteractable);
        void AddButtonListener(UnityAction action);
        void ShowToolTip(bool show);
        void PositionTooltip(Vector3 cursorPosition);
    }
}
