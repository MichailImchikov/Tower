using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEditor.U2D.Aseprite;
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
        readonly EcsFilterInject<Inc<PlayerComponent,AbilityContainer,AttackStateComponent>> _filterPlayer;
        readonly EcsPoolInject<AbilityContainer> _abilityContainerPool;
        readonly EcsPoolInject<ChoosingAbilityUseEvent> _chosingAbilityPool;
        readonly EcsPoolInject<AbilityToUseComponent> _abilityToUsePool;
        readonly EcsPoolInject<RequestTurnAttackEvent> _requestTurnAttackPool;
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
                    if (GameState.Instance.CurrentPlayer.Unpack(_world.Value, out int entityPlayer))
                        if(_abilityContainerPool.Value.Has(entityPlayer))
                            _attakStatePool.Value.Add(entityPlayer);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (GameState.Instance.CurrentPlayer.Unpack(_world.Value, out int entityPlayer))
                        _moveStatePool.Value.Add(entityPlayer);
                }
                for (int i=0 ; i<=9 ; i++)
                {
                    if (!Input.GetKeyDown((KeyCode)(KeyCode.Alpha0 + i))) continue;
                    foreach (var entityPlayer in _filterPlayer.Value)
                    {
                        ref var abilityContainer = ref _abilityContainerPool.Value.Get(entityPlayer);
                        if (abilityContainer.Abilities.Count < i) continue;
                        if (!abilityContainer.Abilities[i - 1].Unpack(_world.Value, out int abilityEntity)) continue;
                        ref var choosingAbilityComp = ref _chosingAbilityPool.Value.Add(entityPlayer);
                        choosingAbilityComp.abilityEntity = _world.Value.PackEntity(abilityEntity);
                        choosingAbilityComp.ownerAbility = _world.Value.PackEntity(entityPlayer);
                    }
                }
                if(Input.GetKeyDown(KeyCode.V))
                {
                    if (!GameState.Instance.CurrentPlayer.Unpack(_world.Value, out int playerEntity)) continue;
                    if (!_abilityToUsePool.Value.Has(playerEntity)) continue;
                    ref var abilityToUseComp = ref _abilityToUsePool.Value.Get(playerEntity);
                    _requestTurnAttackPool.Value.Add(abilityToUseComp.entityAbility.Id);
                }
            }
        }
    }
}