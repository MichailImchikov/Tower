using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Client {
    sealed class MoveToPointSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<MoveToPointComponent, TransformComponent>> _filter;
        readonly EcsPoolInject<TransformComponent> _transformPool;
        readonly EcsPoolInject<MoveToPointComponent> _moveToPointPool;
        readonly EcsPoolInject<CreateAreaWalkingEvent> _areaWalkingPool;
        readonly EcsPoolInject<DrawAreaWalkingEvent> _drawAreaWalkingPool;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var transformComp = ref _transformPool.Value.Get(entity);
                ref var moveToPointComp = ref _moveToPointPool.Value.Get(entity);
                if (Vector3.Distance(transformComp.Transform.position, moveToPointComp.CurrentPoint().PointToWorld) > 0.001f)
                {
                    transformComp.Transform.position = Vector3.MoveTowards(transformComp.Transform.position, moveToPointComp.CurrentPoint().PointToWorld, 3f * Time.deltaTime);
                    var diraction = moveToPointComp.CurrentPoint().PointToWorld - transformComp.Transform.position;
                    if (diraction.x > 0) transformComp.Transform.GetChild(0).localScale = new Vector3(-1, 1, 1);// to do normal, igor bi volosi na gope rval za takoe
                    else transformComp.Transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                }
                else if (!moveToPointComp.NextPoint())
                {
                    _areaWalkingPool.Value.Add(entity);
                    _drawAreaWalkingPool.Value.Add(entity);
                    _moveToPointPool.Value.Del(entity);
                }
            }
        }
    }
}