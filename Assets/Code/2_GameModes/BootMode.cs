using System;
using UnityEngine;

namespace Code.GameModes
{
    public class BootMode : IGameMode
    {
        public event Action WarmUpDoneEvent;

        public void Enter()
        {
            Debug.Log("Enter boot. Warming up 1000 services");
            // _saveLoadAggregate.Load();
            // AnalatycService.Init()
            // AudioService.Init()

            WarmUpDoneEvent?.Invoke();
        }

        public void Exit()
        {

        }

        public void Tick()
        {

        }
    }
}
