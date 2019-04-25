using Assets.Scripts.Enums;
using Assets.Scripts.Managers.Player;
using Assets.Scripts.Models;
using System;
using UnityEngine;

namespace Assets.Scripts.Factories
{
    public class PlayerFactory : IFactory<IPlayerManager>
    {
        public IPlayerManager Produce(PlayerModel product)
        {
            if(product.Race == RaceEnum.Human)
            {
                Debug.Log("Produced Humans");

                return new HumanPlayerManager();
            }
            else if(product.Race == RaceEnum.TechHuman)
            {
                Debug.Log("Produced Techs");

                return new TechPlayerManager();
            }

            return null;
        }
    }
}
