using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] float scoreMultiplierDefault = 0.5f;
    [SerializeField] float scoreMultiplierSpirit = 5f;
    [SerializeField] float scoreMultiplier;
    public TextMeshProUGUI scoreDisplay;
    public Image scoreImage;

    Vector3 startLoc;
    Vector3 lastLoc;

    // Start is called before the first frame update
    void Start()
    {
        scoreMultiplier = scoreMultiplierDefault;
        GetStartLoc();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsPaused)
        {
            if (GameManager.Instance.InSpiritWorld)
            {
                scoreMultiplier = scoreMultiplierSpirit;
            }
            else
            {
                scoreMultiplier = scoreMultiplierDefault;
            }
            TravelScore();
        }
        
        scoreDisplay.text = ": " + GameManager.Instance.Score.ToString();
    }

    private void GetStartLoc()
    {
        startLoc = this.transform.position;
        lastLoc = startLoc;
    }

    private void TravelScore()
    {
        float scoreToAdd = 0;

        scoreToAdd = scoreMultiplier * DistTraveled();
        GameManager.Instance.Score += scoreToAdd;
        GameManager.Instance.Score = Mathf.CeilToInt(GameManager.Instance.Score);

        //Debug.Log(score);
    }

    private float DistTraveled()
    {
        float deltaZ = transform.position.z - lastLoc.z;
        lastLoc.z = transform.position.z;
        return deltaZ;
    }

    
}
