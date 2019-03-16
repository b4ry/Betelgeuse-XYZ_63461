using Assets.Scripts.Enums;
using Assets.Scripts.Managers.Player;
using System;
using UnityEngine;

namespace Assets.Scripts.Factories
{
    public class PlayerFactory : IFactory<IPlayerManager>
    {
        public IPlayerManager Produce(Enum product)
        {
            RaceEnum race = (RaceEnum)product;

            if(race == RaceEnum.Human)
            {
                Debug.Log("Produced Humans");

                return new HumanPlayerManager();
            }
            else if(race == RaceEnum.TechHuman)
            {
                Debug.Log("Produced Techs");

                return new TechPlayerManager();
            }

            return null;
        }
    }
}
