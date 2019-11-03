using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] float scoreMultiplier = 1f;
    public TextMeshProUGUI scoreDisplay;
    public Image scoreImage;

    Vector3 startLoc;
    Vector3 lastLoc;

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
        
        scoreDisplay.text = ": " + GameManager.Instance.Score.ToString();

        Debug.Log(PlayerPrefs.GetFloat("highscore"));
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
