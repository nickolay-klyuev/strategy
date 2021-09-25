using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class AutoTilesGenerator : MonoBehaviour
{
    [Range(0,100), SerializeField] 
    private int initChance;

    [Range(1,8), SerializeField]
    private int birthLimit;

    [Range(1,8), SerializeField] 
    private int deathLimit;

    [Range(1,10), SerializeField]
    private int numR;

    private int count = 0;

    private int[,] terrainMap;

    [SerializeField] private Vector3Int tmapSize;

    [SerializeField] private Tilemap topMap;
    [SerializeField] private Tilemap botMap;
    [SerializeField] private Tile topTile;
    [SerializeField] private Tile botTile;

    private int width;
    private int height;


    private void CreateTileMap(int numR)
    {
        ClearMap(false);

        width = tmapSize.x;
        height = tmapSize.y;



        /*if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            InitPos();
        }

        for (int i = 0; i < numR; i++)
        {
            terrainMap = GenTilePos(terrainMap);
        }*/

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                /*if (terrainMap[x, y] == 1)
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), topTile);
                }*/
                
                botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), botTile);
            }
        }
    }

    private int[,] GenTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighb;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighb = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0)
                    {
                        continue;
                    }

                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        neighb += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighb++;
                    }
                }

                if (oldMap[x, y] == 1)
                {
                    if (neighb < deathLimit)
                    {
                        newMap[x, y] = 0;
                    }
                    else
                    {
                        newMap[x, y] = 1;
                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighb > birthLimit)
                    {
                        newMap[x, y] = 1;
                    }
                    else
                    {
                        newMap[x, y] = 0;
                    }
                }
            }
        }

        return newMap;
    }

    private void InitPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < initChance ? 1 : 0;
            }
        }
    }

    private void ClearMap(bool complete)
    {
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();

        if (complete)
        {
            terrainMap = null;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            CreateTileMap(numR);
        }
        if (Input.GetMouseButtonDown(1))
        {
            ClearMap(true);
        }*/
    }
}
