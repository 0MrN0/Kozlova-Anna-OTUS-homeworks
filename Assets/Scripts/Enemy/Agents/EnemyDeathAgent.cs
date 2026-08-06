namespace ShootEmUp
{
    public sealed class EnemyDeathAgent
    {
        public HitPointsComponent HpComponent { get; private set; }

        public EnemyDeathAgent(HitPointsComponent hpComponent)
        {
            HpComponent = hpComponent;
        }
    }
}