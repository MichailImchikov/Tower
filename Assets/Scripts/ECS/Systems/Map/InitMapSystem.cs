using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Client {
    sealed class InitMapSystem : IEcsInitSystem {
        readonly EcsWorldInject _world;
        readonly EcsPoolInject<MapAreaDrawerComponent> _mapDrawerPool;
        readonly EcsPoolInject<MapAreaWalkingComponent> _mapWalkingPool;
        public void Init (IEcsSystems systems) 
        {
            var TileMapDrawer = GameObject.FindObjectOfType<MapAreaDrawer>();
            var TileMapWalking = GameObject.FindObjectOfType<MapAreaWalking>();
            var entityMap = _world.Value.NewEntity();

            ref var drawerComp = ref _mapDrawerPool.Value.Add(entityMap);
            drawerComp.tilemap = TileMapDrawer.GetComponent<Tilemap>();
            drawerComp.TileWay = TileMapDrawer.TileWay;

            ref var wolkingComp = ref _mapWalkingPool.Value.Add(entityMap);
            wolkingComp.tilemap = TileMapWalking.GetComponent<Tilemap>();
            GameState.Instance.TilemapWalking = wolkingComp.tilemap;
        }
    }
}