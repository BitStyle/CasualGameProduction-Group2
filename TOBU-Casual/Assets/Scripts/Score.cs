using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] float scoreMultiplier = 0.05f;
    public TextMeshProUGUI scoreDisplay;
    public Image scoreImage;

    Vector3 startLoc;

    float score = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetStartLoc();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPaused)
        {
            TravelScore();
        }
        
        scoreDisplay.text = ": " + score.ToString();
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
        score = Mathf.Round(score);

        //Debug.Log(score);
    }

    private float DistTraveled()
    {
        float deltaZ = transform.position.z - startLoc.z;
        return deltaZ;
    }


}
