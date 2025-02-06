using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class MovementCircleSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<PlayerComponent, AreaWalkingComponent>> _filterPlayer;
        readonly EcsPoolInject<AreaWalkingComponent> _areaWalkingPool;
        readonly EcsPoolInject<TransformCircleComponent> _transformCircle;
        private Vector3 mousePosition;
        //readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            SetPosition();
            foreach (var entity in _filterPlayer.Value)
            {
                ref var transformComp=ref _transformCircle.Value.Get(entity);
                ref var areaWalkingComp = ref _areaWalkingPool.Value.Get(entity);
                transformComp.transform.position = mousePosition;
            }
            
        }
        private void SetPosition()
        {
            mousePosition = Input.mousePosition;
        }
    }
}