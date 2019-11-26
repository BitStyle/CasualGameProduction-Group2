using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator_02 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject startingTile;
    [SerializeField] GameObject[][] tilesArray;
    [SerializeField] GameObject tileManager;
    [SerializeField] float tileLength = 0f;
    [SerializeField] float timeInSpiritRealm = 10f;
    [SerializeField] int numOfStartingTiles = 1;
    [SerializeField] int tilesInAdvance = 3;
    [SerializeField] int spiritTilesInAdvance = 6;
    [SerializeField] int maxTilesToDisplay = 6;

    Vector3 posToSpawn;
    int currentNumOfTiles = 0;
    int difficultyMin = 0;
    int difficultyMax = 0;
    bool gateSpawned = false;
    bool inSpiritRealm = false;
    float spiritRealmExitTime = -1f;
    float spiritRealmExitTimeDefault = -1f;

    GameObject[] ringsArray;

    void Start()
    {
        posToSpawn = player.transform.position;
        difficultyMin = 0;
        difficultyMax = 0;

        SpawnStartingTiles();
    }


    void Update()
    {
        if (GameManager.Instance.RingCounter >= 5)
        {
            if (!gateSpawned)
            {
                SpawnGate();

                spiritRealmExitTime = Time.time + timeInSpiritRealm;

                DestroyRings();

                gateSpawned = true;
            }
            else if (gateSpawned)
            {
                SpawnSpiritTiles();
            }
        }
        else
        {
            SpawnTiles();
        }
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
        int arrayIndex = UnityEngine.Random.Range(0, tilesArray.Length - 1);
        int difficultyIndex = UnityEngine.Random.Range(difficultyMin, difficultyMax);

        //If the player's position is within 1 tile length of the tile buffer, spawn a new random tile
        if (playerPos.z >= posToSpawn.z - tileBuffer)
        {
            //Instantiate the tile as a game object, and set the Tile Manager as parent
            GameObject tileGameObject = Instantiate(tilesArray[difficultyIndex][arrayIndex], posToSpawn, Quaternion.identity) as GameObject;
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
