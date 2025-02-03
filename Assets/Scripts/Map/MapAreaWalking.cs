
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapAreaWalking : MonoBehaviour
{
    private Tilemap _tilemap;
    public static MapAreaWalking Instance;
    void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        Instance = this;
        
    }
    private void Start()
    {
        
    }
    public PointMap newPoint(Vector3 pointToWorld) => new(pointToWorld, _tilemap);
    public PointMap newPoint(Vector3Int pointToMap) => new(pointToMap, _tilemap);
}
