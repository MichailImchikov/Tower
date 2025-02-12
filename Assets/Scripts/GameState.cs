using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameState
{
    public static GameState Instance;
    public EcsWorld world;
    public EcsPackedEntity CurrentPlayer;
    private List<Config> Configs;
    private Tilemap tilemapWalking;
    public Tilemap TilemapWalking { set => tilemapWalking = value; }
    public GameState()
    {
        Instance = this;
        Configs = new List<Config>();
    }
    public void AddConfig(Config config)
    {
        Configs.Add(config);
    }
    public ConfigType GetConfig<ConfigType>() where ConfigType : Config
    {
        foreach (var config in Configs)
        {
            if (config is ConfigType) return config as ConfigType;
        }

        return null;
    }
    public PointMap GetNewPoint(Vector3 position)
    {
        return new PointMap(position, tilemapWalking);
    }
    public PointMap GetNewPoint(Vector3Int position)
    {
        return new PointMap(position, tilemapWalking);
    }
}
