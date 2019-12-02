using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator_02 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject startingTile;
    [SerializeField] GameObject[] easyTilesArray;
    [SerializeField] GameObject[] mediumTilesArray;
    [SerializeField] GameObject[] hardTilesArray;
    [SerializeField] GameObject[,] tilesArray;
    [SerializeField] GameObject tileManager;
    [SerializeField] float tileLength = 0f;
    [SerializeField] float timeInSpiritRealm = 10f;
    [SerializeField] int numOfStartingTiles = 1;
    [SerializeField] int numOfDifficulties = 3;
    [SerializeField] int tilesInAdvance = 3;
    [SerializeField] int spiritTilesInAdvance = 6;
    [SerializeField] int maxTilesToDisplay = 6;
    [SerializeField] float difficultyToMediumBreakpoint = 10.0f;
    [SerializeField] float difficultyToHardBreakpoint = 20.0f;
    [SerializeField] float maxDifficultyBreakpoint = 30.0f;
    float timeOffset;

    Vector3 posToSpawn;
    int currentNumOfTiles = 0;
    int difficultyMin = 0;
    int difficultyMax = 0;
    int longestTileArray = 0;
    int shortestTileArray = 0;
    bool gateSpawned = false;
    bool inSpiritRealm = false;

    void Start()
    {
        timeOffset = Time.time;
        posToSpawn = player.transform.position;
        difficultyMin = 0;
        difficultyMax = 0;

        shortestTileArray = easyTilesArray.Length;

        if (mediumTilesArray.Length < shortestTileArray)
        {
            shortestTileArray = mediumTilesArray.Length;
        }
        if (hardTilesArray.Length < shortestTileArray)
        {
            shortestTileArray = hardTilesArray.Length;
        }

        if (easyTilesArray.Length > longestTileArray)
        {
            longestTileArray = easyTilesArray.Length;
        }
        if (mediumTilesArray.Length > longestTileArray)
        {
            longestTileArray = mediumTilesArray.Length;
        }
        if (hardTilesArray.Length > longestTileArray)
        {
            longestTileArray = hardTilesArray.Length;
        }

        tilesArray = new GameObject[numOfDifficulties, longestTileArray];

        for (int i = 0; i < easyTilesArray.Length; i++)
        {
            tilesArray[0, i] = easyTilesArray[i];
        }
        for (int i = 0; i < mediumTilesArray.Length - 1; i++)
        {
            tilesArray[1, i] = mediumTilesArray[i];
        }
        for (int i = 0; i < hardTilesArray.Length - 1; i++)
        {
            tilesArray[2, i] = hardTilesArray[i];
        }


        SpawnStartingTiles();
    }


    void Update()
    {
        AdjustDifficulty();
        SpawnTiles();
        DestroyTiles();
    }

    private void AdjustDifficulty()
    {
        //Debug.Log(Time.time);
        if (Time.time >= (timeOffset + maxDifficultyBreakpoint))
        {
            difficultyMax = 2;
            difficultyMin = 2;
        }
        else if (Time.time >= (timeOffset + difficultyToHardBreakpoint))
        {
            difficultyMax = 2;
            difficultyMin = 1;
        }
        else if (Time.time >= (timeOffset + difficultyToMediumBreakpoint))
        {
            difficultyMax = 1;
        }
        else
        {
            difficultyMax = 0;
        }
    }

    private void SpawnStartingTiles()
    {
        for (int i = 0; i < numOfStartingTiles; i++)
        {
            //Instantiate the tile as a game object, and set the Tile Manager as parent
            GameObject tileGameObject = Instantiate(startingTile, posToSpawn, Quaternion.identity) as GameObject;
            tileGameObject.transform.SetParent(this.transform);

            //Update spawn position of next tile
            posToSpawn.z += tileLength;
            currentNumOfTiles++;
            Debug.Log("Starting Tile Spawned");
        }
    }

    private void SpawnTiles()
    {
        float tileBuffer = tilesInAdvance * tileLength;
        Vector3 playerPos = player.transform.position;
        //int arrayIndex = UnityEngine.Random.Range(0, tilesArray.Length - 1);
        int difficultyIndex = UnityEngine.Random.Range(difficultyMin, difficultyMax + 1);
        int arrayIndex = 0;

        switch (difficultyIndex)
        {
            case 0:
                arrayIndex = UnityEngine.Random.Range(0, easyTilesArray.Length - 1);
                break;
            case 1:
                arrayIndex = UnityEngine.Random.Range(0, mediumTilesArray.Length - 1);
                break;
            case 2:
                arrayIndex = UnityEngine.Random.Range(0, hardTilesArray.Length - 1);
                break;
            default:
                arrayIndex = UnityEngine.Random.Range(0, shortestTileArray - 1);
                break;
        }

        Debug.Log("Difficulty Max" + difficultyMax);

        //If the player's position is within 1 tile length of the tile buffer, spawn a new random tile
        if (playerPos.z >= posToSpawn.z - tileBuffer)
        {
            //Instantiate the tile as a game object, and set the Tile Manager as parent
            GameObject tileGameObject = Instantiate(tilesArray[difficultyIndex, arrayIndex], posToSpawn, Quaternion.identity) as GameObject;
            tileGameObject.transform.SetParent(this.transform);
            //Debug.Log("Tile Spawned");

            //Update spawn position of next tile
            posToSpawn.z += tileLength;
            currentNumOfTiles++;
        }
    }

    //Gets the oldest child of the Tile Manager and destroys it
    private void DestroyTiles()
    {
        if (currentNumOfTiles > maxTilesToDisplay)
        {
            GameObject oldestTile = tileManager.transform.GetChild(0).gameObject;
            if (oldestTile != null)
            {
                Destroy(oldestTile);
                currentNumOfTiles--;
            }
        }
    }

}
