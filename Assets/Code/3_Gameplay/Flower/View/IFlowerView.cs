using System;

namespace Code.Gameplay.Flower.View
{
    public interface IFlowerView: IDisposable
    {
        public event Action Pickupped;
    }
}