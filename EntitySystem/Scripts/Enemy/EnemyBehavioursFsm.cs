namespace YWR.Tools
{
    public class EnemyBehavioursFsm : EntityBehavioursFsm
    {
        public EnemyBehavioursFsm(EntityData data) : base(data)
        {
        }

        protected override void OnBeforeInitialize()
        {
            RegisterState(new FollowState(this));
            RegisterState(new IdleState(this));
            RegisterState(new StunState(this));
            RegisterState(new AttackState(this));
            RegisterState(new DeadState(this));
            RegisterState(new SpawnState(this));
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            Idle();
        }

        public void Follow()
        {
            PushState<FollowState>();
        }

        public void Idle()
        {
            PushState<IdleState>();
        }

        public void Stun()
        {
            PushState<StunState>();
        }

        public void Attack()
        {
            PushState<AttackState>();
        }

        public void Die()
        {
            PushState<DeadState>();
        }

        public void Spawn()
        {
            PushState<SpawnState>();
        }
    }
}