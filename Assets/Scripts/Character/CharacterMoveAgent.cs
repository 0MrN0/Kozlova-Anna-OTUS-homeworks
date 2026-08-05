using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class CharacterMoveAgent : IStartable, IFixedTickable, IDisposable
    {
        private readonly InputManager _inputManager;
        private readonly CharacterComponentsHolder _componentHolder;

        private float _horizontalDir;

        [Inject]
        public CharacterMoveAgent(InputManager inputManager, 
                                  CharacterComponentsHolder componentHolder)
        {
            _inputManager = inputManager;
            _componentHolder = componentHolder;
        }

        public void Start()
        {
            _inputManager.HorizontalDirectionChangedEvent += OnHorizontalDirectionChanged;
        }

        public void Dispose()
        {
            _inputManager.HorizontalDirectionChangedEvent -= OnHorizontalDirectionChanged;
        }

        public void FixedTick()
        {
            _componentHolder.MoveComponent.MoveByRigidbodyVelocity(new Vector2(_horizontalDir, 0) * Time.fixedDeltaTime);
        }

        private void OnHorizontalDirectionChanged(float newDir)
        {
            _horizontalDir = newDir;
        }
    }
}