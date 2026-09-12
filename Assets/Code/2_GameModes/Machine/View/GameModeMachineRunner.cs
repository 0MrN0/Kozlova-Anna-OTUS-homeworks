using UnityEngine;
using Zenject;

namespace Code.GameModes.Machine.View
{
    public class GameModeMachineRunner : MonoBehaviour
    {
        private GameModeMachine _machine;

        [Inject]
        public void Construct(GameModeMachine machine)
        {
            _machine = machine;
        }

        private void Start()
        {
            _machine.Enter<BootMode>();
        }
    }
}