using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    sealed class CheckChangePlayerSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<MouseClickEvent>> _filter;
        readonly EcsPoolInject<MouseClickEvent> _mouseClickPool;
        readonly EcsPoolInject<ChangePlayerEvent> _changePlayerPool;
        readonly EcsFilterInject<Inc<AllyComponent,PointInMapComponent>> _filterAlly;
        readonly EcsPoolInject<PointInMapComponent> _pointMapPool;
        readonly EcsWorldInject _world;
        public void Run(IEcsSystems systems)
        {
            foreach(var entity in _filter.Value)
            {
                ref var clickComponent = ref _mouseClickPool.Value.Get(entity);
                foreach(var entityAlly in _filterAlly.Value)
                {
                    ref var pointOnMap = ref _pointMapPool.Value.Get(entityAlly);
                    if(pointOnMap.pointMap.PointToMap == clickComponent.positionClick.PointToMap)
                    {
                        ref var changePlayer = ref _changePlayerPool.Value.Add(_world.Value.NewEntity());
                        changePlayer.newPlayer = _world.Value.PackEntity(entityAlly);
                        _mouseClickPool.Value.Del(entity);
                        break;
                    }    
                }
            }
        }
    }
}
