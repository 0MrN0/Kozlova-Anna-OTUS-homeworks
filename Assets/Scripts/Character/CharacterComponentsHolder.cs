using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(WeaponComponent))]
    [RequireComponent(typeof(TeamComponent))]
    [RequireComponent(typeof(HitPointsComponent))]
    [RequireComponent(typeof(MoveComponent))]
    public sealed class CharacterComponentsHolder : MonoBehaviour, ISceneCycleAwake
    {
        public MoveComponent MoveComponent { get; private set; }
        public HitPointsComponent HpComponent { get; private set; }
        public TeamComponent TeamComponent { get; private set; }
        public WeaponComponent WeaponComponent { get; private set; }

        public void OnAwake()
        {
            MoveComponent = GetComponent<MoveComponent>();
            HpComponent = GetComponent<HitPointsComponent>();
            HpComponent.Init();
            TeamComponent = GetComponent<TeamComponent>();
            WeaponComponent = GetComponent<WeaponComponent>();
        }
    }
}