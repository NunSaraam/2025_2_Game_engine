using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TileInfos
{
    public int tileX;
    public int tileY;
    public int tileZ;
    public string tileType;
}

public class PerlinNoiseVoxelMap : MonoBehaviour
{
    public GameObject grassPrefab;
    public GameObject dirtPrefab;
    public GameObject waterPrefab;

    public int width = 20;
    
    public int depth = 20;

    public int maxHeight = 16;

    private bool[ , , ] hasBlock;

    [SerializeField] float noiseScale = 20f;

    List<TileInfos> tileInfos = new List<TileInfos>();

    private void Start()
    {
        float offestX = Random.Range(-9999f, 9999f);
        float offestZ = Random.Range(-9999f, 9999f);
        
        hasBlock = new bool[width, maxHeight, depth];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float nx = (x + offestX) / noiseScale;
                float nz = (z + offestZ) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);

                int h = Mathf.FloorToInt(noise * maxHeight);

                if (h <= 0) continue;

                for (int y = 0; y <= h; y++)
                {
                    if (y == h)
                    {
                        PlaceGrass(x, y, z);
                    }
                    else
                    {
                        Place(x, y, z);
                    }
                    
                    
                }
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                for (int y = 0; y <= 5; y++)
                {
                    if (tileInfos.Find(tile => tile.tileX == x && tile.tileY == y && tile.tileZ == z) == null && !hasBlock[x, y, z])
                    {
                        PlaceWater(x, y, z);
                    }
                }
            }
        }
    }

    void Place(int x, int y, int z)
    {
        var go = Instantiate(dirtPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Dirt_{x}_{y}_{z}";
        hasBlock[x, y, z ] = true;
        
        tileInfos.Add(new TileInfos()
        {
            tileX = x,
            tileY = y,
            tileZ = z,
            tileType = "Dirt"
        });
    }

    void PlaceGrass(int x, int y, int z)
    {
        var go = Instantiate(grassPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Grass_{x}_{y}_{z}";
        hasBlock[x, y, z] = true;

        tileInfos.Add(new TileInfos()
        {
            tileX = x,
            tileY = y,
            tileZ = z,
            tileType = "Grass"
        });
    }

    void PlaceWater(int x, int y, int z)
    {
        var go = Instantiate(waterPrefab, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"Water_{x}_{y}_{z}";
        hasBlock[x, y, z] = true;

        tileInfos.Add(new TileInfos()
        {
            tileX = x,
            tileY = y,
            tileZ = z,
            tileType = "Water"
        });
    }
}
