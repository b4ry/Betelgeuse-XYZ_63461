using System;

namespace Assets.Scripts.Factories
{
    public interface IFactory<T>
    {
        T Produce(Enum product);
    }
}
