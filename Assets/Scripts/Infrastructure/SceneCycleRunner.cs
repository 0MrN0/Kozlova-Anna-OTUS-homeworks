using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public class SceneCycleRunner : MonoBehaviour
    {
        private ISceneCycleService _cycleService = new SceneCycleService();
        [SerializeField] private List<MonoBehaviour> entities = new();

#if UNITY_EDITOR
        public void SetMonoBehavioursInEditor()
        {
            foreach (var mb in FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                if (!entities.Contains(mb)) entities.Add(mb);
            }
        }
#endif

        public void RegisterRuntimeEntity(MonoBehaviour e)
        {
            entities.Add(e);
            foreach (var sc in e.GetComponents<ISceneCycle>())
                _cycleService.Register(sc);
        }

        private void Awake()
        {
            foreach (var e in entities)
            {
                if (e is ISceneCycle ce)
                {
                    _cycleService.Register(ce);
                }
            }

            _cycleService.AllAwake();
        }

        private void Start()
        {
            _cycleService.AllPreStart();

            _cycleService.AllStart();
        }

        private void FixedUpdate()
        {
            _cycleService.AllFixedUpdate();
        }

        private void Update()
        {
            _cycleService.AllUpdate();
        }

        private void LateUpdate()
        {
            _cycleService.AllLateUpdate();
        }

        private void OnDestroy()
        {
            _cycleService.AllOnDestroy();
        }
    }
}