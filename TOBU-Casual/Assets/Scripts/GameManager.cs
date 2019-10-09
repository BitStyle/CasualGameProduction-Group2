using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject gameObject = new GameObject("GameManager");
                gameObject.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    public bool IsDead { get; set; } = false;
    private int _ringCounter = 0;
    private int _maxRings = 5;
    public int RingCounter
    {
        get { return _ringCounter; }
        set
        {
            if (_ringCounter > _maxRings)
            {
                _ringCounter = _maxRings;
            }
            else if (_ringCounter < 0)
            {
                _ringCounter = 0;
            }
            else
            {
                _ringCounter = value;
            }
        }
    }

    private void Awake()
    {
        _instance = this;
    }
}
