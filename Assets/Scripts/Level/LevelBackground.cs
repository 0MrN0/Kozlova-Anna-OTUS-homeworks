using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, ISceneCycleAwake, ISceneCycleFixedUpdate
    {
        [SerializeField] private Params backgroundParams;

        private float _startPositionY;
        private float _endPositionY;
        private float _movingSpeedY;
        private float _positionX;
        private float _positionZ;
        private Transform _transform;

        public void OnAwake()
        {
            _startPositionY = backgroundParams.StartPositionY;
            _endPositionY = backgroundParams.EndPositionY;
            _movingSpeedY = backgroundParams.MovingSpeedY;
            _transform = transform;
            var position = _transform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        public void OnFixedUpdate()
        {
            if (_transform.position.y <= _endPositionY)
            {
                _transform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _transform.position -= new Vector3(
                _positionX,
                _movingSpeedY * Time.fixedDeltaTime,
                _positionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField] public float StartPositionY;
            [SerializeField] public float EndPositionY;
            [SerializeField] public float MovingSpeedY;
        }
    }
}