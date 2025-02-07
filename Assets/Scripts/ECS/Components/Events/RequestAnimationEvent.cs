using Leopotam.EcsLite;

namespace Client {
    struct RequestAnimationEvent {
        public AnimationState State;
        public EcsPackedEntity entityPacked;
    }
    public enum AnimationState
    {
        Idle,
        Move,
        Attack
    }
}