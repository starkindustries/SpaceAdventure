using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class LevelGenerator
{
    enum Direction { Left, Right, Up, Down }

    // Drunkard Walk Algorithm
    // https://www.raywenderlich.com/2637-procedural-level-generation-in-games-tutorial-part-1
    // https://en.wikipedia.org/wiki/Random_walk
    // 1. choose (random) start position and mark as floor
    // 2. pick a random direction to move
    // 3. move in that direction and mark as floor, unless it already is a floor
    // 4. repeat steps 2 & 3 until number of desired floors have been placed
    /// <summary>
    /// Generate's a level based on width, tile count, and starting position
    /// </summary>
    /// <param name="levelWidth"></param>
    /// <param name="tileCount"></param>
    /// <param name="startX"></param>
    /// <param name="startY"></param>
    /// <returns></returns>
    public static int[][] GenerateLevelData(int levelWidth, int tileCount, int startX, int startY)
    {
        // initialize the level with zeros
        int[][] newLevel = new int[levelWidth][];
        for (int i = 0; i < levelWidth; i++)
        {
            newLevel[i] = new int[levelWidth];
        }
        
        // Set start position
        newLevel[startX][startY] = 1;
        
        int currentX = startX;
        int currentY = startY;
        bool deadEnd = false;

        // Loop until desired number of tiles are set
        for (int i = 0; i < tileCount; i++)
        {            
            // initialize list of directions
            Direction[] temp = { Direction.Left, Direction.Right, Direction.Up, Direction.Down }; 
            List<Direction> directions = new List<Direction>(temp);
            Debug.Log("Directions: " + directions[0].ToString() + directions[1].ToString() + directions[2].ToString() + directions[3]);

            // loop until a valid tile is found
            bool tileFound = false;
            while (!tileFound)
            {
                // pick random direction
                int index = Random.Range(0, directions.Count);
                Direction direction = directions[index];
                Debug.Log("direction chosen: " + direction);

                // delta values
                int dx = 0;
                int dy = 0;

                if (direction == Direction.Left)
                {
                    dx = -1;
                }
                else if (direction == Direction.Right)
                {
                    dx = 1;                
                }
                else if (direction == Direction.Up)
                {
                    dy = 1;
                }
                else if (direction == Direction.Down)
                {
                    dy = -1;
                }

                if (IsValidTileCoordinate(x: currentX + dx, y: currentY + dy, level: newLevel))
                {
                    currentX += dx;
                    currentY += dy;
                    newLevel[currentX][currentY] = 1;
                    tileFound = true;
                }
                else
                {
                    directions.RemoveAt(index: index);
                }

                // If the directions list is empty. If empty then we've reached a dead end.
                if (directions.Count == 0)
                {
                    Debug.Log("Deadend reached at [" + currentX + ", " + currentY + "]");
                    deadEnd = true;
                    break;
                }
            }    
            
            if (deadEnd)
            {
                break;
            }
        }
        
        return newLevel;        
    }

    private static bool IsValidTileCoordinate(int x, int y, int[][] level)
    {
        if (!IsWithinBoundsAndEmpty(x, y, level))
        {
            return false;
        }

        // prevent loops: the new tile should have only one valid neighbor tile in order to prevent looping
        int neighborCount = 0;
        if (!IsWithinBoundsAndEmpty(x + 1, y, level)) // check right
        {
            neighborCount++;
        }
        if (!IsWithinBoundsAndEmpty(x - 1, y, level)) // check left
        {
            neighborCount++;
        }
        if (!IsWithinBoundsAndEmpty(x, y + 1, level)) // check up
        {
            neighborCount++;
        }
        if (!IsWithinBoundsAndEmpty(x, y - 1, level)) // check down
        {
            neighborCount++;
        }

        if (neighborCount != 1)
        {
            return false;
        }

        // Valid tile found
        return true;
    }    

    private static bool IsWithinBoundsAndEmpty(int x, int y, int[][] level)
    {
        // check boundaries
        if (x < 0 || x >= level.Length || y < 0 || y >= level[0].Length)
        {
            return false;
        }

        // check if coordinate is empty
        if (level[x][y] == 1)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Places tiles within a tilemap according to the data parameter
    /// </summary>
    /// <param name="levelData"></param>
    /// <param name="levelMap"></param>
    /// <param name="wallTile"></param>
    public static void SetLevelTiles(int[][] levelData, Tilemap levelMap, Tile wallTile)
    {
        for (int i = 0; i < levelData.Length; i++)
        {
            for (int j = 0; j < levelData[i].Length; j++)
            {
                PlaceTileAt(i, j, levelData, levelMap, wallTile);                
            }
        }        
    }

    private static void PlaceTileAt(int x, int y, int[][] levelData, Tilemap levelMap, Tile wallTile)
    {
        if (levelData[x][y] == 0)
        {
            return; 
        }
        if (levelData[x][y] == 1)
        {
            // set all neighboring zeros to walls
            if (IsWithinBoundsAndEmpty(x + 1, y, levelData)) // check right
            {
                levelMap.SetTile(position: new Vector3Int(x + 1, y, 0), tile: wallTile);
            }
            if (IsWithinBoundsAndEmpty(x - 1, y, levelData)) // check left
            {
                levelMap.SetTile(position: new Vector3Int(x - 1, y, 0), tile: wallTile);
            }
            if (IsWithinBoundsAndEmpty(x, y + 1, levelData)) // check up
            {
                levelMap.SetTile(position: new Vector3Int(x, y + 1, 0), tile: wallTile);
            }
            if (IsWithinBoundsAndEmpty(x, y - 1, levelData)) // check down
            {
                levelMap.SetTile(position: new Vector3Int(x, y - 1, 0), tile: wallTile);
            }            
        }
    }

    public static int[][] DoubleLevelDataSize(int[][] levelData)
    {
        int factor = 2;

        // initialize the level with zeros
        int[][] newLevel = new int[levelData.Length * factor][];
        for (int i = 0; i < levelData.Length * factor; i++)
        {
            newLevel[i] = new int[levelData.Length * factor];
        }

        // loop through the data and set newLevel accordingly
        for (int i = 0; i < levelData.Length; i++)
        {
            for (int j = 0; j < levelData[i].Length; j++)
            {
                if (levelData[i][j] == 1)
                {
                    newLevel[i * factor][j * factor] = 1;
                    newLevel[i * factor + 1][j * factor] = 1;
                    newLevel[i * factor][j * factor + 1] = 1;
                    newLevel[i * factor + 1][j * factor + 1] = 1; 
                }
            }
        }

        return newLevel;
    }    

    /// <summary>
    /// Prints level data
    /// </summary>
    /// 
    /// Level
    /// 0 0 0 0 0 0 0 0 0 0 
    /// 0 1 0 0 0 0 0 0 0 0 
    /// 0 1 1 1 0 0 0 0 0 0 
    /// 0 0 0 1 0 0 0 0 0 0 
    /// 0 0 0 1 1 0 0 0 0 0 
    /// 0 0 0 0 0 0 0 0 0 0 
    /// 0 0 0 0 0 0 0 0 0 0 
    /// 0 0 0 0 0 0 0 0 0 0 
    /// 0 0 0 0 0 0 0 0 0 0 
    /// 0 0 0 0 0 0 0 0 0 0
    /// 
    /// <param name="level"></param>
    public static void PrintLevel(int[][] level)
    {
        string output = "Level\n";
        for (int i = 0; i < level.Length; i++)
        {
            for (int j = 0; j < level[i].Length; j++)
            {
                output += level[i][j] + " ";
            }
            output += "\n";
        }
        Debug.Log(output);
    }
}
