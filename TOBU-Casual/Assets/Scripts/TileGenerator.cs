using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject startingTile;
    [SerializeField] GameObject[] tilesArray;
    [SerializeField] GameObject tileManager;
    [SerializeField] float tileLength = 0;
    [SerializeField] int numOfStartingTiles = 1;
    [SerializeField] int tilesInAdvance = 3;
    [SerializeField] int maxTilesToDisplay = 6;

    Vector3 posToSpawn;
    int currentNumOfTiles = 0;

    void Start()
    {
        posToSpawn = player.transform.position;

        SpawnStartingTiles();
    }


    void Update()
    {
        SpawnTiles();
        DestroyTiles();
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
        var rng = new System.Random();
        int arrayIndex = rng.Next(0, tilesArray.Length);

        //If the player's position is within 1 tile length of the tile buffer, spawn a new random tile
        if (playerPos.z >= posToSpawn.z - tileBuffer)
        {
            //Instantiate the tile as a game object, and set the Tile Manager as parent
            GameObject tileGameObject = Instantiate(tilesArray[arrayIndex], posToSpawn, Quaternion.identity) as GameObject;
            tileGameObject.transform.SetParent(this.transform);
            Debug.Log("Tile Spawned");

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
