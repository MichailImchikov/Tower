using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace Client {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;        
        IEcsSystems _systems;
        GameState _state;
        private void Awake()
        {
            _state = new GameState();
            LoadConfigs();
        }
        void Start () {
            _world = new EcsWorld ();
            _state.world = _world;
            _systems = new EcsSystems (_world, _state);
            _systems
                .Add(new InitMapSystem())
                .Add(new InitPlayerSystems())
                .Add(new InputSystem())

                .Add(new CheckInputChangePlayerSystem())
                .Add(new CheckInputForMovementSystem())

                .Add(new ChangeAnimationSystem())
                .DelHere<RequestAnimationEvent>()

                .DelHere<MouseClickEvent>()
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


#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject()
                .Init ();
        }

        void Update () {
            // process systems here.
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
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
    }
}