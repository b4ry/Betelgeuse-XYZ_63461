using Assets.Scripts.Enums;

namespace Assets.Scripts.Managers.Player
{
    public interface IPlayerManager
    {
        RaceEnum Race { get; set; }

        void Build(string regionName);
        void DisplayBuildings(string regionName);
    }
}
