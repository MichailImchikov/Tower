using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class InputSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsFilterInject<Inc<MousePositionComponent>> _filterInput;
        readonly EcsPoolInject<MouseClickUpEvent> _mouseClickUpPool;
        readonly EcsPoolInject<MouseClickDownEvent> _mouseClickDownPool;
        readonly EcsPoolInject<MousePositionComponent> _mousePositionComponent;
        readonly EcsPoolInject<ScrollMouseWheelEvent> _scrollMouseWheelPool;
        readonly EcsWorldInject _world;

        readonly EcsPoolInject<ChangeMoveStateEvent> _moveStatePool;
        readonly EcsPoolInject<ChangeAttackStateEvent> _attakStatePool;
        readonly EcsFilterInject<Inc<PlayerComponent,AbilityContainer>> _filterPlayer;
        readonly EcsPoolInject<AbilityContainer> _abilityContainerPool;
        readonly EcsPoolInject<ChoosingAbilityUseEvent> _chosingAbilityPool;
        readonly EcsPoolInject<AttackStateComponent> _attackStatePool;
        public void Init(IEcsSystems systems)
        {
            var entityInput = _world.Value.NewEntity();
            ref var mousePositionComp = ref _mousePositionComponent.Value.Add(entityInput);
            mousePositionComp.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public void Run(IEcsSystems systems)
        {
            foreach(var entity in _filterInput.Value)
            {
                ref var mousePositionComp = ref _mousePositionComponent.Value.Get(entity);
                mousePositionComp.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Input.GetMouseButtonUp(0))
                    _mouseClickUpPool.Value.Add(entity);
                if (Input.GetMouseButtonDown(0))
                    _mouseClickDownPool.Value.Add(entity);
                if (Input.mouseScrollDelta.y != 0)
                    _scrollMouseWheelPool.Value.Add(entity).ScrollSize = Input.mouseScrollDelta.y;
                // todo Transfer to the UI
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    foreach(var entityPlayer in _filterPlayer.Value)
                    {
                        _attakStatePool.Value.Add(entityPlayer);
                    }
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    foreach (var entityPlayer in _filterPlayer.Value)
                    {
                        _moveStatePool.Value.Add(entityPlayer);
                    }
                }
                for (int i=0 ; i<=9 ; i++)
                {
                    if (!Input.GetKeyDown((KeyCode)(KeyCode.Alpha0 + i))) continue;
                    foreach (var entityPlayer in _filterPlayer.Value)
                    {
                        ref var abilityContainer = ref _abilityContainerPool.Value.Get(entityPlayer);
                        if (abilityContainer.Abilities.Count < i) continue;
                        if (!abilityContainer.Abilities[i - 1].Unpack(_world.Value, out int abilityEntity)) continue;
                        if (!_attackStatePool.Value.Has(entityPlayer)) continue;
                        ref var choosingAbilityComp = ref _chosingAbilityPool.Value.Add(entityPlayer);
                        choosingAbilityComp.abilityEntity = _world.Value.PackEntity(abilityEntity);
                        choosingAbilityComp.ownerAbility = _world.Value.PackEntity(entityPlayer);
                    }
                }
            }
        }
    }
}