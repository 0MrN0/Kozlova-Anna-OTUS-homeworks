using System;
using System.Linq;
using System.Collections.Generic;
using Code.Infrastructure.DI;
using UnityEngine;
using Zenject;

namespace Code.GameModes.Machine
{
    public class GameModeMachine : ITickable
    {
        private readonly IProjectDiService _projectDi;
        private readonly Dictionary<Type, IGameMode> _modes;

        private IGameMode _current;

        public GameModeMachine(IProjectDiService projectDi)
        {
            _projectDi = projectDi;
            _modes = _projectDi.ResolveAll<IGameMode>().ToDictionary(m => m.GetType(), m => m);
            ((BootMode)_modes[typeof(BootMode)]).WarmUpDoneEvent += OnWarmUpDone;
        }

        private void OnWarmUpDone()
        {
            Enter<BattleMode>();
        }

        public void Enter<TMode>() where TMode : IGameMode
        {
            Switch(_modes[typeof(TMode)]);
        }

        private void Switch(IGameMode nextMode)
        {
            Debug.Log($"[GameModeMachine] {_current?.GetType().Name ?? "none"} -> {nextMode.GetType().Name}");

            _current?.Exit();
            _current = nextMode;
            _current.Enter();
        }

        public void Tick()
        {
            _current.Tick();
        }
    }
}
