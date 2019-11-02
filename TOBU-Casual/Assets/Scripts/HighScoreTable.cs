using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] Transform scoreContainer;
    [SerializeField] Transform scoreEntry;
    [SerializeField] float entryHeight = 35f;

    Transform entryTransform;
    float anchorPointY = 0;

    private void Awake()
    {
        scoreEntry.gameObject.SetActive(false);

        for(int i = 0; i < 10; i++)
        {
            anchorPointY = -entryHeight * i;
            entryTransform = Instantiate(scoreEntry, scoreContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, anchorPointY);
            entryTransform.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
