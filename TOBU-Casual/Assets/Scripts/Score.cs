using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] float scoreMultiplier = 1.0f;

    Vector3 startLoc;

    static float score = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetStartLoc();
    }

    // Update is called once per frame
    void Update()
    {
        TravelScore();
    }

    private void GetStartLoc()
    {
        startLoc = transform.position;
    }

    private void TravelScore()
    {
        float scoreToAdd = 0;

        scoreToAdd = scoreMultiplier * DistTraveled();
        score += scoreToAdd;

        Debug.Log(score);
    }

    private float DistTraveled()
    {
        float deltaZ = transform.position.z - startLoc.z;
        return deltaZ;
    }


}
