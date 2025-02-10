using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    sealed class CheckInputChangePlayerSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<MouseClickUpEvent,MousePositionComponent>,Exc<MouseClampComponent>> _filter;
        readonly EcsPoolInject<MousePositionComponent> _mousePositionPool;
        readonly EcsPoolInject<MouseClickUpEvent> _mouseClickUpPool;
        readonly EcsPoolInject<ChangePlayerEvent> _changePlayerPool;
        readonly EcsFilterInject<Inc<AllyComponent,PointInMapComponent>> _filterAlly;
        readonly EcsPoolInject<PointInMapComponent> _pointMapPool;
        readonly EcsWorldInject _world;
        public void Run(IEcsSystems systems)
        {

            foreach(var entity in _filter.Value)
            {
                ref var mousePositionComponent = ref _mousePositionPool.Value.Get(entity);
                foreach(var entityAlly in _filterAlly.Value)
                {
                    ref var pointOnMap = ref _pointMapPool.Value.Get(entityAlly);
                    if(pointOnMap.pointMap.PointToMap == mousePositionComponent.pointMap.PointToMap)
                    {
                        ref var changePlayer = ref _changePlayerPool.Value.Add(_world.Value.NewEntity());
                        changePlayer.newPlayer = _world.Value.PackEntity(entityAlly);
                        _mouseClickUpPool.Value.Del(entity);
                        break;
                    }    
                }
            }
        }
    }
}
