using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Managers
{
    public interface IPanelManager
    {
        void SetActive(bool active);
        void SetupRegionSummaryPanel(RegionModel regionModel, bool exploreButtonInteractable);
        void AddButtonListener(UnityAction action);
        void ShowButtonTooltip(bool show);
        void PositionButtonTooltip(Vector3 cursorPosition);
        void ShowBiomeTooltip(bool show);
        void PositionBiomeTooltip(Vector3 cursorPosition);
        void SetBiomeTooltipText(string text);
    }
}
