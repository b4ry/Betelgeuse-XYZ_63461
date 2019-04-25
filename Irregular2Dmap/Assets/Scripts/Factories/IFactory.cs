using Assets.Scripts.Models;
using System;

namespace Assets.Scripts.Factories
{
    public interface IFactory<T>
    {
        T Produce(PlayerModel product);
    }
}
