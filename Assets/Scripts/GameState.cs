using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState 
{
    public static GameState Instance;
    private List<Config> Configs;
    public EcsWorld world;
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
}
