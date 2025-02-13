using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Client {
    sealed class MoveToPointSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<MoveToPointComponent, TransformComponent,PointInMapComponent>> _filter;
        readonly EcsPoolInject<TransformComponent> _transformPool;
        readonly EcsPoolInject<MoveToPointComponent> _moveToPointPool;
        readonly EcsPoolInject<CreateAreaWalkingEvent> _areaWalkingPool;
        readonly EcsPoolInject<DrawAreaWalkingEvent> _drawAreaWalkingPool;
        readonly EcsPoolInject<PointInMapComponent> _pointMapComponent;
        readonly EcsPoolInject<RequestAnimationEvent> _requestAnimationPool;
        readonly EcsPoolInject<PlayerComponent> _playerPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var transformComp = ref _transformPool.Value.Get(entity);
                ref var moveToPointComp = ref _moveToPointPool.Value.Get(entity);
                if (transformComp.Transform.position != moveToPointComp.CurrentPoint().PointToWorld)
                {

                    transformComp.Transform.position = Vector3.MoveTowards(transformComp.Transform.position, moveToPointComp.CurrentPoint().PointToWorld, 3f * Time.deltaTime);
                    
                }
                else if (!moveToPointComp.NextPoint())
                {
                    ref var pointMapcomp = ref _pointMapComponent.Value.Get(entity);
                    pointMapcomp.pointMap = moveToPointComp.WayToPoint.Last();
                    ref var requestAnimation = ref  _requestAnimationPool.Value.Add(_world.Value.NewEntity());
                    requestAnimation.State = AnimationState.Idle;
                    requestAnimation.entityPacked = _world.Value.PackEntity(entity);
                    _moveToPointPool.Value.Del(entity);
                    if (!_playerPool.Value.Has(entity))continue;
                    _areaWalkingPool.Value.Add(entity);
                    _drawAreaWalkingPool.Value.Add(entity);
                }
                else
                {
                    var diraction = moveToPointComp.CurrentPoint().PointToWorld - transformComp.Transform.position;
                    if (diraction.y != 0) continue;
                    if (diraction.x > 0) transformComp.Transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
                    else transformComp.Transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }
}