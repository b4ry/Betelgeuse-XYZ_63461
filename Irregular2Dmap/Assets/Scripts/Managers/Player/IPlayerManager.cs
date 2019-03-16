using Assets.Scripts.Enums;

namespace Assets.Scripts.Managers.Player
{
    public interface IPlayerManager
    {
        RaceEnum Race { get; set; }

        void DisplayAvailableBuildings(string regionName);
        void DisplayBuiltBuildings(string regionName);
    }
}
