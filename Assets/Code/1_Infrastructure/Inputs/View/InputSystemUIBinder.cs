using UnityEngine.InputSystem.UI;
using Zenject;

namespace Code.Infrastructure.Inputs.View
{
    public sealed class InputSystemUIBinder
    {
        [Inject]
        public void Construct(IInputService input, InputSystemUIInputModule module)
        {
            module.actionsAsset = input.Actions;
        }
    }
}