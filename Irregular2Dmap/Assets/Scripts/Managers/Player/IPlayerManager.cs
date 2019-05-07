using Assets.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers.Player
{
    public interface IPlayerManager
    {
        GameObject InitialRegion { get; set; }
        List<string> VisitedRegions { get; set; }
        RaceEnum Race { get; set; }
        
        void DisplayAvailableBuildings(string regionName);
        void DisplayBuiltBuildings(string regionName);
    }
}
