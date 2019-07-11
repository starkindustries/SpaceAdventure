using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Level : MonoBehaviour
{
    public Tilemap levelMap;
    public Tile wallTile;
    public int startX, startY;
    public int width;
    public int tileCount;
    public bool shouldSetTiles;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Systems online!");
        int[][] levelData = LevelGenerator.GenerateLevelData(width, tileCount, startX, startY);
        levelData = LevelGenerator.DoubleLevelDataSize(levelData);
        levelData = LevelGenerator.DoubleLevelDataSize(levelData);
        LevelGenerator.PrintLevel(levelData);
        if (shouldSetTiles)
        {
            LevelGenerator.SetLevelTiles(levelData, levelMap, wallTile);
        }        
    }    

    // Update is called once per frame
    void Update()
    {
        
    }
}
