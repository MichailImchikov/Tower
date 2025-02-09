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
            _systems
                .Add(new InitMapSystem())
                .Add(new InitPlayerSystems())
                .Add(new ChangeAnimationSystem())
                .DelHere<RequestAnimationEvent>()
                .Add(new CreateWayToPointSystem())
                .DelHere<CreateWayToPointEvent>()
                .Add(new MoveToPointSystem())
                .Add(new ChangePlayerSystems())
                .Add(new ClearMapDrawerSystem())
                .Add(new CreateAreaWalkingSystem())
                .Add(new DrawAreaWalkingSystem())
                .DelHere<ChangePlayerEvent>()
                .DelHere<CreateAreaWalkingEvent>()
                .DelHere<DrawAreaWalkingEvent>()
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
                .Add(new CheckInputChangePlayerSystem())
                .Add(new CheckInputForMovementSystem())
                ;

            InjectAllSystems(_systems, _inputSystems);
            InitAllSystems(_systems, _inputSystems);

        }

        void Update () {
            // process systems here.
            _systems?.Run ();
            _inputSystems?.Run();
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