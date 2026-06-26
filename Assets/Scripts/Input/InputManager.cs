using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public Action<float> HorizontalDirectionChangedEvent;
        public Action FireRequiredEvent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FireRequiredEvent?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirectionChangedEvent?.Invoke(-1);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirectionChangedEvent?.Invoke(1);
            }
            else
            {
                HorizontalDirectionChangedEvent?.Invoke(0);
            }
        }
    }
}