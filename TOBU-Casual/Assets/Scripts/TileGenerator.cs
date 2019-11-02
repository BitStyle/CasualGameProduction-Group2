using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject startingTile;
    [SerializeField] GameObject spiritGate;
    [SerializeField] GameObject[] tilesArray;
    [SerializeField] GameObject[] spiritTilesArray;
    [SerializeField] GameObject tileManager;
    [SerializeField] float tileLength = 0f;
    [SerializeField] float timeInSpiritRealm = 10f;
    [SerializeField] int numOfStartingTiles = 1;
    [SerializeField] int tilesInAdvance = 3;
    [SerializeField] int spiritTilesInAdvance = 6;
    [SerializeField] int maxTilesToDisplay = 6;

    Vector3 posToSpawn;
    int currentNumOfTiles = 0;
    bool gateSpawned = false;
    float spiritRealmExitTime = -1f;
    float spiritRealmExitTimeDefault = -1f;
    GameObject[] ringsArray;

    void Start()
    {
        posToSpawn = player.transform.position;

        SpawnStartingTiles();
    }


    void Update()
    {
        if(GameManager.Instance.RingCounter >= 5)
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

    private void DestroyRings()
    {
        ringsArray = GameObject.FindGameObjectsWithTag("Ring");
        foreach(GameObject ring in ringsArray)
        {
            Destroy(ring);
        }
        Debug.Log("Rings Destroyed");
    }

    private void SpawnGate()
    {
        GameObject gateGameObject = Instantiate(spiritGate, posToSpawn, Quaternion.identity) as GameObject;
        gateGameObject.transform.SetParent(this.transform);

        posToSpawn.z += tileLength;
        currentNumOfTiles++;
        Debug.Log("Gate Tile Spawned");
    }

    private void SpawnSpiritTiles()
    {
        float tileBuffer = tilesInAdvance * tileLength;
        Vector3 playerPos = player.transform.position;
        var rng = new System.Random();
        int arrayIndex = rng.Next(1, tilesArray.Length);

        //If the player's position is within 1 tile length of the tile buffer, spawn a new random tile
        if (playerPos.z >= posToSpawn.z - tileBuffer)
        {
            //Instantiate the tile as a game object, and set the Tile Manager as parent
            GameObject tileGameObject = Instantiate(spiritTilesArray[arrayIndex], posToSpawn, Quaternion.identity) as GameObject;
            tileGameObject.transform.SetParent(this.transform);
            Debug.Log("Spirit Tile Spawned");

            //Update spawn position of next tile
            posToSpawn.z += tileLength;
            currentNumOfTiles++;
        }

        
        if(Time.time >= spiritRealmExitTime)
        {
            gateSpawned = false;
            GameManager.Instance.RingCounter = 0;
            spiritRealmExitTime = spiritRealmExitTimeDefault;
            SpawnGate();
            //Debug.Log("Rings RESET. Ring Counter: " + GameManager.Instance.RingCounter);
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
