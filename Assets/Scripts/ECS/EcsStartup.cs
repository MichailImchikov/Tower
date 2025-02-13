using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;
        GameState _state;
        EcsSystems _inputSystems;
        EcsSystems _abilitySystem;
        EcsSystems _fightSystems;
        EcsSystems _unitStateSystems;
        private void Awake()
        {
            _state = new GameState();
            LoadConfigs();
        }
        void Start () {
            _world = new EcsWorld ();
            _state.world = _world;
            _systems = new EcsSystems (_world, _state);
            _inputSystems = new EcsSystems (_world, _state);
            _fightSystems = new EcsSystems ( _world, _state);
            _abilitySystem = new EcsSystems ( _world, _state);
            _unitStateSystems = new EcsSystems ( _world, _state);
            _systems
                .Add(new InitMapSystem())
                .Add(new InitPlayerSystems())
                .Add(new ChangeAnimationSystem())
                .Add(new ChangeWeaponSystem())
                .DelHere<RequestAnimationEvent>()
                .Add(new CreateWayToPointSystem())
                .Add(new RemoveMovementPointsSystem())
                .DelHere<RemoveMovementPointsEvent>()
                .DelHere<CreateWayToPointEvent>()
                .Add(new MoveToPointSystem())
                .Add(new ChangePlayerSystems())
                .Add(new ClearMapDrawerSystem())
                .Add(new CreateAreaWalkingSystem())
                .Add(new DrawAreaWalkingSystem())
                .Add(new DrawAttackZoneSystem())
                .DelHere<ChangePlayerEvent>()
                .DelHere<CreateAreaWalkingEvent>()
                .DelHere<DrawAreaWalkingEvent>()
                .DelHere<DrawAttackZoneEvent>()
                .DelHere<ChangeWeaponEvent>()
                .Add(new MovementCircleSystem())
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                ;
            _inputSystems
                .DelHere<MouseClickUpEvent>()
                .DelHere<MouseClickDownEvent>()
                .DelHere<ScrollMouseWheelEvent>()
                .Add(new DragAndDropSystem())

                .Add(new MoveCameraSystem())
                .DelHere<DragAndDropEvent>()

                .Add(new InputSystem())
                .Add(new CheckClampButtonSystem())
                .Add(new TimerMouseClampSystem())
                .Add(new ScrollCameraSystem())

                .Add(new CheckInvokeAbilitySystem())
                .Add(new CheckInitAttackZoneSystem())
                .Add(new CheckInputChangePlayerSystem())
                .Add(new CheckInputForMovementSystem())

                ;
            _abilitySystem
                .Add(new ChoosingAbilityUseSystem())
                .Add(new InitAbilitySystem())
                .Add(new InitAttackZoneSystem())
                
                .Add(new InvokeAbilitySystem())
                .DelHere<InitAttackZoneEvent>()
                .DelHere<InitAbilityEvent>()
                .DelHere<InvokeAbilityEvent>()
                

                .DelHere<ChoosingAbilityUseEvent>()

                ;
            _fightSystems
                .Add(new RequestTakeDamageSystem())
                .Add(new TakeDamageSystem())

                .DelHere<TakeDamageEvent>()
                .DelHere<RequestDamageEvent>()
                ;
            _unitStateSystems
                .Add(new DeathSystem())
                .Add(new ChangeAttackStateSystem())
                .Add(new ChangeMoveStateSystem())
                .DelHere<DeathEvent>()
                .DelHere<ChangeMoveStateEvent>()
                .DelHere<ChangeAttackStateEvent>()
                ;

            InjectAllSystems(_systems, _inputSystems,_fightSystems, _abilitySystem, _unitStateSystems);
            InitAllSystems(_systems, _inputSystems,_fightSystems, _abilitySystem, _unitStateSystems);

        }

        void Update () {
            // process systems here.
            _systems?.Run ();
            _inputSystems?.Run();
            _fightSystems?.Run();
            _abilitySystem?.Run();
            _unitStateSystems?.Run();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
            }
            if (_inputSystems != null)
            {
                _inputSystems.Destroy();
                _inputSystems = null;
            }           
            if (_fightSystems != null)
            {
                _fightSystems.Destroy();
                _fightSystems = null;
            }           
            if (_abilitySystem != null)
            {
                _abilitySystem.Destroy();
                _abilitySystem = null;
            }           
            if (_unitStateSystems != null)
            {
                _unitStateSystems.Destroy();
                _unitStateSystems = null;
            }
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
        void LoadConfigs()
        {
            Object[] confs = Resources.LoadAll("Configs", typeof(Config));

            foreach (var config in confs)
            {
                Config newConfig = config as Config;

                _state.AddConfig(newConfig);
            }
        }
        private void InjectAllSystems(params EcsSystems[] systems)
        {
            foreach (var system in systems)
            {
                system.Inject();
            }
        }

        private void InitAllSystems(params EcsSystems[] systems)
        {
            foreach (var system in systems)
            {
                system.Init();
            }
        }
    }
}