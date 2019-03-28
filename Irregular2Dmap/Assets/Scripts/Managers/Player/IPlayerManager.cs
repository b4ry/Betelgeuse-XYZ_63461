using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Managers.Player
{
    public interface IPlayerManager
    {
        GameObject InitialRegion { get; set; }
        RaceEnum Race { get; set; }
        
        void DisplayAvailableBuildings(string regionName);
        void DisplayBuiltBuildings(string regionName);
    }
}
