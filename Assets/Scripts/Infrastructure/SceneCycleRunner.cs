using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp
{
    public class SceneCycleRunner : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> entities = new();

        private ISceneCycleService _cycleService = new SceneCycleService();

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

        private void Init()
        {
            foreach (var e in entities)
            {
                if (e is ISceneCycle ce)
                {
                    _cycleService.Register(ce);
                }
            }
        }

        private void Awake()
        {
            Init();

            _cycleService.AllAwake();
        }

        private void OnEnable()
        {
            _cycleService.AllResume();
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

        private void OnDisable()
        {
            _cycleService.AllPause();
        }

        private void OnDestroy()
        {
            _cycleService.AllOnDestroy();
        }
    }
}