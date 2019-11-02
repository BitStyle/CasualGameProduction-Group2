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

        if(score > PlayerPrefs.GetFloat("highscore"))
        {
            PlayerPrefs.SetFloat("highscore", score);
        }

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
        score += scoreToAdd;
        score = Mathf.CeilToInt(score);

        //Debug.Log(score);
    }

    private float DistTraveled()
    {
        float deltaZ = transform.position.z - lastLoc.z;
        lastLoc.z = transform.position.z;
        return deltaZ;
    }

    
}
